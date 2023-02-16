using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Core.DTOs
{
    public class CreateNoteDto
    {
        public int ParentId { get; set; }
        public string Value { get; set; }
    }
}
