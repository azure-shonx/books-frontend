namespace net.shonx.books.frontend.Models;

using System.Collections.Generic;
using Newtonsoft.Json;

public class List : IJsonable
{
    [JsonProperty(PropertyName = "id")]
    public string Id { get; set; }
    [JsonProperty(PropertyName = "owner")]
    public string Owner { get; set; }
    [JsonProperty(PropertyName = "books")]
    public List<string> BookISBNs { get; set; }
    [JsonProperty(PropertyName = "old_books")]
    public List<string> PreviousBookISBNs { get; set; }
    internal IEnumerable<Book> Books { get; set; }
    [JsonProperty(PropertyName = "subscribers")]
    public List<string> Subscribers { get; set; }

    [JsonConstructor]
    public List(string id, string Owner, List<string> BookISBNs, List<string> PreviousBookISBNs, List<string> Subscribers)
    {
        Id = id;
        this.Owner = Owner;
        this.PreviousBookISBNs = PreviousBookISBNs;
        this.BookISBNs = BookISBNs;
        this.Subscribers = Subscribers;
        Books = BookIndex.GetBooks(BookISBNs);

    }

    public List(EditableList list)
    {
        Id = list.id;
        Owner = list.owner;
        BookISBNs = JsonConvert.DeserializeObject<List<string>>(list.books) ?? [];
        PreviousBookISBNs = JsonConvert.DeserializeObject<List<string>>(list.old_books) ?? [];
        Subscribers = JsonConvert.DeserializeObject<List<string>>(list.subscribers) ?? [];
        Books = BookIndex.GetBooks(BookISBNs);
    }



    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }
    public override bool Equals(object? obj)
    {
        if (obj is not List l)
            return false;
        return l.Id.Equals(Id) &&
            l.Owner.Equals(Owner) &&
            EqualityComparer.ListsEqual(l.BookISBNs, BookISBNs) &&
            EqualityComparer.ListsEqual(l.Subscribers, Subscribers);
    }
    public override int GetHashCode()
    {
        return Id.GetHashCode() ^
            Owner.GetHashCode() ^
            BookISBNs.GetHashCode() ^
            Subscribers.GetHashCode();
    }
}