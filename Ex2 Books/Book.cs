using System;

namespace Ex2_Book
{

    public class Book
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public DateTime Published { get; set; }

        public static decimal TotalBooks { get; set; }

        public Book(string category, string amt, DateTime date)
        {
            Title = category;
            Genre = amt;
            Published = date;

            
        }

        public override string ToString()
        {
            return string.Format("{0} {1:C} {2}", Title, Genre, Published.ToShortDateString());
        }


    }
}