using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Note>> GetByIdNotesAsync(int id)
        {
            //var noteIds = await _context.Notes
            //    .FromSqlInterpolated($"SELECT * FROM dbo.GetDescendantNodeIds({id})")
            //    .Select(n => n.Id).ToListAsync();

            //var notes = await _context.Notes
            //    .Where(n => noteIds.Contains(id))
            //    .ToListAsync();;


            var notes=_context.Notes.FromSqlRaw(@"
                    WITH Tree (Id, ParentId, Value, CreatedTime, UpdatedTime, UserId) AS
                    (
                        SELECT Id, ParentId, Value, CreatedTime, UpdatedTime, UserId
                        FROM Notes
                        WHERE ParentId IS NULL
                        UNION ALL
                        SELECT n.Id, n.ParentId, n.Value, n.CreatedTime, n.UpdatedTime, n.UserId
                        FROM Notes n
                        JOIN Tree t ON n.ParentId = t.Id
                    )
                    SELECT Id, Value, ParentId, CreatedTime, UpdatedTime, UserId
                    FROM Tree
                    ORDER BY Id")
                                  .ToList();

            return notes;
        }
    }
}
