using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDW_Builder_Action.Models.Configuration
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