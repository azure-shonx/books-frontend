namespace net.shonx.books.frontend.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class ErrorController : Controller
{
    [AllowAnonymous]
    public IActionResult Index()
    {
        return RedirectToAction("InternalServerError");
    }

    [AllowAnonymous]
    public new IActionResult NotFound()
    {
        Response.StatusCode = 404;
        return  View();
    }

    [AllowAnonymous]
    public new IActionResult BadRequest()
    {
        Response.StatusCode = 400;
        return  View();
    }

    [AllowAnonymous]
    public IActionResult InternalServerError()
    {
        Response.StatusCode = 500;
        return  View();
    }

    [AllowAnonymous]
    public IActionResult BookNameTaken()
    {
        Response.StatusCode = 400;
        return  View();
    }

    [AllowAnonymous]
    public new IActionResult Unauthorized()
    {
        Response.StatusCode = 401;
        return  View();
    }
}
