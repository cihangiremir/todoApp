using Core.Utilities.Results;
using Entities.Domain;
using Entities.Dto.Todo;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITodoService
    {
        public Task<IDataResult<TodoOutput>> Add(TodoAddInput addInput);
        public Task<IResult> Update(TodoUpdateInput updateInput);
        public Task<IResult> Delete(Guid id);
        public Task<IDataResult<IList<TodoOutput>>> GetListByFilter(Expression<Func<Todo, bool>> filter = null);
        public Task<IDataResult<TodoOutput>> GetByFilter(Expression<Func<Todo, bool>> filter);
    }
}
