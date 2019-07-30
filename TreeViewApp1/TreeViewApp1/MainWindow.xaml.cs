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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TreeViewApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Book> bookList;
        public List<Author> originalAuthorList;
        public MainWindow()
        {
            InitializeComponent();
            bookList = new List<Book>();
            originalAuthorList = new List<Author>();

            System.IO.StreamReader file = new System.IO.StreamReader(@"authors.txt");
            string line = "";
            int counter = 0;
            List<Author> authors = new List<Author>();
            while ((line = file.ReadLine()) != null)
            {
                string[] textLine = line.Split(',');
                Author author = new Author();
                author.AuthorID = textLine[0];
                author.AuthorName = textLine[1];

                authors.Add(author);
                counter++;
            }

            originalAuthorList = authors;

            authorTree.ItemsSource = originalAuthorList;
        }


        public List<Author> OriginalAuthorList
        {
            get
            {
                return originalAuthorList;
            }
            set
            {
                originalAuthorList = value;
            }
        }
    }



    public class Book
    {
        public string BookID { get; set; }
        public string BookTitle { get; set; }
        public string BookISBN { get; set; }

    }

    public class Author
    {
        public string AuthorID { get; set; }
        public string AuthorName { get; set; }

    }
}
