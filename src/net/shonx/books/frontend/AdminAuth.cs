namespace net.shonx.books.frontend;

using net.shonx.jwt;

public class AdminAuth
{
    private const string ADMIN_ID = "Admin";
    public bool IsAdmin { get; }

    public string Email { get; }
    public AdminAuth(bool isAdmin, string email)
    {
        IsAdmin = isAdmin;
        Email = email;
    }

    public AdminAuth(JwtToken token)
    {
        string[]? roles = token.Payload.Roles;
        if (roles is not null)
        {
            IsAdmin = roles.Contains(ADMIN_ID);
        }
        else
        {
            IsAdmin = false;
        }
        if (string.IsNullOrEmpty(token.Payload.Email))
        {
            if (string.IsNullOrEmpty(token.Payload.PreferredUsername))
            {
                if (string.IsNullOrEmpty(token.Payload.UniqueName))
                {
                    throw new NullReferenceException("Unable to get email.");
                }
                Email = token.Payload.UniqueName;
            }
            else
            {
                Email = token.Payload.PreferredUsername;
            }
        }
        else
        {
            Email = token.Payload.Email;
        }
    }
}