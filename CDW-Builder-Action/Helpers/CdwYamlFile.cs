using CDW_Builder_Action.Models.Git;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CDW_Builder_Action
{
    public class CdwYamlFile
    {
        private readonly FileInfo _file;

        public bool Exists { get; private set; }

        public bool IsExtensionValid { get; private set; }

        public DateTimeOffset? EventDate { get; private set; }

        public CdwYamlFile(string file) : this(new FileInfo(file))
        {
        }

        public CdwYamlFile(FileInfo file)
        {
            _file = file;
            Exists = file.Exists;
            CheckExtension(file);
            InitializeEventDate(file);
        }

        private void CheckExtension(FileInfo file)
        {
            IsExtensionValid = file.Extension == ".yml";
        }

        private void InitializeEventDate(FileInfo file)
        {
            var parentName = file.Directory?.Name;
            if (parentName != null)
            {
                var dateMatch = Regex.Match(parentName, @"^(\d{4})-(\d{2})-(\d{2})$");
                if (dateMatch.Success)
                {
                    var dateParts = dateMatch.Groups
                        .Cast<Group>()
                        .Skip(1)
                        .Select(x => int.Parse(x.Value))
                        .ToArray();

                    var tz = TimeZoneInfo.GetSystemTimeZones().First(x => x.Id == "W. Europe Standard Time");
                    var offset = tz.GetUtcOffset(new DateTime(dateParts[0], dateParts[1], dateParts[2]));
                    EventDate = new DateTimeOffset(dateParts[0], dateParts[1], dateParts[2], 0, 0, 0, offset);
                }
            }
        }

        public WorkshopEventDto Read()
        {
            if (!Exists)
            {
                throw new Exception("Cannot read workshop data: File does not exist");
            }

            if (!IsExtensionValid)
            {
                throw new Exception("Cannot read workshop data: Invalid filetype");
            }

            if (EventDate == null)
            {
                throw new Exception("Cannot read workshop data: Invalid parent folder format");
            }

            using var fs = _file.OpenRead();
            using var ymlContent = new StreamReader(fs);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var result = deserializer.Deserialize<WorkshopEventDto>(ymlContent);
            result.EventDate = EventDate.Value;

            return result;
        }
    }
}