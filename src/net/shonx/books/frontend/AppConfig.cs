using System.Diagnostics.CodeAnalysis;

namespace net.shonx.books.frontend;

public static class AppConfig
{
    public static readonly string APIM_KEY = NoNull(Environment.GetEnvironmentVariable("API_KEY"), "API_KEY");

    private static string NoNull([NotNullWhen(false)] string? Value, string Key)
    {
        if (string.IsNullOrEmpty(Value))
            throw new NullReferenceException($"{Key} is not set.");
        return Value;
    }

}