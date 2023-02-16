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
        private readonly INoteRepository noteRepository;
        public NoteService(IRepository<Note> repository, IMapper mapper, IUnitOfWork unitOfWork, INoteRepository noteRepository) : base(repository, mapper, unitOfWork)
        {
            this.noteRepository = noteRepository;
        }

        public async Task<ResponseDto<NoteDto>> AddAsync(CreateNoteDto dto)
        {
            var newEntity = _mapper.Map<Note>(dto);
            await noteRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<NoteDto>(newEntity);
            return ResponseDto<NoteDto>.Succes(newDto, StatusCodes.Status201Created);
        }
    }
}