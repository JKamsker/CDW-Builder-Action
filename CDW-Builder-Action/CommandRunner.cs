using AutoMapper;

using CommandLine;

using CDW_Builder_Action;
using CDW_Builder_Action.Dal;
using CDW_Builder_Action.Models.Configuration;
using CDW_Builder_Action.Models.Database;
using CDW_Builder_Action.Models.Git;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.IO;

public class CommandRunner : IHostedService
{
    private readonly ILogger<CommandRunner> _logger;
    private readonly IOptions<GitConfiguration> _options;
    private readonly IMapper _mapper;
    private readonly EventDao _eventDao;

    public CommandRunner
    (
        EventDao eventDao,
        ILogger<CommandRunner> logger,
        IOptions<GitConfiguration> options,
        IMapper mapper
    )
    {
        _logger = logger;
        _options = options;
        _mapper = mapper;
        _eventDao = eventDao;
        //Environment.GetCommandLineArgs();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var args = Environment.GetCommandLineArgs();
        var parser = CommandLine.Parser.Default.ParseArguments<ActionInputs>(() => new(), args);
        parser.WithNotParsed(
            errors =>
            {
                _logger.LogError(string.Join(Environment.NewLine, errors.Select(error => error.ToString())));
                Environment.Exit(2);
            });

        await parser.WithParsedAsync(options => StartAnalysisAsync(options));
        Environment.Exit(0);
    }

    private async Task StartAnalysisAsync(ActionInputs inputs)
    {
        var events = ParseEvents(inputs);
        foreach (var @event in events)
        {
            var dbEvent = await _eventDao.FindByDateAsync(@event.EventDate);
            if (dbEvent == null)
            {
                dbEvent = _mapper.Map<WorkshopEvent>(@event);
                await _eventDao.InsertAsync(dbEvent);
            }
            else
            {
                dbEvent = _mapper.Map(@event, dbEvent)
                    ?? throw new Exception("This is impossible to reach lol");

                await _eventDao.UpdateAsync(dbEvent);
            }
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
    }

    private List<WorkshopEventDto> ParseEvents(ActionInputs inputs)
    {
        List<WorkshopEventDto> workshopEvents = new();
        List<DateTimeOffset> workshopDates = new();

        //logger.LogInformation("Hey, im actually running, LOL!");
        _logger.LogInformation($"Changes from console: {inputs.ChangedFilesJson}");
        try
        {
            var paths = JsonSerializer.Deserialize<string[]>(inputs.ChangedFilesJson);
            foreach (var item in paths)
            {
                _logger.LogInformation($"Got change: {item}");

                var file = new CdwYamlFile(Path.Combine(_options.Value.BasePath, item));
                if (!file.IsExtensionValid || file.EventDate == null)
                {
                    _logger.LogInformation($"Skipping invalid file");
                    continue;
                }

                if (file.EventDate is DateTimeOffset eventDate)
                {
                    workshopDates.Add(eventDate);
                }

                if (!file.Exists)
                {
                    // delete workshop or smth

                    _logger.LogInformation($"Workshop was deleted");
                    continue;
                }
                try
                {
                    var workshopEvent = file.Read();
                    workshopEvents.Add(workshopEvent);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed Parsing yml: {ex.Message}");
                    continue;
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed deserializing: {ex.Message}");
        }

        return MergeByDate(workshopEvents, workshopDates);

        static List<WorkshopEventDto> MergeByDate(List<WorkshopEventDto> workshopEvents, List<DateTimeOffset> workshopDates)
        {
            var result = workshopEvents
                .SelectMany(x => x.Workshops.Select(workshop => (x.EventDate, workshop)))
                .GroupBy(x => x.EventDate)
                .Select(x => new WorkshopEventDto
                {
                    EventDate = x.Key.Date,
                    Workshops = x.Select(m => m.workshop).ToList()
                }).ToList();

            //Ensure that also empty dates are covered
            foreach (var date in workshopDates)
            {
                if (!result.Any(x => x.EventDate.Date == date.Date))
                {
                    result.Add(new() { EventDate = date, Workshops = new() });
                }
            }

            return result;
        }
    }
}