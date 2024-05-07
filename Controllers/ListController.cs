namespace net.shonx.books.frontend.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using net.shonx.books.frontend.Data;
using net.shonx.books.frontend.Models;
using net.shonx.jwt;
using Newtonsoft.Json;

[AuthorizeForScopes(ScopeKeySection = "Books:Scopes")]
public class ListController(ILogger<BookController> logger, ITokenAcquisition tokenAcquisition) : DataController(logger, tokenAcquisition)
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        List<List> lists = await BackendCommunicator.GetListsByEmail(auth.Email);
        return ViewWithAdmin(auth, lists);
    }

    [HttpGet]
    public async Task<IActionResult> Browse()
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        return ViewWithAdmin(new(token), await BackendCommunicator.GetLists());
    }

    [HttpGet]
    public async new Task<IActionResult> View(string? id)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        List? potential = await BackendCommunicator.GetList(id);
        if (potential is null)
            return RedirectToAction("NotFound", "Error");
        return ViewWithAdmin(new(token), potential);
    }

    [HttpGet]
    public async Task<IActionResult> Subscribe(string? id)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        List? list = await GetList(id);
        if (list is null)
            return RedirectToAction("NotFound", "Error");
        return ViewWithAdmin(new(token), new SubscribeList(list));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Subscribe(string? ID, [Bind()] string? subscribe)
    {
        Console.WriteLine($"ID = [{ID}] subscribe=[{subscribe}]");
        Console.WriteLine($"ModelState.IsValid {ModelState.IsValid}");
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");

        if (ModelState.IsValid)
        {
            bool WantsToSubscribe = !string.IsNullOrEmpty(subscribe);
            List? list = await GetList(ID);
            if (list is null)
                return RedirectToAction("NotFound", "Error");
            AdminAuth auth = new(token);
            if (WantsToSubscribe)
            {
                if (!list.Subscribers.Contains(auth.Email))
                    list.Subscribers.Add(auth.Email);
            }
            else
                list.Subscribers.Remove(auth.Email);
            await BackendCommunicator.UpdateList(list);
            return RedirectToAction(nameof(Browse));
        }
        else
        {
            return RedirectToAction("BadRequest", "Error");
        }

    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        return ViewWithAdmin(auth, new EditableList(auth.Email));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind()] EditableList List)
    {
        JwtToken? token = await Token();
        if (!ValidateList(List))
            return RedirectToAction("BadRequest", "Error");
        List? potential = await GetList(List.id);
        if (potential is not null)
        {
            await System.IO.File.AppendAllTextAsync("taken-list.json", potential.ToJson());
            return RedirectToAction("ListNameTaken", "Error");
        }
        await BackendCommunicator.CreateList(new List(List));
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Edit(string? ID)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        AdminAuth auth = new(token);
        List? list = await GetList(ID);
        if (list is null)
            return RedirectToAction("NotFound", "Error");
        if (auth.IsAdmin || auth.Email.Equals(list.Owner))
        {
            return ViewWithAdmin(auth, new EditableList(list));
        }
        return RedirectToAction("Unauthorized", "Error");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit([Bind()] EditableList list, [Bind()] List<string> SelectedBooks)
    {
        JwtToken? token = await Token();
        if (token is null)
            return RedirectToAction("Unauthorized", "Error");
        if (ModelState.IsValid)
        {
            if (!ValidateList(list))
                return RedirectToAction("BadRequest", "Error");
            List? List = await BackendCommunicator.GetList(list.old_id);
            if (List is null)
                return RedirectToAction("NotFound", "Error");
            List.BookISBNs = SelectedBooks;
            AdminAuth auth = new(token);
            if (!(auth.IsAdmin || auth.Email.Equals(List.Owner)))
                return RedirectToAction("Unauthorized", "Error");
            if (!list.NameChanged())
            {
                await BackendCommunicator.UpdateList(List);
            }
            else
            {
                await BackendCommunicator.DeleteList(list.old_id);
                List.Id = list.id;
                await BackendCommunicator.CreateList(List);
            }
        }
        else
        {
            Console.WriteLine("Invalid ModelState.");
            Console.WriteLine(list.ToJson());
            Console.WriteLine(JsonConvert.SerializeObject(SelectedBooks));
            return RedirectToAction("BadRequest", "Error");
        }
        return RedirectToAction(nameof(Index));
    }
}