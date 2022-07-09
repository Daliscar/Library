using System;
using Library.Controllers;
using Library.Enums;
using Library.Helpers;

namespace Library
{
    public class Program
    {
        /// <summary>
        /// Entry Point
        /// </summary>
        static void Main(string[] args)
        {

            #region init
            StringHelper.GreetingMessage();
            StateManager.SetState(StateEnum.ActiveState);
            LibraryController library = new LibraryController();
            var command = Console.ReadLine();
            #endregion

            #region Read Commands
            while (StateManager.GetState() != (int)StateEnum.QuitState)
            {
                switch (command.ToUpper())
                {
                    case "LIBRARY":
                        library.GetAllBooksInLibrary();
                        command = Console.ReadLine();
                        break;
                    case "RENTED":
                        library.GetAllRentedBooks();
                        command = Console.ReadLine();
                        break;
                    case "COMMANDS":
                        StringHelper.GreetingMessage();
                        command = Console.ReadLine();
                        break;
                    case "QUIT":
                        Console.WriteLine($"You may quit now, have a great day!");
                        StateManager.SetState(StateEnum.QuitState);
                        break;
                    case "ADD":
                        library.AddBook();
                        command = Console.ReadLine();
                        break;
                    case "COUNT":
                        Console.WriteLine($"Enter book name: ");
                        library.GetBookCountByName(Console.ReadLine());
                        command = Console.ReadLine();
                        break;
                    case "RENT":
                        Console.WriteLine($"Enter book name: ");
                        library.RentBook(Console.ReadLine());
                        command = Console.ReadLine();
                        break;
                    case "PASSDAY":
                        library.PassDay();
                        command = Console.ReadLine();
                        break;
                    case "RETURN":
                        Console.WriteLine($"Enter book name: ");
                        library.ReturnBook(Console.ReadLine());
                        command = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine($"Unknown Command!");
                        command = Console.ReadLine();
                        break;
                }
            }
            #endregion

        }
    }
}
