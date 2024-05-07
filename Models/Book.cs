namespace net.shonx.books.frontend.Models;

using System.Collections.Generic;
using Newtonsoft.Json;

public class Book : IJsonable
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "Title")]
    public string Title { get; set; }
    [JsonProperty(PropertyName = "authors")]
    public List<string> Authors { get; set; }
    [JsonProperty(PropertyName = "publication_year")]
    public string PublicationYear { get; set; }
    [JsonProperty(PropertyName = "genre")]
    public string Genre { get; set; }
    [JsonProperty(PropertyName = "cover-image")]
    public string CoverImage { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Book()
    {
        // Json will fix it, don't worry...
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    public Book(EditableBook book)
    {
        Id = book.id ?? throw new NullReferenceException();
        Title = book.title ?? throw new NullReferenceException();
        Authors = book.AuthorList();
        PublicationYear = book.publicationYear ?? throw new NullReferenceException();
        Genre = book.genre ?? throw new NullReferenceException(); ;
        CoverImage = book.coverImage ?? throw new NullReferenceException(); ;
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Book o)
            return false;
        return o.Id.Equals(Id) &&
            o.Title.Equals(Title) &&
            EqualityComparer.ListsEqual(o.Authors, Authors) &&
            o.PublicationYear.Equals(PublicationYear) &&
            o.Genre.Equals(Genre) &&
            o.CoverImage.Equals(CoverImage);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode() ^
            Title.GetHashCode() ^
            Authors.GetHashCode() ^
            PublicationYear.GetHashCode() ^
            Genre.GetHashCode() ^
            CoverImage.GetHashCode();
    }
}