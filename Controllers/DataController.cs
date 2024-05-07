namespace net.shonx.books.frontend.Controllers;

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using net.shonx.books.frontend.Data;
using net.shonx.books.frontend.Models;
using net.shonx.jwt;

public abstract class DataController(ILogger<BookController> logger, ITokenAcquisition tokenAcquisition) : Controller
{
    protected readonly ILogger<BookController> _logger = logger;
    protected readonly ITokenAcquisition _tokenAcquisition = tokenAcquisition;

    private const string SCOPE = "api://8cb831dd-a219-4922-bbba-98ffdd64fae0/Books";

    protected async Task<JwtToken?> Token()
    {
        string token = await _tokenAcquisition.GetAccessTokenForUserAsync([SCOPE]).ConfigureAwait(false);
        if (token is null)
            return null;
        return TokenCreator.Create(token);
    }

    protected ViewResult ViewWithAdmin(AdminAuth AdminAuth, object? model = null)
    {
        if (AdminAuth is null)
            throw new NullReferenceException("AdminAuth");
        ViewResult view = View(model);
        view.ViewData["AdminAuth"] = AdminAuth;
        return view;
    }

    protected static async Task<Book?> GetBook(string? ISBN)
    {
        return await BackendCommunicator.GetBook(ISBN);
    }

    protected static bool ValidateBook(EditableBook? book)
    {
        if (book is null)
            return false;
        if (string.IsNullOrEmpty(book.id))
            return false;
        if (string.IsNullOrEmpty(book.title))
            return false;
        if (string.IsNullOrEmpty(book.authors))
            return false;
        if (string.IsNullOrEmpty(book.publicationYear))
            return false;
        if (string.IsNullOrEmpty(book.genre))
            return false;
        if (string.IsNullOrEmpty(book.coverImage))
            return false;
        return true;
    }

    protected static async Task<List?> GetList(string? id)
    {
        return await BackendCommunicator.GetList(id);
    }

    protected static bool ValidateList(EditableList? list)
    {
        if (list is null)
            return false;
        if (string.IsNullOrEmpty(list.id))
            return false;
        if (string.IsNullOrEmpty(list.owner))
            return false;
        return true;
    }
}