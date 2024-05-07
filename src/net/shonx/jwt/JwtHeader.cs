namespace net.shonx.jwt;

using Newtonsoft.Json;

public class JwtHeader
{
    [JsonProperty(PropertyName = "typ")]
    public string? Type { get; set; }
    [JsonProperty(PropertyName = "nonce")]
    public string? Nonce { get; set; }
    [JsonProperty(PropertyName = "alg")]
    public string? Algorithm { get; set; }
    [JsonProperty(PropertyName = "x5t")]
    public string? X5T { get; set; }
    [JsonProperty(PropertyName = "kid")]
    public string? KeyIdentifier { get; set; }

    public string ToJSON()
    {
        return JsonConvert.SerializeObject(this);
    }
}
