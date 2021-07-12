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
    public class NoticeProfile : Profile
    {
        public NoticeProfile()
        {
            CreateMap<Notice, NoticeDTO>();
            CreateMap<NoticeDTO, Notice>()
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore());
        }
    }
}
