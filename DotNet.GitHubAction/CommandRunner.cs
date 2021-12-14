using CommandLine;

using DotNet.GitHubAction;
using DotNet.GitHubAction.Dal;
using DotNet.GitHubAction.Models.Git;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;



public class CommandRunner : IHostedService
{
    private readonly ILogger<CommandRunner> _logger;
    private readonly EventDao _eventDao;

    public CommandRunner
    (
        ILogger<CommandRunner> logger,
        EventDao eventDao
    )
    {
        _logger = logger;
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
        var events = GetEvents(inputs);
        foreach (var @event in events)
        {
            var dbEvent = _eventDao.FindByDateAsync(@event.EventDate);
        }
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
    }

    private List<WorkshopEventDto> GetEvents(ActionInputs inputs)
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

                var file = new CdwYamlFile(item);
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