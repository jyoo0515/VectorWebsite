using AutoMapper;
using VectorWebsite.Domain.DTOs;
using VectorWebsite.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorWebsite.Domain.Profiles
{
    public class FinanceReportProfile : Profile
    {
        public FinanceReportProfile()
        {
            CreateMap<FinanceReport, FinanceReportDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Creator.UserName));

            CreateMap<FinanceReportDTO, FinanceReport>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Creator, opt => opt.Ignore())
                .ForMember(dest => dest.FileName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}
