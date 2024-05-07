namespace net.shonx.books.frontend;

using Microsoft.AspNetCore.Mvc.Razor;

internal static class Util
{
    internal static bool IsAdmin<T>(RazorPage<T> page)
    {
        AdminAuth? auth = page.ViewData["AdminAuth"] as AdminAuth;
        if (auth is not null)
            return auth.IsAdmin;
        throw new NullReferenceException("AdminAuth");
    }
}