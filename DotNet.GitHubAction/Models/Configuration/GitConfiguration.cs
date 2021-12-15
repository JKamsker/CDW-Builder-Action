using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.GitHubAction.Models.Configuration
{
    public class GitConfiguration
    {
        private string _basePath;

        public string BasePath
        {
            get => string.IsNullOrEmpty(_basePath) ? Directory.GetCurrentDirectory() : _basePath; 
            set => _basePath = value;
        }
    }
}