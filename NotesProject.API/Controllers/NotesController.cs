using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesProject.Core.DTOs;
using NotesProject.Core.Services;

namespace NotesProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : BaseController
    {
        private readonly INoteService _service;

        public NotesController(INoteService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateNoteDto dto)
        {
            return CreateActionResult(await _service.AddAsync(dto));
        }
    }
}
