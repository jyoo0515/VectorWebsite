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
    public class InquiryProfile : Profile
    {
        public InquiryProfile()
        {
            CreateMap<Inquiry, InquiryDTO>();
            CreateMap<InquiryDTO, Inquiry>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}
