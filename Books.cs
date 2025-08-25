using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject
{
    internal class Books
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; } = false;
        public void Display()
        {
            Console.WriteLine($"ID : {ID}, Title : {Title}, Author : {Author}, IsBorrowed : {IsBorrowed}");
        }
        public void SaveToFile()
        {
            File.AppendAllText("Library.txt", $"ID : {ID}, Title : {Title}, Author : {Author}, IsBorrowed : {IsBorrowed}\n");
        }
    }
}
