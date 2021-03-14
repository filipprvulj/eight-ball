using AutoMapper;
using EightBall.Data.Entities;
using EightBall.MVC.Models;
using EightBall.Shared.Dtos;
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
        }
    }
}