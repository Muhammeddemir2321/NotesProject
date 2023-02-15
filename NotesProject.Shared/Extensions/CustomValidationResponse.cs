using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NotesProject.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Shared.Extensions
{
    public static class CustomValidationResponse
    {
        public static void UseCustomValidationResponce(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Values.Where(x => x.Errors.Count > 0).SelectMany(x => x.Errors).Select(x => x.ErrorMessage);

                    ErrorDto errorDto = new ErrorDto(errors.ToList(), true);

                    var responce = ResponseDto<NoContentDto>.Fail(errorDto, 400);

                    return new BadRequestObjectResult(responce);
                };
            });
        }
    }
}
