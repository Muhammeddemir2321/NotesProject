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
            //    .ToListAsync();;;

            var notes=await _context.Notes.FromSqlRaw(@$"
                    WITH NodeHierarchy (Id, ParentId, Value, CreatedTime, UpdatedTime, UserId,Level)
                    AS 
                    (
                        SELECT Id, ParentId, Value, CreatedTime, UpdatedTime, UserId, 0 AS Level
                        FROM Notes
                        WHERE Id = {id}
		                    UNION ALL
                        SELECT n.Id, n.ParentId, n.Value, n.CreatedTime, n.UpdatedTime, n.UserId, nh.Level + 1
                        FROM Notes as n
		                    INNER JOIN
	                    NodeHierarchy as nh ON n.ParentId = nh.Id
                    )
                    SELECT Id, ParentId, Value,CreatedTime, UpdatedTime, UserId, Level
                    FROM NodeHierarchy")
                                  .ToListAsync();

            return notes;
        }
    }
}
