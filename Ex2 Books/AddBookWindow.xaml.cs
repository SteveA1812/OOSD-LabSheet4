using Ex2_Book;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Ex2_Books
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddBookWindow : Window
    {
        public AddBookWindow()
        {
            InitializeComponent();

            cbxGenre.ItemsSource = new string[] { "Sci-Fi/Fantasy", "Thriller", "True-Crime" };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
           
            string genre = cbxGenre.SelectedItem as string;
            string title = tbxTitle.Text;
            DateTime date = dpDate.SelectedDate.Value;

          
            Book newBk = new Book(title, genre, date);

            //get reference to main window
            MainWindow main = Owner as MainWindow;
                      
            main.Books.Add(newBk);

            //close the window
            this.Close();

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //close the window
            this.Close();
        
        }
    }
}
