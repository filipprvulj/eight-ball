using AutoMapper;
using EightBall.Data.Entities;
using EightBall.MVC.Models;
using EightBall.Shared.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region Table Maps

            CreateMap<Table, TableDto>().ReverseMap();
            CreateMap<TableDto, TableModel>().ReverseMap();

            #endregion Table Maps

            #region Appointment maps

            CreateMap<Appointment, AppointmentDto>().ReverseMap();
            CreateMap<AppointmentDto, AppointmentModel>().ReverseMap();

            #endregion Appointment maps

            #region Reservation maps

            CreateMap<IdentityUser, UserDto>()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => Guid.Parse(src.Id)))
                .ReverseMap()
                .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id.ToString()));

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => Guid.Parse(src.UserId)))
                .ReverseMap()
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId.ToString()));

            #endregion Reservation maps
        }
    }
}