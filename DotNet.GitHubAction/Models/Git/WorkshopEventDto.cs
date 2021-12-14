﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.GitHubAction.Models.Git
{
    public class WorkshopEventDto
    {
        public DateTimeOffset EventDate { get; set; }
        public List<WorkshopDto> Workshops { get; set; }
    }

    public class WorkshopDto
    {
        public string Begintime { get; set; } = string.Empty;
        public string Endtime { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string TargetAudience { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Prerequisites { get; set; } = string.Empty;
        public List<string> Mentors { get; set; } = new();
        public string ShortCode { get; set; } = string.Empty;
    }
}