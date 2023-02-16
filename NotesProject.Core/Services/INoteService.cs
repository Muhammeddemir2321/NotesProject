using NotesProject.Core.DTOs;
using NotesProject.Core.Models;
using NotesProject.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Core.Services
{
    public interface INoteService:IService<Note,NoteDto>
    {
        Task<ResponseDto<List<NoteDto>>> GetByIdNotesAsync(int id);
        Task<ResponseDto<NoteDto>> AddAsync(CreateNoteDto dto);

    }

    

}
