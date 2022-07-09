using System;

namespace Library.Helpers
{
    internal static class StringHelper
    {
        #region Read Int Input
        /// <summary>
        /// Validation for integer type input when needed.
        /// </summary>
        /// <param name="input">The recieved string.</param>
        /// <returns></returns>
        internal static int ReadIntInput(string? input)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                if(!string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Please enter a number, or leave blank to submit 0.");
                    return ReadIntInput(Console.ReadLine());
                }
                else
                {
                    Console.WriteLine("Submitted 0.");
                    return 0;
                }
            }
        }
        #endregion

        #region Greeting Message
        /// <summary>
        /// Default message with the Application tutorial.
        /// </summary>
        internal static void GreetingMessage()
        {
            Console.WriteLine($"Greetings, below are the library commands: \n\n" +

                              $"Library: -Lists every book in the library currently- \n" +
                              $"Rented: -Lists every currently rented book- \n" +
                              $"Passday: -Go to the next day- \n\n" +

                              $"Add: -Add a new book to the library- \n" +
                              $"Rent: -Rent a book(by name)- \n" +
                              $"Return: -Return a book(by name)- \n" +
                              $"Count: -retrieves the current count for a certain book(by name)- \n\n" +

                              $"Quit: -quits the application- \n" +
                              $"Commands: -Shows this message again- \n");
        }
        #endregion
    }
}
