namespace Library.Models
{
    public class BookModel
    {
        /// <summary>
        /// Id of the book.
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// Name of the book.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// International Standard Book Number.
        /// </summary>
        public int ISBN { get; set; }
        /// <summary>
        /// Daily fee for renting the book.
        /// </summary>
        public double Fee { get; set; }
        /// <summary>
        /// How many copies of this book are currently rented.
        /// </summary>
        public int RentedAmount { get; set; }
        /// <summary>
        /// How many days have passed since renting the first copy of this book.
        /// </summary>
        public int DaysRented { get; set; }
        /// <summary>
        /// The amount of copies inside the library.
        /// </summary>
        public int Count { get; set; } = 1;
    }
}
