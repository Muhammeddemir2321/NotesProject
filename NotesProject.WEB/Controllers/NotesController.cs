using Microsoft.AspNetCore.Mvc;
using NotesProject.Core.DTOs;
using NotesProject.WEB.Services;

namespace NotesProject.WEB.Controllers
{
    public class NotesController : Controller
    {
        private readonly NoteApiService _service;

        public NotesController(NoteApiService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateNoteDto dto)
        {
            if(ModelState.IsValid)
            {
                await _service.AddAsync(dto);
                return View();
            }
            return View();   
        }
    }
}
