using Ex2_Book;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Ex2_Books
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Book> Books = new ObservableCollection<Book>();

        ObservableCollection<Book> matchingBooks = new ObservableCollection<Book>();

        string[] Genres = { "Sci-Fi/Fantasy", "Thriller", "True-Crime" };
        string[] TitleWords = {"Mercury", "Venus", "Mars",
                "Earth", "Jupiter",  "Saturn", "Uranus", "Neptune", "Pluto","Tadpole","Pinwheel","Milky Way","Andromeda", "Sword", "Dragon", "Killer","Detective", "Cop",};
        string[] StartTitle = { "A", "The", "On" };

       

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Random randomFactory = new Random();


           
            Book b1 = new Book("Death in Paradise","True-Crime" ,new DateTime(2018, 1, 15));
            Book b2 = GetRandomBook(randomFactory);
            Book b3 = GetRandomBook(randomFactory);
            Book b4 = GetRandomBook(randomFactory);
            Book b5 = GetRandomBook(randomFactory);

           
            Books.Add(b1);
            Books.Add(b2);
            Books.Add(b3);
            Books.Add(b4);
            Books.Add(b5);

           
            lbxBooks.ItemsSource = Books;

            decimal total = Books.Count();
            tblkTotal.Text = string.Format("{0}", total);

           
            Array.Sort(Genres);
            cbxFilter.ItemsSource = Genres;



        }

        //generate random Book
        private Book GetRandomBook(Random randomFactory)
        {
            Random rf = new Random();
            int random1 = randomFactory.Next(0, 3);
            string ran1 = StartTitle[random1];
            int random2= randomFactory.Next(0, 18);
            string ran2 = TitleWords[random2];
            int random3 = randomFactory.Next(0, 18);
            string ran3 = TitleWords[random3];
            int randNumber4 = randomFactory.Next(0, 3);
            string randomGenre = Genres[randNumber4];
            string randomTitle = $"{ran1} {ran2} {ran3}";
              


            

            DateTime randomDate = DateTime.Now.AddDays(-randomFactory.Next(0, 32));

            Book randomBook = new Book(randomTitle, randomGenre, randomDate);

            return randomBook;
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            
            Book selectedBook = lbxBooks.SelectedItem as Book;

            if (selectedBook != null)
            {


                Books.RemoveAt(Books.Count -1 );

                decimal total = Books.Count ;
                tblkTotal.Text = string.Format("{0}", total);
            }

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            


            AddBookWindow addBk = new AddBookWindow();
            addBk.Owner = this;
            addBk.ShowDialog();

        }

        //Searches for Book by Title
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //read info from screen - what is user looking for
            string searchTerm = tbxSearch.Text;

            if (!String.IsNullOrEmpty(searchTerm))
            {
                //clear Books to that blank at start of every search
                matchingBooks.Clear();

                //search collection of Books for matches
                foreach (Book book in Books)
                {
                    string BookTitle = book.Title;

                    if (BookTitle.Equals(searchTerm))
                    {
                        matchingBooks.Add(book);
                    }
                }

                //display matches on screen
                lbxBooks.ItemsSource = matchingBooks;
            }



        }

        //shows all Books, used after a search to see everything, remove filters
        private void btnShowAll_Click(object sender, RoutedEventArgs e)
        {
            lbxBooks.ItemsSource = Books;
        }

        //filter by genre of Book
        private void cbxFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //determine what the user selected
            string selectedBookGenre = cbxFilter.SelectedItem as string;

            if (selectedBookGenre != null)
            {
                //clear search results
                matchingBooks.Clear();

                //search in the collection
                foreach (Book book in Books)
                {
                    //find match
                    string bookGenre = book.Genre;

                    if (bookGenre.Equals(selectedBookGenre))
                    {
                        //add match to search results
                        matchingBooks.Add(book);
                    }
                }

                //update display
                lbxBooks.ItemsSource = matchingBooks;
            }

        }

        //save Book objects to JSON
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //get string of objects - json formatted
            string json = JsonConvert.SerializeObject(Books, Formatting.Indented);

            //write that to file
            using (StreamWriter sw = new StreamWriter(@"c:\users\TEMP\Books.json"))
            {
                sw.Write(json);
            }
        }

        //loads json file
        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //connect to a file
            using (StreamReader sr = new StreamReader(@"c:\users\TEMP\Books.json"))
            {
                //read text
                string json = sr.ReadToEnd();

                //convert from json to objects
                Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);

                //refresh the display
                lbxBooks.ItemsSource = Books;
            }


        }
    }
}
