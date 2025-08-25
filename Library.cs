using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject
{
    internal class Library
    {
        List<Books> books = new List<Books>();
        List<Users> users = new List<Users>();

        int nextId = 1;
        public Users CurrentUser { get; set; }
        public Library()
        {
            AddAdminIfMissing("admin", "admin142");
            AddAdminIfMissing("mohamed", "MH456");
        }
        public string Encoder(string password)
        {
            string buffer1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz 0123456789&é#-_!@%";
            string buffer2 = "yXDwrVIGHcKRfshOkLotTFBWYdm7JZeMgP8jQlaSn96ENtUvCx Az2#034@qbip&é1%-_5!";

            return password = new string(password.Select(c => buffer2[buffer1.IndexOf(c)]).ToArray());
        }
        void AddAdminIfMissing(string username, string password)
        {
            password = Encoder(password);

            if (!users.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)))
                users.Add(new Users { UserName = username, Password = password, IsAdmin = true });
        }        
        public Users Login(string username, string password)
        {
            password = Encoder(password);

            foreach (var user in users)
            {
                if (user.UserName == username && user.Password == password)
                {
                    return user;
                }
            }
            return null;
        }
        public void AddUsers(string username, string password, bool isAdmin = false)
        {

            password = Encoder(password);

            users.Add(new Users { UserName = username, Password = password, IsAdmin = isAdmin });
        }
        public void ShowAllUsers()
        {
            if (users.Count == 0)
            {
                Console.WriteLine("No found untill users yet");
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine($"Username: {user.UserName}, IsAdmin: {user.IsAdmin}");
                if (user.BorrowedBookIDs.Count > 0)
                    Console.WriteLine("Borrowed Books : " + string.Join(", ", user.BorrowedBookIDs));
                else
                    Console.WriteLine("No borrowed book");
                Console.WriteLine();
            }
        }
        public void SaveAllBooks()
        {
            File.WriteAllText("Library.txt", "");
            foreach (var book in books)
            {
                book.SaveToFile();
            }
        }
        public void SaveAllUsers()
        {
            File.WriteAllText("Users.txt", "");
            foreach (var user in users)
            {
                user.SaveToFile();
            }
        }
        public void AddBook(string title, string author)
        {
            books.Add(new Books { ID = nextId++, Title = title, Author = author });
            Console.WriteLine("Book added successfuly!");
        }
        public void ShowAllBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("Library is empty!");
                return;
            }
            foreach (var book in books)
            {
                book.Display();
            }
        }
        public void Search()
        {
            Console.Write("Enter title to search : ");
            string title = Console.ReadLine();
            bool found = false;
            foreach (var book in books)
            {
                if (book.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                {
                    book.Display();
                    found = true;
                }                
            }
            if (!found)
                Console.WriteLine("Not Found");
        }
        public void DeleteBook()
        {
            Console.Write("Enter ID to delete : ");
            int id = int.Parse(Console.ReadLine());
            bool found = false;
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ID == id)
                {
                    books.RemoveAt(i);
                    Console.WriteLine("Book is Removed!");
                    SaveAllBooks();
                    found = true;
                    break;
                }
            }
            if (!found)
                Console.WriteLine("Not Found!");
        }
        public void BorrowBook()
        {
            Console.Write("Enter ID to Borrow : ");
            int id = int.Parse(Console.ReadLine());
            bool found = false;
            foreach (var book in books)
            {
                if (book.ID == id)
                {
                    found = true;
                    if (book.IsBorrowed)
                        Console.WriteLine("The book is aleardy borrowed!");
                    else
                    {
                        if (CurrentUser.BorrowedBookIDs.Count >= CurrentUser.MaxBorrowed)
                        {
                            Console.WriteLine("You reached the maximum number of the books");
                            return;
                        }
                        else
                        {
                            CurrentUser.BorrowedBookIDs.Add(book.ID);
                            book.IsBorrowed = true;
                            Console.WriteLine("The book is borrowed successfuly!");
                            SaveAllBooks();
                            break;
                        }
                    }
                }
            }
            if (!found)
                Console.WriteLine("Not Found!");
        }
        public void ReturnBook()
        {
            Console.Write("Enter ID to return : ");
            int id = int.Parse(Console.ReadLine());
            bool found = false;
            foreach (var book in books)
            {
                if (book.ID == id)
                {
                    found = true;
                    if (!book.IsBorrowed)
                    {
                        Console.WriteLine("The book is not borrowed");
                    }
                    else
                    {
                        if (CurrentUser.BorrowedBookIDs.Contains(book.ID))
                        {
                            book.IsBorrowed = false;
                            CurrentUser.BorrowedBookIDs.Remove(book.ID);
                            Console.WriteLine("Book is returned");
                            SaveAllBooks();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You didn't borrow this book!");
                        }
                    }
                }
            }
            if (!found)
                Console.WriteLine("Not Found");
        }
    }
}
