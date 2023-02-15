using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NotesProject.Shared.Dtos
{
    public class ResponseDto<TDto>
    {
        public TDto Data { get; set; }
        public ErrorDto Error { get; set; }
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccessful { get; set; }

        public static ResponseDto<TDto> Succes(TDto data, int statusCode)
        {
            return new ResponseDto<TDto> { Data = data, StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<TDto> Succes(int statusCode)
        {
            return new ResponseDto<TDto> { StatusCode = statusCode, IsSuccessful = true };
        }

        public static ResponseDto<TDto> Fail(ErrorDto errorDto, int statusCode)
        {
            return new ResponseDto<TDto> { Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }

        public static ResponseDto<TDto> Fail(string error, int statusCode,bool isShow)
        {
            ErrorDto errorDto = new ErrorDto(error, isShow);

            return new ResponseDto<TDto> {  Error = errorDto, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
