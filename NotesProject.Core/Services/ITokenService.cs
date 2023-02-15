using NotesProject.Core.DTOs;
using NotesProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(AppUser appUser);
    }
}
