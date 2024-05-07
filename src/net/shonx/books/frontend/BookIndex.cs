namespace net.shonx.books.frontend;

using System.Diagnostics.CodeAnalysis;
using net.shonx.books.frontend.Data;
using net.shonx.books.frontend.Models;

public static class BookIndex
{
    private static readonly Dictionary<string, Book> Books = [];

    public static void Initalize()
    {
        List<Book> books = BackendCommunicator.GetBooks().Result;
        foreach (Book book in books)
            Books[book.Id] = book;
    }

    public static Book? GetBook(string ISBN)
    {
        if(string.IsNullOrEmpty(ISBN))
            return null;
        Books.TryGetValue(ISBN, out Book? potential);
        return potential;
    }
    public static void AddBook(Book book)
    {
        if (book is null || string.IsNullOrEmpty(book.Id))
            throw new NullReferenceException("Book");
        Books[book.Id] = book;
    }

    public static bool RemoveBook(string ISBN)
    {
        return Books.Remove(ISBN);
    }

    public static IEnumerable<Book> AllBooks()
    {
        foreach (KeyValuePair<string, Book> obj in Books)
            yield return obj.Value;
    }

    internal static IEnumerable<Book> GetBooks(List<string> bookISBNs)
    {
        if (bookISBNs is null)
            yield break;
        foreach (KeyValuePair<string, Book> obj in Books)
            if (bookISBNs.Contains(obj.Key))
                yield return obj.Value;
    }
}