namespace net.shonx.books.frontend.Data;

using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using net.shonx.books.frontend.Models;
using Newtonsoft.Json;

internal static class Helper
{
    private static readonly HttpClient httpClient = new();
    internal static async Task<T?> WriteRequest<T>(HttpRequestMessage request, IJsonable? data = null)
    {
        if (data is not null)
        {
            request.Content = new StringContent(data.ToJson(), Encoding.UTF8, "application/json");
        }
        using HttpResponseMessage response = await httpClient.SendAsync(request);
        HttpStatusCode statusCode = response.StatusCode;
        int sc = (int)statusCode;
        if (!((sc >= 200 && sc < 300) || sc == 404))
        {
            await Console.Error.WriteLineAsync($"Helper got response {sc}: {statusCode}");
            return default;
        }
        return await GetReply<T>(response.Content.ReadAsStream());

    }

    internal static List? Verify(List? list)
    {
        if (list is null)
            return null;
        return string.IsNullOrEmpty(list.Id) &&
                string.IsNullOrEmpty(list.Owner) &&
                CollectionUtilities.IsNullOrEmpty(list.BookISBNs) &&
                CollectionUtilities.IsNullOrEmpty(list.Subscribers) &&
                CollectionUtilities.IsNullOrEmpty(list.Books)
            ? null
            : list;
    }
    internal static List<List> Verify([NotNullWhen(false)] List<List>? Lists)
    {
        List<List> BadLists = [];

        if (Lists is null)
            return [];
        foreach (List? l in Lists)
            if (Verify(l) is null)
            {
                BadLists.Add(l);
            }
        foreach (List? l in BadLists)
            Lists.Remove(l);
        return Lists;
    }

    internal static List<Book> Verify([NotNullWhen(false)] List<Book>? Books)
    {
        List<Book> BadBooks = [];
        if (Books is null)
            return [];
        foreach (Book? b in Books)
            if (Verify(b) is null)
            {
                BadBooks.Add(b);
            }
        foreach (Book b in BadBooks)
            Books.Remove(b);
        return Books;
    }

    internal static Book? Verify(Book? book)
    {
        if (book is null)
            return null;
        return string.IsNullOrEmpty(book.Id) &&
                string.IsNullOrEmpty(book.Title) &&
                CollectionUtilities.IsNullOrEmpty(book.Authors) &&
                string.IsNullOrEmpty(book.PublicationYear) &&
                string.IsNullOrEmpty(book.Genre) &&
                string.IsNullOrEmpty(book.CoverImage)
            ? null
            : book;
    }


    private static async Task<T?> GetReply<T>(Stream stream)
    {
        string json = await new StreamReader(stream).ReadToEndAsync();
        if (string.IsNullOrEmpty(json))
        {
            return default;
        }
        return JsonConvert.DeserializeObject<T>(json);
    }
}