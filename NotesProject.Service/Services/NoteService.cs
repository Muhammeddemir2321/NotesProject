using AutoMapper;
using Microsoft.AspNetCore.Http;
using NotesProject.Core.DTOs;
using NotesProject.Core.Models;
using NotesProject.Core.Repositories;
using NotesProject.Core.Services;
using NotesProject.Core.UnitOfWorks;
using NotesProject.Repository.Repositories;
using NotesProject.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Service.Services
{
    public class NoteService : Service<Note, NoteDto>, INoteService
    {
        private readonly INoteRepository _noteRepository;
        public NoteService(IRepository<Note> repository, IMapper mapper, IUnitOfWork unitOfWork, INoteRepository noteRepository) : base(repository, mapper, unitOfWork)
        {
            _noteRepository = noteRepository;
        }

        public async Task<ResponseDto<NoteDto>> AddAsync(CreateNoteDto dto)
        {
            var newEntity = _mapper.Map<Note>(dto);
            await _noteRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<NoteDto>(newEntity);
            return ResponseDto<NoteDto>.Succes(newDto, StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<List<NoteDto>>> GetByIdNotesAsync(int id)
        {

            if (id == null)
                return ResponseDto<List<NoteDto>>.Fail("not found id", StatusCodes.Status404NotFound, true);

            var entities = await _noteRepository.GetByIdNotesAsync(id);

            var noteDtos = _mapper.Map<List<NoteDto>>(entities);

            if (noteDtos == null)
                return ResponseDto<List<NoteDto>>.Fail("not found id", StatusCodes.Status400BadRequest, true);

            return ResponseDto<List<NoteDto>>.Succes(noteDtos,StatusCodes.Status200OK);
        }
    }
}