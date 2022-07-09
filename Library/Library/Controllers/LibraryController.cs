using Library.Helpers;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
    /// <summary>
    /// Controls the interaction of the user with the library.
    /// </summary>
    internal class LibraryController
    {
        #region Variables
        private List<BookModel> _booksInLibrary;
        private ResourcesModel resources = new ResourcesModel();
        #endregion

        #region Constructors
        internal LibraryController()
        {
            _booksInLibrary = new List<BookModel>
            {
                new BookModel{ BookId = 1, Name = "Adventures of Charlie", ISBN = 3312, Fee = 12.00 },
                new BookModel{ BookId = 2, Name = "Adventures of Alexander", ISBN = 4123, Fee = 12.00 },
                new BookModel{ BookId = 3, Name = "Adventures of Christian", ISBN = 5345, Fee = 12.00 },
                new BookModel{ BookId = 4, Name = "Adventures of Simona", ISBN = 5672, Fee = 12.00 },
                new BookModel{ BookId = 5, Name = "Adventures of Iulian", ISBN = 7821, Fee = 12.00 },
                new BookModel{ BookId = 6, Name = "Adventures of Mihai", ISBN = 7856, Fee = 12.00 }
            };

        }
        internal LibraryController(List<BookModel> books)
        {
            if(books.Count > 0)
            {
                foreach (BookModel book in books)
                {
                    _booksInLibrary.Add(book);
                }
            }
        }
        #endregion

        #region Get Total Book Count
        /// <summary>
        /// Gets Total Book Count.
        /// </summary>
        /// <returns>The number of books in the library.</returns>
        internal int GetTotalBookCount()
        {
            return _booksInLibrary.Count;
        }
        #endregion

        #region Get Book Count By Name
        /// <summary>
        /// Displays the amount of books in the library with that name.
        /// </summary>
        /// <param name="bookName">The name of the requested book.</param>
        internal void GetBookCountByName(string bookName)
        {
            if (_booksInLibrary.FirstOrDefault(x => x.Name == bookName).Count > 0)
            {
                Console.WriteLine($"There are { _booksInLibrary.FirstOrDefault(x => x.Name == bookName).Count } {bookName} books in the library.");
                return;
            }
            Console.WriteLine($"There are no { bookName } books.");
            return;
        }
        #endregion

        #region Get All Books In Library
        /// <summary>
        /// Display all the books in the library.
        /// </summary>
        internal void GetAllBooksInLibrary()
        {
            if (_booksInLibrary.Count <= 0)
            {
                Console.WriteLine("There are no books in the library yet.");
            }
            else
            {
                foreach (BookModel book in _booksInLibrary)
                {
                    if(book.Count > 0)
                    {
                    Console.WriteLine($"Book name is: ({book.BookId}){book.Name} x{book.Count} \n" +
                                      $"Book ISBN is: {book.ISBN} \n" +
                                      $"Book price is: {book.Fee} \n");
                    }
                }
            }

        }
        #endregion

        #region Get All Rented Books
        /// <summary>
        /// Display all the currently rented books and useful information.
        /// </summary>
        internal void GetAllRentedBooks()
        {
            if (!_booksInLibrary.Any(x => x.RentedAmount > 0))
            {
                Console.WriteLine("No books rented yet.");
            }
            else
            {
                foreach (BookModel book in _booksInLibrary)
                {
                    if (book.RentedAmount > 0)
                    {
                        Console.WriteLine($"Book name is: ({book.BookId}){book.Name} \n" +
                                          $"Left in library: {book.Count} \n" +
                                          $"Rented amount: {book.RentedAmount} \n" +
                                          $"Days Rented: {book.DaysRented}");
                    }
                }
            }
        }
        #endregion

        #region Add Book
        /// <summary>
        /// Adds a book to the library.
        /// </summary>
        internal void AddBook()
        {
            #region build book and check for existing
            BookModel newBook = new BookModel();

            Console.WriteLine("Enter book name: ");
            newBook.Name = Console.ReadLine();

            #region Check if book exists
            if (_booksInLibrary.FirstOrDefault(x => x.Name == newBook.Name) != null)
            {
                _booksInLibrary.FirstOrDefault(x => x.Name == newBook.Name).Count++;
                Console.WriteLine("Book already exists, an additional copy was registered!");
                return;
            }
            #endregion

            newBook.BookId = _booksInLibrary.Last().BookId + 1;
            Console.WriteLine("Enter book ISBN: (only numbers)");
            newBook.ISBN = StringHelper.ReadIntInput(Console.ReadLine());
            Console.WriteLine("Enter book daily rental fee: (only numbers)");
            newBook.Fee = StringHelper.ReadIntInput(Console.ReadLine());

            _booksInLibrary.Add(newBook);
            Console.WriteLine("Book succesfully added!");
            return;
            #endregion
        }
        #endregion

        #region Return Book
        /// <summary>
        /// Returns one rented book by name.
        /// </summary>
        /// <param name="bookName">the name of the book to be returned to the library.</param>
        internal void ReturnBook(string bookName)
        {
            var targetBook = _booksInLibrary.FirstOrDefault(x => x.Name == bookName);
            if (targetBook.RentedAmount > 0)
            {
                targetBook.RentedAmount--;
                targetBook.Count++;
                if (targetBook.RentedAmount == 0)
                {
                    #region Return book and calculate amount to charge / penalty.
                    double chargedFee;
                    if (targetBook.DaysRented > 14) // swap with enum
                    {
                        chargedFee = targetBook.Fee * targetBook.DaysRented + 0.01 * targetBook.DaysRented - 14;
                        ResourcesController.PayFee(ref resources, chargedFee);
                    }
                    else
                    {
                        chargedFee = targetBook.Fee * targetBook.DaysRented;
                        ResourcesController.PayFee(ref resources, chargedFee);
                    }
                    Console.WriteLine($"You have returned every rented book, you have been charged for { chargedFee }" +
                        $"Current Balance: { resources.Money } \n"); 
                    #endregion
                }
                Console.WriteLine($"You have successfully returned { bookName }, there are { targetBook.Count } books left in the library, and { targetBook.RentedAmount } in your posession.");
                return;
            }
            Console.WriteLine($"There was an error, you can't return book { bookName }.");
            return;
        }
        #endregion

        #region Rent Book
        /// <summary>
        /// Rent a book by name.
        /// </summary>
        /// <param name="bookName">The name of the book to rent.</param>
        internal void RentBook(string bookName)
        {
            var targetBook = _booksInLibrary.FirstOrDefault(x => x.Name == bookName);
            if (targetBook != null && targetBook.Count > 0)
            {
                targetBook.RentedAmount++;
                targetBook.Count--;
                Console.WriteLine($"You have successfully rented { bookName }, there are { targetBook.Count } books left.");
                return;
            }
            Console.WriteLine($"There was an error, you can't rent book { bookName }");
            return;
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Library Helpers.
        /// </summary>

        #region Pass Day
        internal void PassDay()
        {
            foreach (BookModel book in _booksInLibrary)
            {
                if (book.RentedAmount > 0)
                {
                    book.DaysRented += 1;
                }
            }
            resources.Day = ResourcesController.PassDay(resources.Day);
            Console.WriteLine($"The day has passed, current day: { resources.Day }.");
        }  
        #endregion

        #endregion
    }
}
