namespace net.shonx.jwt;

using System.Text;
using Newtonsoft.Json;

public static class TokenCreator
{
    public static JwtToken? Create(string Token)
    {
        string[] parts = Token.Split(".");
        if (parts.Length != 3)
            return null;

        string? header = Decode(parts[0]);
        string? payload = Decode(parts[1]);
        string signature = parts[2];

        if (string.IsNullOrEmpty(header) || string.IsNullOrEmpty(payload) || string.IsNullOrEmpty(signature))
            return null;


        JwtHeader? Header = JsonConvert.DeserializeObject<JwtHeader>(header);
        JwtPayload? Payload = JsonConvert.DeserializeObject<JwtPayload>(payload);
        if (Header is null || Payload is null)
            return null;

        return new(Token, Header, Payload, signature);
    }

    private static string? Decode(string encoded)
    {
        int remainder = encoded.Length % 4;
        if (remainder != 0)
        {
            encoded += new string('=', 4 - remainder);
        }
        try
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
        }
        catch (FormatException)
        {
            return null;
        }

    }
}