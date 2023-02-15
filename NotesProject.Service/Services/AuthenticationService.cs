using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotesProject.Core.DTOs;
using NotesProject.Core.Models;
using NotesProject.Core.Repositories;
using NotesProject.Core.Services;
using NotesProject.Core.UnitOfWorks;
using NotesProject.Shared.Dtos;

namespace NotesProject.Service.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IRepository<UserRefreshToken> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticationService(ITokenService tokenService, UserManager<AppUser> userManager, IRepository<UserRefreshToken> repository, IUnitOfWork unitOfWork)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDto<TokenDto>> CreateTokenAsync(SignInDto signInDto)
        {
            if (signInDto == null) throw new ArgumentNullException(nameof(signInDto));

            var user = await _userManager.FindByEmailAsync(signInDto.Email);

            if (user == null) ResponseDto<TokenDto>.Fail("Email or Password is wrong", StatusCodes.Status400BadRequest, true);

            if (user != null)
            {
                if (!await _userManager.CheckPasswordAsync(user, signInDto.Password))
                    return ResponseDto<TokenDto>.Fail("Email or Password is wrong", StatusCodes.Status400BadRequest, true);

                var tokenDto = _tokenService.CreateToken(user);

                var userRefreshToken = await _repository.Where(x => x.UserId == user.Id).SingleOrDefaultAsync();

                if (userRefreshToken == null)
                {
                    await _repository.AddAsync(new UserRefreshToken { UserId = user.Id, Code = tokenDto.RefreshToken, Expiration = tokenDto.RefreshTokenExpiration });
                }
                else
                {
                    userRefreshToken.Code = tokenDto.RefreshToken;
                    userRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;
                }

                await _unitOfWork.CommitAsync();

                return ResponseDto<TokenDto>.Succes(tokenDto, StatusCodes.Status201Created);
            }

            return ResponseDto<TokenDto>.Fail("Bilinmeyen bir hata oluştu", StatusCodes.Status500InternalServerError, false);
        }

        public async Task<ResponseDto<TokenDto>> CreateTokenByRefreshTokenAsync(string refreshTokenCode)
        {
            var existRefreshToken = await _repository.Where(x => x.Code == refreshTokenCode).SingleOrDefaultAsync();

            if (existRefreshToken == null) ResponseDto<TokenDto>.Fail("Refresh token not found", StatusCodes.Status404NotFound, true);

            var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);

            if (user == null) ResponseDto<TokenDto>.Fail("User not found", StatusCodes.Status404NotFound, true);

            var tokenDto = _tokenService.CreateToken(user);

            existRefreshToken.Code = tokenDto.RefreshToken;
            existRefreshToken.Expiration = tokenDto.RefreshTokenExpiration;

            await _unitOfWork.CommitAsync();

            return ResponseDto<TokenDto>.Succes(tokenDto, StatusCodes.Status201Created);
        }

        public async Task<ResponseDto<NoContentDto>> RevokeRefreshTokenAsync(string refreshTokenCode)
        {
            var existRefreshToken = await _repository.Where(x => x.Code == refreshTokenCode).SingleOrDefaultAsync();

            if (existRefreshToken == null) ResponseDto<TokenDto>.Fail("Refresh token not found", StatusCodes.Status404NotFound, true);

            _repository.Remove(existRefreshToken);

            await _unitOfWork.CommitAsync();

            return ResponseDto<NoContentDto>.Succes(200);
        }
    }
}
