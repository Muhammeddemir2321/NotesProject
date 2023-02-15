using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NotesProject.Core.DTOs;
using NotesProject.Core.Models;
using NotesProject.Core.Services;
using NotesProject.Core.UnitOfWorks;
using NotesProject.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Service.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto)
        {
            var userEntity = new AppUser { Email = createUserDto.Email, UserName = createUserDto.UserName, FullName = createUserDto.FullName, City = createUserDto.City };

            var result=await _userManager.CreateAsync(userEntity,createUserDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description).ToList();

                return ResponseDto<AppUserDto>.Fail(new ErrorDto(errors, true), StatusCodes.Status400BadRequest);
            }

            var userDto = _mapper.Map<AppUserDto>(userEntity);

            return ResponseDto<AppUserDto>.Succes(userDto, StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<AppUserDto>> GetUserFindByNameAsync(string userName)
        {
            if (userName == null) throw new ArgumentNullException("Token is wrong");

            var userEntity = await _userManager.FindByNameAsync(userName);

            if (userEntity == null) return ResponseDto<AppUserDto>.Fail("username is not found", StatusCodes.Status404NotFound, true);

            var userDto = _mapper.Map<AppUserDto>(userEntity);

            return ResponseDto<AppUserDto>.Succes(userDto, StatusCodes.Status200OK);
        }
    }
}
