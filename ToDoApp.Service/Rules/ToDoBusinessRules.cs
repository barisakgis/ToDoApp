using Core.Exceptions;
using ToDoApp.Repository.Repositories.Abstracts;
using ToDoApp.Service.Constants;

namespace ToDoApp.Service.Rules;

public class ToDoBusinessRules(IToDoRepository _toDoRepository)
{


    public virtual bool ToDoIsPresent(Guid id)
    {
        var toDo = _toDoRepository.GetById(id);
        if (toDo is null)
        {
            throw new NotFoundException(Messages.ToDoIsNotPresentMessage(id));
        }

        return true;

    }


    public void ToDoTitleMustBeUnique(string title)
    {
        var toDo = _toDoRepository.GetAll(x => x.Title == title);
        if (toDo.Count > 0)
        {
            throw new BusinessException("ToDo benzersiz olmalı.");
        }
    }

}
