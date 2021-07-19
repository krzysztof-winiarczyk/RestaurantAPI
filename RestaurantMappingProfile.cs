using AutoMapper;
using RestaurantAPI.Entities;
using RestaurantAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantAPI
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(dest => dest.City, conf => conf.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, conf => conf.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.PostalCode, conf => conf.MapFrom(src => src.Address.PostalCode));

            //if properties of class and classDto are the same we do not have to map them manually

            CreateMap<Dish, DishDto>(); //create map cause we use DishDto in RestaurantDto

            CreateMap<CreateRestaurantDto, Restaurant>()
                .ForMember(r => r.Address, c => c.MapFrom(dto => new Address() 
                    { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));

            CreateMap<CreateDishDto, Dish>();
        }
    }
}
