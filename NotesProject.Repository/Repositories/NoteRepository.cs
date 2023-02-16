using NotesProject.Core.Models;
using NotesProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Repository.Repositories
{
    public class NoteRepository : Repository<Note>,INoteRepository
    {
        public NoteRepository(AppDbContext context) : base(context)
        {
        }
    }
}
