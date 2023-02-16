using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Core.Models
{
    public class Note:BaseEntity
    {
        public int ParentId { get; set; }
        public string Value { get; set; }

    }
}
