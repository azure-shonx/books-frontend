namespace net.shonx.books.frontend.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using net.shonx.books.frontend.Data;
using net.shonx.books.frontend.Models;
using net.shonx.jwt;

[AuthorizeForScopes(ScopeKeySection = "Books:Scopes")]
public class BookController(ILogger<BookController> logger, ITokenAcquisition tokenAcquisition) : DataController(logger, tokenAcquisition)
{

    // GET: /Book/
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        return ViewWithAdmin(new(token), BookIndex.AllBooks());
    }

    // GET: /Book/Create
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        if (!auth.IsAdmin)
            return RedirectToAction("Unauthorized", "Error");
        return ViewWithAdmin(auth, new EditableBook());
    }

    // POST: /Book/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind()] EditableBook Book)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        if (!auth.IsAdmin)
            return RedirectToAction("Unauthorized", "Error");
        if (!ValidateBook(Book))
            return RedirectToAction("BadRequest", "Error");
        Book? potential = await GetBook(Book.id);
        if (potential is not null)
            return RedirectToAction("BookNameTaken", "Error");
        await BackendCommunicator.CreateBook(new(Book));
        return RedirectToAction(nameof(Index));
    }

    // GET: /Book/Edit/{ISBN}
    [HttpGet]
    public async Task<IActionResult> Edit(string? ISBN)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        if (!auth.IsAdmin)
            return RedirectToAction("Unauthorized", "Error");
        Book? Book = await GetBook(ISBN);
        if (Book is null)
            return RedirectToAction("NotFound", "Error");
        return ViewWithAdmin(auth, new EditableBook(Book));
    }

    // POST: /Book/Edit/{ISBN}
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? ISBN, [Bind()] EditableBook Book)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        if (!auth.IsAdmin)
            return RedirectToAction("Unauthorized", "Error");
        if (ModelState.IsValid)
        {
            if (!ValidateBook(Book))
                return RedirectToAction("BadRequest", "Error");
            await BackendCommunicator.UpdateBook(new(Book));
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: /Book/Delete/{ISBN}
    [HttpGet]
    public async Task<IActionResult> Delete(string? ISBN)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        if (!auth.IsAdmin)
            return RedirectToAction("Unauthorized", "Error");
        Book? Book = await GetBook(ISBN);
        if (Book is null)
            return RedirectToAction("NotFound", "Error");
        return ViewWithAdmin(auth, Book);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    // POST: /Book/Delete/{ISBN}
    public async Task<IActionResult> DeleteConfirmed(string? ISBN)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        if (!auth.IsAdmin)
            return RedirectToAction("Unauthorized", "Error");
        if (string.IsNullOrEmpty(ISBN))
            return RedirectToAction("NotFound", "Error");
        await BackendCommunicator.DeleteBook(ISBN);
        return RedirectToAction(nameof(Index));
    }
}