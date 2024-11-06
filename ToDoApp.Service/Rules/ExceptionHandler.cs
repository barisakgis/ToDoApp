using Core.Entities;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp.Service.Rules;

public static class ExceptionHandler<T>
{

    public static ReturnModel<T> HandleException(Exception exception)
    {
        if(exception.GetType() == typeof(NotFoundException))
        {
            return new ReturnModel<T>
            {
                Message = exception.Message,
                Status = 404,
                Success = false
            };
        }

        if(exception.GetType() == typeof(BusinessException))
        {
            return new ReturnModel<T>
            {
                Message = exception.Message,
                Status = 400,
                Success = false
            };
        }

        return  new ReturnModel<T>
        {
            Message = exception.Message,
            Status = 500,
            Success = false
        };
    }
}
