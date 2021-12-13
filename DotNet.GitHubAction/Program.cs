using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using CommandLine;

//using DotNet.CodeAnalysis;
//using Microsoft.CodeAnalysis.CodeMetrics;
using DotNet.GitHubAction;

//using DotNet.GitHubAction.Analyzers;
//using DotNet.GitHubAction.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using static CommandLine.Parser;

var supposedInput = new[] { ".github/workflows/build.yml", "2021-12-17/Plan copy.yml", "2021-12-17/Plan.yml" };
var ser = JsonSerializer.Serialize(supposedInput);
var nObj = new
{
    commandLineArgs = ser
};

var nSer = JsonSerializer.Serialize(nObj);

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) => { })//services.AddLogging()
    .Build();

static TService Get<TService>(IHost host)
    where TService : notnull =>
    host.Services.GetRequiredService<TService>();

var logger = Get<ILoggerFactory>(host)
       .CreateLogger("DotNet.GitHubAction.Program");

logger.LogInformation($"Args: '{string.Join("', '", args)}'");

var exists = File.Exists("changes.json");
logger.LogInformation($"File exists: {exists}");
if (exists)
{
    Console.WriteLine($"Changes: '{File.ReadAllText("changes.json")}'");
}

static async Task StartAnalysisAsync(ActionInputs inputs, IHost host)
{
    //using ProjectWorkspace workspace = Get<ProjectWorkspace>(host);
    using CancellationTokenSource tokenSource = new();

    Console.CancelKeyPress += delegate
    {
        tokenSource.Cancel();
    };

    var logger = Get<ILoggerFactory>(host).CreateLogger(nameof(StartAnalysisAsync));
    logger.LogInformation("Hey, im actually running, LOL!");
    logger.LogInformation(inputs.ChangedFilesJson);
    var des = JsonSerializer.Deserialize<string[]>(inputs.ChangedFilesJson);
    Debugger.Break();

    //foreach (var item in inputs.ChangedFiles ?? Enumerable.Empty<string>())
    //{
    //    logger.LogInformation($"Something with file: {item}");
    //}

    //var projectAnalyzer = Get<ProjectMetricDataAnalyzer>(host);

    //Matcher matcher = new();
    //matcher.AddIncludePatterns(new[] { "**/*.csproj", "**/*.vbproj" });

    //Dictionary<string, CodeAnalysisMetricData> metricData = new(StringComparer.OrdinalIgnoreCase);
    //var projects = matcher.GetResultsInFullPath(inputs.Directory);

    //foreach (var project in projects)
    //{
    //    var metrics =
    //        await projectAnalyzer.AnalyzeAsync(
    //            workspace, project, tokenSource.Token);

    //    foreach (var (path, metric) in metrics)
    //    {
    //        metricData[path] = metric;
    //    }
    //}

    //var updatedMetrics = false;
    //var title = "";
    //StringBuilder summary = new();
    //if (metricData is { Count: > 0 })
    //{
    //    var fileName = "CODE_METRICS.md";
    //    var fullPath = Path.Combine(inputs.Directory, fileName);
    //    var logger = Get<ILoggerFactory>(host).CreateLogger(nameof(StartAnalysisAsync));
    //    var fileExists = File.Exists(fullPath);

    //    logger.LogInformation(
    //        $"{(fileExists ? "Updating" : "Creating")} {fileName} markdown file with latest code metric data.");

    //    summary.AppendLine(
    //        title = $"{(fileExists ? "Updated" : "Created")} {fileName} file, analyzed metrics for {metricData.Count} projects.");

    //    foreach (var (path, _) in metricData)
    //    {
    //        summary.AppendLine($"- *{path}*");
    //    }

    //    await File.WriteAllTextAsync(
    //        fullPath,
    //        metricData.ToMarkDownBody(inputs),
    //        tokenSource.Token);

    //    updatedMetrics = true;
    //}
    //else
    //{
    //    summary.Append("No metrics were determined.");
    //}

    // https://docs.github.com/actions/reference/workflow-commands-for-github-actions#setting-an-output-parameter
    //Console.WriteLine($"::set-output name=updated-metrics::{updatedMetrics}");
    //Console.WriteLine($"::set-output name=summary-title::{title}"); // ${{ steps.dotnet-code-metrics.outputs.summary-title }}
    //Console.WriteLine($"::set-output name=summary-details::{summary}");

    Environment.Exit(0);
}

var parser = Default.ParseArguments<ActionInputs>(() => new(), args);
parser.WithNotParsed(
    errors =>
    {
        Get<ILoggerFactory>(host)
            .CreateLogger("DotNet.GitHubAction.Program")
            .LogError(
                string.Join(Environment.NewLine, errors.Select(error => error.ToString())));

        Environment.Exit(2);
    });

await parser.WithParsedAsync(options => StartAnalysisAsync(options, host));
await host.RunAsync();