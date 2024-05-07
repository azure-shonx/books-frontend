namespace net.shonx.books.frontend.Models;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

public class EditableBook
{
    [DisplayName("ISBN")]
    [RegularExpression(Constants.REGEX_ISBN, ErrorMessage = "Please provide a valid ISBN number.")]
    public string id { get; set; }
    [DisplayName("Title")]
    public string title { get; set; }
    [DisplayName("Comma seperated list of authors")]
    public string authors { get; set; }
    [DisplayName("Publication Year")]

    public string publicationYear { get; set; }
    [DisplayName("Genre")]

    public string genre { get; set; }
    [DisplayName("URL to Cover Image")]
    public string coverImage { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public EditableBook() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public EditableBook(Book book)
    {
        id = book.Id;
        title = book.Title;
        authors = From(book.Authors);
        publicationYear = book.PublicationYear;
        genre = book.Genre;
        coverImage = book.CoverImage;
    }

    private static string From(List<string> list)
    {
        StringBuilder builder = new();
        foreach (var entry in list)
        {
            builder.Append(entry + ",");
        }
        return builder.ToString()[..^1];
    }

    public List<string> AuthorList()
    {
        if (string.IsNullOrEmpty(authors))
            return [];
        return [.. authors.Split(",")];
    }
}