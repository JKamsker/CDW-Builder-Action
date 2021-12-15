using AutoMapper;

using CDW_Builder_Action.Models.Database;
using CDW_Builder_Action.Models.Git;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rnd.Mapping.Automapper;

namespace CDW_Builder_Action.Models.Mapping
{
    public class WorkshopEventProfile : Profile
    {
        public WorkshopEventProfile()
        {
            CreateMap<WorkshopEventDto, WorkshopEvent>();
            CreateMap<WorkshopDto, Workshop>();
        }
    }
}