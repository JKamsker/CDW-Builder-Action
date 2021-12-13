using System;
using CommandLine;

namespace DotNet.GitHubAction
{
    public class ActionInputs
    {
        string _repositoryName = null!;
        string _branchName = null!;

        public ActionInputs()
        {
            var greetings = Environment.GetEnvironmentVariable("GREETINGS");
            if (greetings is { Length: > 0 })
            {
                Console.WriteLine(greetings);
            }
        }

        [Option('f', "files",
            Required = true,
            HelpText = "Changed Files")]
        public string ChangedFilesJson { get; set; } = null!;

        //[Option('n', "name",
        //    Required = true,
        //    HelpText = "The repository name, for example: \"samples\". Assign from `github.repository`.")]
        //public string Name
        //{
        //    get => _repositoryName;
        //    set => ParseAndAssign(value, str => _repositoryName = str);
        //}

        //[Option('b', "branch",
        //    Required = true,
        //    HelpText = "The branch name, for example: \"refs/heads/main\". Assign from `github.ref`.")]
        //public string Branch
        //{
        //    get => _branchName;
        //    set => ParseAndAssign(value, str => _branchName = str);
        //}

        //[Option('d', "dir",
        //    Required = true,
        //    HelpText = "The root directory to start recursive searching from.")]
        //public string Directory { get; set; } = null!;

        //[Option('w', "workspace",
        //    Required = true,
        //    HelpText = "The workspace directory, or repository root directory.")]
        //public string WorkspaceDirectory { get; set; } = null!;

        //static void ParseAndAssign(string? value, Action<string> assign)
        //{
        //    if (value is { Length: > 0 } && assign is not null)
        //    {
        //        assign(value.Split("/")[^1]);
        //    }
        //}
    }
}
