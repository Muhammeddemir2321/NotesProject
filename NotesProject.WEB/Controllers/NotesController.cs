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

        public IActionResult Index(int id)
        {
            return View();
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        public async Task<IActionResult> GetByIdNotes(int id)
        {
            var noteList=await _service.GetByIdNotesAsyn(id);
            return View(noteList);
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
