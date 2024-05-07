// namespace net.shonx.test;

// using net.shonx.books.frontend.Data;
// using net.shonx.books.frontend.Models;


// internal static class TestBackend
// {

//     private static readonly Book BOOK = new(new()
//     {
//         id = "9780545139120",
//         title = "Among the Enemy (Shadow Children)",
//         authors = "Margaret Peterson Haddix",
//         publicationYear = "2005",
//         genre = "Mystery",
//         coverImage = "https://images.isbndb.com/covers/91/20/9780545139120.jpg"
//     });
//     private static readonly Book NEW_BOOK = new(new()
//     {
//         id = "9780545139120",
//         title = "Among the Enemy (Shadow Children)",
//         authors = "Margaret Peterson Haddix",
//         publicationYear = "2008",
//         genre = "Mystery",
//         coverImage = "https://images.isbndb.com/covers/91/20/9780545139120.jpg"
//     });
//     private static readonly List LIST = new(new()
//     {
//         id = "Sydney",
//         owner = "azure@shonx.net",
//         books = ["9780545139120"],
//         subscribers = []
//     });
//     private static readonly List NEW_LIST = new(new()
//     {
//         id = "Sydney",
//         owner = "azure@shonx.net",
//         books = [],
//         subscribers =  []
//     });

//     internal static void Run()
//     {
//         {
//             CreateBook().Wait();
//             Console.WriteLine("[OK] Create Book");
//         }
//         if (!GetBook().Result)
//         {
//             Console.WriteLine("(Book) Get mismatch.");
//             return;
//         }
//         else
//         {
//             Console.WriteLine("[OK] Get Book");
//         }
//         if (!GetBooks().Result)
//         {
//             Console.WriteLine("(Book) All count mismatch.");
//             return;
//         }
//         else
//         {
//             Console.WriteLine("[OK] Get Books");
//         }
//         if (!UpdateBook().Result)
//         {
//             Console.WriteLine("(Book) Update mismatch.");
//             return;
//         }
//         else
//         {
//             Console.WriteLine("[OK] Update Book");
//         }
//         {
//             DeleteBook().Wait();
//             Console.WriteLine("[OK] Delete Book");
//         }

//         {
//             CreateList().Wait();
//             Console.WriteLine("[OK] Create List");
//         }
//         if (!GetList().Result)
//         {
//             Console.WriteLine("(List) Get mismatch.");
//             return;
//         }
//         else
//         {
//             Console.WriteLine("[OK] Read List");
//         }
//         if (!GetLists().Result)
//         {
//             Console.WriteLine("(List) All count mismatch.");
//             return;
//         }
//         else
//         {
//             Console.WriteLine("[OK] Read Lists");
//         }
//         if (!UpdateList().Result)
//         {
//             Console.WriteLine("(List) Update mismatch.");
//             return;
//         }
//         else
//         {
//             Console.WriteLine("[OK] Update List");
//         }
//         {
//             DeleteList().Wait();
//             Console.WriteLine("[OK] Delete List");
//         }
//     }

//     private static async Task CreateBook()
//     {
//         await BackendCommunicator.CreateBook(BOOK);
//     }
//     private static async Task<bool> GetBook()
//     {
//         Book? book = await BackendCommunicator.GetBook(BOOK.Id);
//         if (book is null)
//             return false;
//         return BOOK.Equals(book);
//     }
//     private static async Task<bool> GetBooks()
//     {
//         List<Book> books = await BackendCommunicator.GetBooks();
//         return books.Count == 1;
//     }
//     private static async Task<bool> UpdateBook()
//     {
//         await BackendCommunicator.UpdateBook(NEW_BOOK);
//         Book? book = await BackendCommunicator.GetBook(NEW_BOOK.Id);
//         return NEW_BOOK.Equals(book);
//     }
//     private static async Task DeleteBook()
//     {
//         await BackendCommunicator.DeleteBook(NEW_BOOK.Id);
//     }

//     private static async Task CreateList()
//     {
//         await BackendCommunicator.CreateList(LIST);
//     }
//     private static async Task<bool> GetList()
//     {
//         List? list = await BackendCommunicator.GetList(LIST.Id);
//         return LIST.Equals(list);
//     }
//     private static async Task<bool> GetLists()
//     {
//         List<List> lists = await BackendCommunicator.GetLists();
//         return lists.Count == 1;
//     }
//     private static async Task<bool> UpdateList()
//     {
//         await BackendCommunicator.UpdateList(NEW_LIST);
//         List? list = await BackendCommunicator.GetList(NEW_LIST.Id);
//         return NEW_LIST.Equals(list);
//     }
//     private static async Task DeleteList()
//     {
//         await BackendCommunicator.DeleteList(NEW_LIST.Id);
//     }
// }