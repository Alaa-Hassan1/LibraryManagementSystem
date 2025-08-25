namespace LibraryProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
            ConsoleHelper consoleHelper = new ConsoleHelper();
            consoleHelper.PrintWithColor("Welcome to library", ConsoleColor.Yellow, ConsoleColor.Red);
            while (true)
            {
                Console.WriteLine("Enter your name: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter password: ");
                string password = Console.ReadLine();

                Users currentUser = library.Login(name, password);

                if (currentUser == null)
                {
                    library.AddUsers(name, password);
                    currentUser = library.Login(name, password);
                    consoleHelper.PrintWithColor("New user added", ConsoleColor.Green, ConsoleColor.Black);
                }
                library.CurrentUser = currentUser;
                Console.Clear();
                if (currentUser.IsAdmin)
                {
                    bool exitAdmin = false;
                    while (!exitAdmin)
                    {
                        consoleHelper.PrintWithColor("1. Add Book\n2. Delete Book\n3. Show All Books\n" +
                        "4. Show All Users\n0. Exit", ConsoleColor.Yellow, ConsoleColor.DarkRed);
                        Console.WriteLine("Choose an option : ");
                        Console.ResetColor();
                        string choice = Console.ReadLine();
                        Console.Clear();
                        switch (choice)
                        {
                            case "1":
                                Console.WriteLine("Enter title of the book: ");
                                string title = Console.ReadLine();
                                Console.Write("Enter name of author: ");
                                string author = Console.ReadLine();
                                library.AddBook(title, author);
                                break;
                            case "2":
                                library.DeleteBook();
                                break;
                            case "3":
                                library.ShowAllBooks();
                                break;
                            case "4":
                                library.ShowAllUsers();
                                break;
                            case "0":
                                exitAdmin = true;
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                    }
                }
                else
                {
                    consoleHelper.PrintWithColor($"Welcome {name}", ConsoleColor.Yellow, ConsoleColor.Red);
                    bool exitUser = false;
                    while (!exitUser)
                    {
                        consoleHelper.PrintWithColor("1. Show All Books\n2. Search Book\n3. Borrow Book\n" +
                        "4.Return Book\n0.Exit\n", ConsoleColor.Yellow, ConsoleColor.DarkRed);
                        Console.WriteLine("Enter choice : ");
                        string choice = Console.ReadLine();
                        Console.Clear();
                        switch (choice)
                        {
                            case "1":
                                library.ShowAllBooks();
                                break;
                            case "2":
                                library.Search();
                                break;
                            case "3":
                                library.BorrowBook();
                                break;
                            case "4":
                                library.ReturnBook();
                                break;
                            case "0":
                                exitUser = true;
                                break;
                            default:
                                Console.WriteLine("Invalid option");
                                break;
                        }
                    }
                }
                library.SaveAllUsers();
            }
        }
    }
}
