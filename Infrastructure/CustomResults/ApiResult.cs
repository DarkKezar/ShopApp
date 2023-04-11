using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Infrastructure.CustomResults;

public class ApiResult : IConvertToActionResult
{
    public readonly string Message;
    public readonly HttpStatusCode HttpStatusCode;
    public readonly Object? ObjectResult;

    public ApiResult(string message, HttpStatusCode httpStatusCode, Object? objectResult = null)
    {
        Message = message;
        HttpStatusCode = httpStatusCode;
        ObjectResult = objectResult;
    }

    public ApiResult(ApiResult result){
        Message = result.Message;
        HttpStatusCode = result.HttpStatusCode;
        ObjectResult = result.ObjectResult;
    }

    public IActionResult Convert()
    {
        Object Result = new {
            Message = Message,
            Value = ObjectResult
        };
        return new ObjectResult(Result){
            StatusCode = (int)HttpStatusCode
        };
    }
}
