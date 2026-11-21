using Business_InfoTransito.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Business_InfoTransito.Notifications;
using System.Collections;

namespace Api_InfoTransito.Controllers;

[ApiController]
public abstract class MainController : ControllerBase
{
    private readonly INotifier _notifier;

    public MainController(INotifier notifier)
    {
        _notifier = notifier;
    }

    protected bool ValidOperation()
    {
        return !_notifier.HasNotification();
    }

    protected ActionResult CustomResponse(object result = null)
    {
        if (ValidOperation())
        {
            if (result is ICollection collection && collection.Count == 0)
            {
                return Ok(new
                {
                    success = true,
                    message = "Nenhum registro encontrado.",
                    Data = result
                });
            }

            return Ok(new
            {
                success = true,
                Data = result
            });
        }

        return BadRequest(new
        {
            success = false,
            errors = _notifier.GetNotifications().Select(n => n.Menssage)
        });
    }

    protected ActionResult CustomResponseNotFound(string message = "Nenhum registro encontrado.")
    {
        return NotFound(new
        {
            success = false,
            errors = new[] { message }
        });
    }

    protected ActionResult CustomResponse(ModelStateDictionary modelState)
    {
        if (!ModelState.IsValid) NotifyErrorModelInvalid(modelState);
        return CustomResponse();
    }

    protected void NotifyErrorModelInvalid (ModelStateDictionary modelState)
    {
        var errors = modelState.Values.SelectMany(x => x.Errors);
        foreach (var erro in errors)
        {
            var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
            NotifyError(erroMsg);
        }
    }

    protected void NotifyError(string message)
    {
        _notifier.Handle(new Notification(message));
    }
}
