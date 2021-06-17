using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using SharedLibrary.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.CustomExtensions
{
    public static class AddCustomValidationResponse
    {
        public static void UseCustomValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(k => k.Errors.Count > 0).SelectMany(k => k.Errors).Select(k => k.ErrorMessage);
                    ErrorDto errorDto = new ErrorDto(errors.ToList(), true);

                    var response = Response<NoContentResult>.Failed(errorDto, 400);
                    return new BadRequestObjectResult(response);
                };
            });
        }
    }
}
