using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation.AppUser;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Mapper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Domain;
using Entities.Dto.Todo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TodoManager : ITodoService
    {
        private ITodoDal _todoDal;
        public TodoManager(ITodoDal todoDal)
        {
            _todoDal = todoDal;
        }

        [ValidationAspect(typeof(TodoAddInputValidator))]
        public async Task<IDataResult<TodoOutput>> Add(TodoAddInput addInput)
        {
            var todo = MapsterTool.Map<TodoAddInput, Todo>(addInput);
            todo.TodoItems.ToList().ForEach(t => t.Id = Guid.NewGuid());
            await _todoDal.Add(todo);

            var todoOutput = MapsterTool.Map<Todo, TodoOutput>(todo);
            return new SuccessDataResult<TodoOutput>(todoOutput, Messages.Successfully);

        }

        public async Task<IResult> Delete(Guid id)
        {
            var todo = await _todoDal.Get(t => t.Id == id);
            if (todo == null) return new ErrorResult(Messages.UnSuccessfully);
            await _todoDal.Delete(todo);

            return new SuccessResult(Messages.Successfully);
        }

        public async Task<IDataResult<TodoOutput>> GetByFilter(Expression<Func<Todo, bool>> filter)
        {
            var todo = await _todoDal.Get(filter);

            var todoOutput = MapsterTool.Map<Todo, TodoOutput>(todo);

            return new SuccessDataResult<TodoOutput>(todoOutput, Messages.Successfully);
        }

        public async Task<IDataResult<IList<TodoOutput>>> GetListByFilter(Expression<Func<Todo, bool>> filter = null)
        {
            var todos = await _todoDal.GetList(filter);
            var todoOutputs = MapsterTool.Map<IList<Todo>, IList<TodoOutput>>(todos);

            return new SuccessDataResult<IList<TodoOutput>>(todoOutputs, Messages.Successfully);
        }
        [ValidationAspect(typeof(TodoUpdateInputValidator))]
        public async Task<IResult> Update(TodoUpdateInput updateInput)
        {
            var todo = await _todoDal.Get(t => t.Id == updateInput.Id);
            todo.TodoItems.ToList().Where(t => t.Id == null).ToList().ForEach(t => t.Id = Guid.NewGuid());
            todo.TodoItems = MapsterTool.Map<IList<TodoItemUpdateInput>, IList<TodoItem>>(updateInput.TodoItems);

            await _todoDal.Update(todo);

            return new SuccessResult(Messages.Successfully);
        }

    }
}
