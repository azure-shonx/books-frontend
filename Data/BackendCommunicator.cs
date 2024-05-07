namespace net.shonx.books.frontend.Data;

using Microsoft.IdentityModel.Tokens;
using net.shonx.books.frontend.Models;

public static class BackendCommunicator
{
    private static string BuildURL(string method, string operation, string? id = null)
    {
        if (string.IsNullOrEmpty(operation))
            throw new NullReferenceException();
        UriBuilder uriBuilder = new(Constants.APIM_URL);
        uriBuilder.Path += method + "/" + operation;
        if (string.IsNullOrEmpty(id))
            uriBuilder.Query = $"key={AppConfig.APIM_KEY}";
        else
            uriBuilder.Query = $"id={id}&key={AppConfig.APIM_KEY}";
        return uriBuilder.Uri.ToString();
    }
    public static async Task CreateBook(Book? book)
    {
        if (book is null)
            return;
        HttpRequestMessage request = new(HttpMethod.Post, BuildURL(Constants.BOOK, "create", book.Id));
        await Helper.WriteRequest<object>(request, book);
        BookIndex.AddBook(book);
    }

    public static async Task DeleteBook(string? ISBN)
    {
        if (string.IsNullOrEmpty(ISBN))
            return;
        HttpRequestMessage request = new(HttpMethod.Delete, BuildURL(Constants.BOOK, "delete", ISBN));
        await Helper.WriteRequest<object>(request);
        BookIndex.RemoveBook(ISBN);
    }

    public static async Task<List<Book>> GetBooks()
    {
        HttpRequestMessage request = new(HttpMethod.Get, BuildURL(Constants.BOOK, "get"));
        List<Book>? Books = await Helper.WriteRequest<List<Book>>(request);
        return Helper.Verify(Books);
    }

    public static async Task<Book?> GetBook(string? ISBN)
    {
        if (string.IsNullOrEmpty(ISBN))
            return null;
        HttpRequestMessage request = new(HttpMethod.Get, BuildURL(Constants.BOOK, "get", ISBN));
        Book? b = await Helper.WriteRequest<Book>(request);
        return Helper.Verify(b);
    }

    public static async Task UpdateBook(Book? book)
    {
        if (book is null)
            return;
        HttpRequestMessage request = new(HttpMethod.Put, BuildURL(Constants.BOOK, "update", book.Id));
        await Helper.WriteRequest<object>(request, book);
        BookIndex.AddBook(book);
    }

    public static async Task CreateList(List? list)
    {
        if (list is null)
            return;
        HttpRequestMessage request = new(HttpMethod.Post, BuildURL(Constants.LIST, "create", list.Id));
        await Helper.WriteRequest<object>(request, list);
    }

    public static async Task DeleteList(string? ID)
    {
        if (string.IsNullOrEmpty(ID))
            return;
        HttpRequestMessage request = new(HttpMethod.Delete, BuildURL(Constants.LIST, "delete", ID));
        await Helper.WriteRequest<object>(request);
    }

    public static async Task<List<List>> GetLists()
    {
        HttpRequestMessage request = new(HttpMethod.Get, BuildURL(Constants.LIST, "get"));
        List<List>? Lists = await Helper.WriteRequest<List<List>>(request);
        return Helper.Verify(Lists);
    }

    public static async Task<List?> GetList(string? id)
    {
        if (string.IsNullOrEmpty(id))
            return null;
        HttpRequestMessage request = new(HttpMethod.Get, BuildURL(Constants.LIST, "get", id));
        List? l = await Helper.WriteRequest<List>(request);
        return Helper.Verify(l);
    }

    public static async Task<List<List>> GetListsByEmail(string? email)
    {
        if (string.IsNullOrEmpty(email))
            return [];
        HttpRequestMessage request = new(HttpMethod.Get, BuildURL(Constants.LIST, "getbyemail", email));
        List<List>? Lists = await Helper.WriteRequest<List<List>>(request) ?? [];
        return Helper.Verify(Lists);
    }

    public static async Task UpdateList(List? list)
    {
        if (list is null)
            return;
        HttpRequestMessage request = new(HttpMethod.Put, BuildURL(Constants.LIST, "update", list.Id));
        await Helper.WriteRequest<object>(request, list);
    }

}