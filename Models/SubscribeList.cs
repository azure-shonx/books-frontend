using Newtonsoft.Json;

namespace net.shonx.books.frontend.Models;

public class SubscribeList(string Id, bool WantsToSubscribe, List List) : IJsonable
{
    public string Id { get; set; } = Id;
    public bool WantsToSubscribe { get; set; } = WantsToSubscribe;
    internal List List { get; set; } = List;
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
    public SubscribeList() : this(default, default, default) { }
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

    public SubscribeList(List List) : this(List.Id, false, List) { }

    public string ToJson() {
        return JsonConvert.SerializeObject(this);
    }
}