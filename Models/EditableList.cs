namespace net.shonx.books.frontend.Models;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

public class EditableList(string id, string owner, List<string> books, List<string> old_books, List<string> subscribers) : IJsonable
{
    [RegularExpression(Constants.REGEX_ALPHANUMERIC, ErrorMessage = "Please use only alphanumeric characters and dashes.")]
    public string id { get; set; } = id;

    public string old_id { get; set; } = id;
    public string owner { get; set; } = owner;
    public string books { get; set; } = JsonConvert.SerializeObject(books);
    public string old_books { get; set; } = JsonConvert.SerializeObject(old_books);
    public string subscribers { get; set; } = JsonConvert.SerializeObject(subscribers);

    internal IEnumerable<Book> AllBooks { get; set; } = BookIndex.AllBooks();

    public EditableList() : this("", "", [], [], []) { }

    public EditableList(string Owner) : this("", Owner, [], [], []) { }

    public EditableList(List list) : this(list.Id, list.Owner, list.BookISBNs, list.PreviousBookISBNs, list.Subscribers) { }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    public bool NameChanged()
    {
        if(string.IsNullOrEmpty(id) || string.IsNullOrEmpty(old_id))
            throw new NullReferenceException();
        return !id.Equals(old_id);
    }
}