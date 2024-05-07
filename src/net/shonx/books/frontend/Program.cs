namespace net.shonx.books.frontend;

using net.shonx.books.frontend.web;
// using net.shonx.test;

public class Program
{
    public static void Main(string[] args)
    {
        WebHandler web = new(args);
        web.Run();
    }
}