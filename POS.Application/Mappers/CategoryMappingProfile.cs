using AutoMapper;
using POS.Application.Dtos.Request;
using POS.Application.Dtos.Response;
using POS.Domain.Entities;
using POS.Infraestructure.Commons.Bases.Response;
using POS.Utilities.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Application.Mappers
{
    public class CategoryMappingProfile :Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CategoryResponseDTO>()
                .ForMember(x => x.StateCategory, x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDTO>>()
                .ReverseMap();

            CreateMap<CategoryRequestDTO, Category>();

            CreateMap<Category, CategorySelectResponseDTO>()
                .ReverseMap();
        }
    }
}
