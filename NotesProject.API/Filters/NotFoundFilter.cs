using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using NotesProject.Shared.Dtos;
using NotesProject.Core.Repositories;

namespace NotesProject.API.Filters
{
    //public class NotFoundFilter<TEntity> : IAsyncActionFilter where TEntity : class
    //{
    //    private readonly IRepository<TEntity> _repository;

    //    public NotFoundFilter(IRepository<TEntity> repository)
    //    {
    //        _repository = repository;
    //    }

    //    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //    {
    //        var id = context.ActionArguments.Values.FirstOrDefault();

    //        if (id == null)
    //        {
    //            await next.Invoke();
    //            return;
    //        }

    //        var anyEntity = await _repository.AnyAsync(i => i.Id == (int)id);

    //        if (anyEntity)
    //        {
    //            await next.Invoke();
    //            return;
    //        }

    //        context.Result = new NotFoundObjectResult(ResponseDto<NoContent>
    //                                                    .Fail(($"{typeof(TEntity).Name} {id} not found"), 404, true));
    //    }
    //}
}
