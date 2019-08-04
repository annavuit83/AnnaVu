using System;
using System.Collections;
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
        public List<Book> originalBookList;
        public List<Author> originalAuthorList;
        public MainWindow()
        {
            InitializeComponent();
            originalBookList = new List<Book>();
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

            System.IO.StreamReader fileBook = new System.IO.StreamReader(@"Books.txt");
           
//retrieve book list
            List<Book> bookList = new List<Book>();
             string lineBook = "";
            while ((lineBook = fileBook.ReadLine()) != null)
            {
                string[] textLine = lineBook.Split(',');
                Book book = new Book();
                book.BookID = textLine[0];
                book.AuthorID = textLine[1];
                book.BookTitle = textLine[2];
                book.BookISBN = textLine[3];

                bookList.Add(book);
            }

            for (int i = 0; i < authors.Count; i++)
            {
                List<Book> booksByAuthorList = getListBookByAuthorID(bookList, authors[i].AuthorID);
                authors[i].BookList = booksByAuthorList;

            }

             originalBookList = bookList;

            authorTree.ItemsSource = originalAuthorList;

            //Add Feature 1
        }

        

        private List<Book> getListBookByAuthorID(List<Book> originalList, string authorID)
        {
            List<Book> returnBooks = new List<Book>();

            for (int i = 0; i < originalList.Count; i++)
            {
                if (originalList[i].AuthorID == authorID)
                {
                    returnBooks.Add(originalList[i]);
                }
            }

            return returnBooks;

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

        private void AuthorTree_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if ((((TreeView)sender).SelectedItem).GetType().ToString() == "TreeViewApp1.Author")
            {
                authorTextBox.Text = "";
                bookTitleTextBox.Text = "";
            }

            if ((((TreeView)sender).SelectedItem).GetType().ToString() == "TreeViewApp1.Book")
            {
                bookTitleTextBox.Text = ((Book)((TreeView)sender).SelectedItem).BookTitle.ToString();
            }

        }

        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)

        {

            DependencyObject parent = VisualTreeHelper.GetParent(item);

            while (!(parent is TreeViewItem || parent is TreeView))

            {

                parent = VisualTreeHelper.GetParent(parent);

            }   

            return parent as ItemsControl;

        }
    }



    public class Book
    {
        public string BookID { get; set; }
        public string AuthorID { get; set; }
        public string BookTitle { get; set; }
        public string BookISBN { get; set; }

    }

    public class Author
    {
        public string AuthorID { get; set; }
        public string AuthorName { get; set; }

        public List<Book> BookList { get; set; }

        public IList Children
        {
            get
            {
                return new CompositeCollection()
            {
                new CollectionContainer() { Collection = BookList }
            };
            }
        }

    }
}
