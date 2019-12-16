using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DesignPatterns_Proxy
{
    class Program
    {
        //original source https://metanit.com/sharp/patterns/4.5.php
        static void Main(string[] args)
        {
            Console.WriteLine("Design Patterns - Proxy!");

            ILibrary libraryRepository = new LibraryRepositoryProxy();
            libraryRepository.Get("War and Peace");
            libraryRepository.Get("Bible");
            libraryRepository.Get("American Tragedy");
        }
    }

    class TownLibraryRepository : ILibrary
    {
        private List<Book> _townLibrary;

        public TownLibraryRepository()
        {
            _townLibrary = new List<Book>()
            {
                new Book("L.N. Tolstoy", "War and Peace"),
                new Book("F.M. Dostoevskiy", "Crime and Punishment")
            };
        }

        public Book Get(string name)
        {
            IEnumerable<Book> bookList;
            bookList = _townLibrary.Where(book => book.Name == name);
            if (bookList.Any())
            {
                Console.WriteLine($"Book {name} has been found in town library.");
                return bookList.First();
            }

            return null;
        }
    }

    class StateLibraryRepository : ILibrary
    {
        private List<Book> _stateLibrary;

        public StateLibraryRepository()
        {
            _stateLibrary = new List<Book>()
            {
                new Book("", "Bible")
            };
        }

        public Book Get(string name)
        {
            IEnumerable<Book> bookList;
            bookList = _stateLibrary.Where(book => book.Name == name);
            if (bookList.Any())
            {
                Console.WriteLine($"Book {name} has been found in state library.");
                return bookList.First();
            }

            return null;
        }
    }

    class LibraryRepositoryProxy : ILibrary
    {
        private StateLibraryRepository _stateLibrary;
        private TownLibraryRepository _townLibrary;
        
        public LibraryRepositoryProxy()
        {
            _stateLibrary = new StateLibraryRepository();
            _townLibrary = new TownLibraryRepository();
        }

        public Book Get(string name)
        {
            Book book;
            book = _townLibrary.Get(name);
            if (book != null)
            {
                return book;
            }
            else
            {
                book = _stateLibrary.Get(name);
                if (book != null)
                {
                    return book;
                }
                else
                {
                    Console.WriteLine($"Book {name} has not been found");
                    return null;
                }
            }
        }
    }



    public interface ILibrary
    {
        Book Get(string name);
    }

    public class Book
    {
        public Book(string author, string name)
        {
            Author = author;
            Name = name;
        }
        public string Author { get; set; }
        public string Name { get; set; }
    }
}
