namespace net.shonx.books.frontend;

public static class Constants
{
    public const string REGEX_ISBN = @"^(?:ISBN(?:-13)?:?\ )?(?=[0-9]{13}$|(?=(?:[0-9]+[-\ ]){4})[-\ 0-9]{17}$)97[89][-\ ]?[0-9]{1,5}[-\ ]?[0-9]+[-\ ]?[0-9]+[-\ ]?[0-9]$";
    public const string REGEX_ALPHANUMERIC = @"^[A-Za-z0-9-]+$";
    public const string APIM_URL = "https://apim-books.azure-api.net/";
    public const string BOOK = "books";
    public const string LIST = "lists";
}