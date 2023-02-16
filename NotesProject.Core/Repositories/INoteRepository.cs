using NotesProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Core.Repositories
{
    public interface INoteRepository:IRepository<Note>
    {
        Task<List<Note>> GetByIdNotesAsync(int id);
    }
}
