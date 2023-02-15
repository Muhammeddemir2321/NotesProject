using NotesProject.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NotesProject.Core.Services
{
    public interface IService<TEntity,TDto> where TEntity : class where TDto:class
    {
        Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync();
        Task<ResponseDto<TDto>> GetByIdAsync(int id);
        Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> expression);
        Task<ResponseDto<bool>> AnyAsync(Expression<Func<TEntity, bool>> expression);
        Task<ResponseDto<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> dtos);
        Task<ResponseDto<TDto>> AddAsync(TDto dto);
        Task<ResponseDto<NoContentDto>> UpdateAsync(TDto dto);
        Task<ResponseDto<NoContentDto>> RemoveAsync(int id);
        Task<ResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids);
    }
}
