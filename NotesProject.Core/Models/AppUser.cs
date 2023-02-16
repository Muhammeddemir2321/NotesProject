using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Core.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public string City { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}
