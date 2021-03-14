using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EightBall.MVC.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelStateErrors(this ModelStateDictionary modelState, Dictionary<string, string> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }
        }
    }
}