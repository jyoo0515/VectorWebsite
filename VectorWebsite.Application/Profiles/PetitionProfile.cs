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
    public class PetitionProfile : Profile
    {
        public PetitionProfile()
        {
            CreateMap<Petition, PetitionDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Creator.UserName));

            // CreateMap<PetitionDTO, Petition>()
            //     .ForMember(dest => dest.Comments, opt => opt.Ignore())
            //     .ForMember(dest => dest.CreatedDate, opt => opt.Ignore())
            //     .ForMember(dest => dest.ExpiryDate, opt => opt.Ignore());

            CreateMap<PetitionComment, PetitionCommentDTO>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Creator.UserName));

            // CreateMap<PetitionCommentDTO, PetitionComment>()
            //     .ForMember(dest => dest.Petition, opt => opt.Ignore());
        }
    }
}
