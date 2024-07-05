using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new
        {
            Error = context.Exception.Message,
            StackTrace = context.Exception.StackTrace
        })
        {
            StatusCode = 500
        };
    }
}
