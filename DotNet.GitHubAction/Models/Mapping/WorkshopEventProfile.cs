using AutoMapper;

using DotNet.GitHubAction.Models.Database;
using DotNet.GitHubAction.Models.Git;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rnd.Mapping.Automapper;

namespace DotNet.GitHubAction.Models.Mapping
{
    public class WorkshopEventProfile : Profile
    {
        public WorkshopEventProfile()
        {
            base.CreateMap<WorkshopEventDto, WorkshopEvent>()
                .SimpleMap(x => x, x => x);
        }
    }
}