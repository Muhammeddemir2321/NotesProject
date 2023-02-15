using NotesProject.Core.DTOs;
using NotesProject.Shared.Dtos;

namespace NotesProject.Core.Services
{
    public interface IUserService
    {
        Task<ResponseDto<AppUserDto>> CreateUserAsync(CreateUserDto createUserDto);
        Task<ResponseDto<AppUserDto>> GetUserFindByNameAsync(string userName);
    }
}
