using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotesProject.Core.DTOs;
using NotesProject.Core.Services;

namespace NotesProject.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthsController : BaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthsController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken(SignInDto signInDto)
        {
            return CreateActionResult(await _authenticationService.CreateTokenAsync(signInDto));
        }

        [HttpPost]
        public async Task<IActionResult> RevokeRefreshToken(UserRefreshTokenDto userRefreshTokenDto)
        {
            return CreateActionResult(await _authenticationService.RevokeRefreshTokenAsync(userRefreshTokenDto.Code));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTokenByRefreshToken(UserRefreshTokenDto userRefreshTokenDto)
        {
            return CreateActionResult(await _authenticationService.CreateTokenByRefreshTokenAsync(userRefreshTokenDto.Code));
        }
    }
}
