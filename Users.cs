using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject
{
    internal class Users
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<int> BorrowedBookIDs { get; set; } = new List<int>();

        public bool IsAdmin { get; set; } = false;

        public int MaxBorrowed { get; set; } = 3;

        public void SaveToFile()
        {
            File.AppendAllText("Users.txt", $"Name: {UserName}, Password: {Password}, BorrowedBook: {string.Join(", ", BorrowedBookIDs)}, IsAdmin {IsAdmin}\n");
        }
    }
}
