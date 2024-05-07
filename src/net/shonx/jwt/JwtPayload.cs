namespace net.shonx.jwt;

using Newtonsoft.Json;


public class JwtPayload
{
    [JsonProperty(PropertyName = "aud")]
    public string? Audience { get; set; }
    [JsonProperty(PropertyName = "iss")]
    public string? Issuer { get; set; }
    [JsonProperty(PropertyName = "iat")]
    public int IssuedAt { get; set; }
    [JsonProperty(PropertyName = "nbf")]
    public int NotBefore { get; set; }
    [JsonProperty(PropertyName = "exp")]
    public int ExpiresAt { get; set; }
    [JsonProperty(PropertyName = "acct")]
    public int Account { get; set; }
    [JsonProperty(PropertyName = "acr")]
    public string? AuthenticationContextClass { get; set; }
    [JsonProperty(PropertyName = "aio")]
    public string? AIO { get; set; }
    [JsonProperty(PropertyName = "altsecid")]
    public string? AltSecId { get; set; }
    [JsonProperty(PropertyName = "amr")]
    public string[]? AuthenticationMethods { get; set; }
    [JsonProperty(PropertyName = "app_displayname")]
    public string? ApplicationDisplayName { get; set; }
    [JsonProperty(PropertyName = "appid")]
    public string? ApplicationID { get; set; }
    [JsonProperty(PropertyName = "appidacr")]
    public string? ApplicationIDAuthenticationContextClass { get; set; }
    [JsonProperty(PropertyName = "azp")]
    public string? AZP { get; set; }
    [JsonProperty(PropertyName = "azpacr")]
    public string? AZPContextClass { get; set; }
    [JsonProperty(PropertyName = "email")]
    public string? Email { get; set; }
    [JsonProperty(PropertyName = "family_name")]
    public string? LastName { get; set; }
    [JsonProperty(PropertyName = "given_name")]
    public string? FirstName { get; set; }
    [JsonProperty(PropertyName = "idp")]
    public string? IdentityProvider { get; set; }
    [JsonProperty(PropertyName = "idtyp")]
    public string? IDType { get; set; }
    [JsonProperty(PropertyName = "ipaddr")]
    public string? IpAddress { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string? FullName { get; set; }
    [JsonProperty(PropertyName = "oid")]
    public string? ID { get; set; }
    [JsonProperty(PropertyName = "platf")]
    public string? Platform { get; set; }
        [JsonProperty(PropertyName = "preferred_username")]
    public string? PreferredUsername { get; set; }
    [JsonProperty(PropertyName = "puid")]
    public string? PUID { get; set; }
    [JsonProperty(PropertyName = "rh")]
    public string? RH { get; set; }
    [JsonProperty(PropertyName = "roles")]
    public string[]? Roles { get; set; }
    [JsonProperty(PropertyName = "scp")]
    public string? Scopes { get; set; }
    [JsonProperty(PropertyName = "signin_state")]
    public string[]? SignInState { get; set; }
    [JsonProperty(PropertyName = "sub")]
    public string? Subject { get; set; }
    [JsonProperty(PropertyName = "tenant_region_scope")]
    public string? TenantRegion { get; set; }
    [JsonProperty(PropertyName = "tid")]
    public string? TenantID { get; set; }
    [JsonProperty(PropertyName = "unique_name")]
    public string? UniqueName { get; set; }
    [JsonProperty(PropertyName = "uti")]
    public string? UTI { get; set; }
    [JsonProperty(PropertyName = "ver")]
    public string? Version { get; set; }
    [JsonProperty(PropertyName = "wids")]
    public string[]? WorkIDs { get; set; }
    [JsonProperty(PropertyName = "xms_st")]
    public Dictionary<string, string>? XMS_ST { get; set; }
    [JsonProperty(PropertyName = "xms_tcdt")]
    public int TokenCreationDate { get; set; }

    public bool IsExpired()
    {
        int currentTime = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        return currentTime >= ExpiresAt;
    }

    public string ToJSON()
    {
        return JsonConvert.SerializeObject(this);
    }
}