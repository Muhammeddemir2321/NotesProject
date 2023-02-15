using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesProject.Shared.Dtos;

namespace NotesProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ResponseDto<T> responce)
        {
            if (responce.StatusCode == 204)
            {
                return new ObjectResult(null) { StatusCode = responce.StatusCode };
            }

            return new ObjectResult(responce) { StatusCode = responce.StatusCode };
        }
    }
}
