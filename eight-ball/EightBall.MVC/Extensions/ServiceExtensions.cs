using EightBall.Service.Services;
using EightBall.Shared.ServiceInterfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITableService, TableService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            return services;
        }
    }
}