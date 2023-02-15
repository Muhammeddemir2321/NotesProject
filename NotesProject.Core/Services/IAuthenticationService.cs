using NotesProject.Core.DTOs;
using NotesProject.Shared.Dtos;

namespace NotesProject.Core.Services
{
    public interface IAuthenticationService
    {
        Task<ResponseDto<TokenDto>> CreateTokenAsync(SignInDto signInDto);
        Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshToken);
        Task<ResponseDto<NoContentDto>> RevokeRefreshTokenAsync(string refreshToken);
    }
}
