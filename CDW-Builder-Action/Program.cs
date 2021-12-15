using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using CommandLine;

//using DotNet.CodeAnalysis;
//using Microsoft.CodeAnalysis.CodeMetrics;
using CDW_Builder_Action;
using CDW_Builder_Action.Models;

//using CDW_Builder_Action.Analyzers;
//using CDW_Builder_Action.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

using Microsoft.Extensions.Configuration;

using static CommandLine.Parser;
using CDW_Builder_Action.Extensions;

// This seems to work in ps
// .\CDW_Builder_Action.exe -f '[\".github/workflows/build.yml\",\"2021-12-17/Plan copy.yml\",\"2021-12-17/Plan.yml\"]'
// Cmd
// CDW_Builder_Action.exe -f "[\".github/workflows/build.yml\",\"2021-12-17/Plan copy.yml\",\"2021-12-17/Plan.yml\"]"
public class Program
{
    public static async Task Main(string[] args)
    {
        //Console.WriteLine(HostDefaults.EnvironmentKey);

        Host.CreateDefaultBuilder(args)
             .UseEnvironment(Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Development")
             //.ConfigureServices((_, services) => { })//services.AddLogging()
             .ConfigureAppConfiguration((context, builder) =>
             {
                 builder.SetBasePath(Directory.GetCurrentDirectory())
                     .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                     .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                     .AddEnvironmentVariables();
             })
            .UseStartup<Startup>()
            .Build()
            .Run();
    }

    private static void GenerateParamString()
    {
        var supposedInput = new[] { ".github/workflows/build.yml", "2021-12-17/Plan copy.yml", "2021-12-17/Plan.yml" };
        var ser = JsonSerializer.Serialize(supposedInput);
        var nObj = new
        {
            commandLineArgs = ser
        };

        var nSer = JsonSerializer.Serialize(nObj);
    }

    //    // https://docs.github.com/actions/reference/workflow-commands-for-github-actions#setting-an-output-parameter
    //    //Console.WriteLine($"::set-output name=updated-metrics::{updatedMetrics}");
    //    //Console.WriteLine($"::set-output name=summary-title::{title}"); // ${{ steps.dotnet-code-metrics.outputs.summary-title }}
    //    //Console.WriteLine($"::set-output name=summary-details::{summary}");

    //    Environment.Exit(0);
}