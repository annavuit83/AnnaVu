using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookSearchApplication
{
    public partial class Form1 : Form
    {
        private List<Book> originalBookList;
        private List<Author> originalAuthorList;
        

        public Form1()
        {
            InitializeComponent();

            originalBookList = new List<Book>();

            System.IO.StreamReader file =  new System.IO.StreamReader(@"authors.txt");
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
            string lineBook = "";
           
            List<Book> bookList = new List<Book>();
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

            originalBookList = bookList;

            for (int i = 0; i < authors.Count; i++)
            {
                TreeNode treeNode = new TreeNode();
                treeNode.Name = authors[i].AuthorName;
                treeNode.Text = authors[i].AuthorName;

                treeView1.Nodes.Add(treeNode);

                List<Book> booksByAuthorList = getListBookByAuthorID(bookList, authors[i].AuthorID);

                for (int j = 0; j < booksByAuthorList.Count; j++)
                {
                    TreeNode bookNode = new TreeNode();
                    bookNode.Name = booksByAuthorList[j].BookTitle;
                    bookNode.Text = booksByAuthorList[j].BookTitle;

                    treeNode.Nodes.Add(bookNode);
                }



                
            }
             

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

        public class Author
        {
            public string AuthorID { get; set; }
            public string AuthorName { get; set; }
        }

        public class Book
        {
            public string BookID { get; set; }
            public string AuthorID { get; set; }

            public string BookTitle { get; set; }

            public string BookISBN { get; set; }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            if (treeView1.SelectedNode.Level == 1)
            {
                String nodeName = treeView1.SelectedNode.Name;

                Book bookInfo = lookForBookByName(originalBookList, nodeName);

                if (bookInfo != null)
                {
                    textBox1.Text = treeView1.SelectedNode.Parent.Name;
                    textBox2.Text = bookInfo.BookTitle;
                    textBox3.Text = bookInfo.BookISBN;

                    
                }
            }
            

            


            

        }

        private Book lookForBookByName(List<Book> books, string bookName)
        {
            Book returnBook = null;

            for (int i = 0; i < originalBookList.Count; i++)
            {
                if (originalBookList[i].BookTitle == bookName)
                {
                    returnBook = originalBookList[i];
                    break;
                }

            }

            return returnBook;
        }

        private Book lookForBookByISBN(List<Book> books, string ISBN)
        {
            Book returnBook = null;

            for (int i = 0; i < originalBookList.Count; i++)
            {
                if (originalBookList[i].BookISBN == ISBN)
                {
                    returnBook = originalBookList[i];
                    break;
                }

            }

            return returnBook;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Book returnBook = null;
            if (textBox4.Text != null && textBox4.Text != "")
            {
                returnBook = lookForBookByName(originalBookList, textBox4.Text);

                if (returnBook == null)
                {
                    returnBook = lookForBookByISBN(originalBookList, textBox4.Text);
                }

                if (returnBook == null)
                {
                    MessageBox.Show("There is no book match.");
                    return;
                }
                else
                {
                    //textBox2.Text = returnBook.BookTitle;
                    //textBox3.Text = returnBook.BookISBN;

                    for (int i = 0; i < originalAuthorList.Count; i++)
                    {
                        if (originalAuthorList[i].AuthorID == returnBook.BookID)
                        {
                            //textBox1.Text = originalAuthorList[i].AuthorName;

                            for (int k = 0; k < treeView1.Nodes.Count; k++)
                            {
                                if (treeView1.Nodes[k].Text == originalAuthorList[i].AuthorName)
                                {
                                    for (int m = 0; m < treeView1.Nodes[k].Nodes.Count; m++)
                                    {
                                        if (treeView1.Nodes[k].Nodes[m].Text == returnBook.BookTitle)
                                        {
                                            treeView1.SelectedNode = treeView1.Nodes[k].Nodes[m];
                                            treeView1.Select();
                                            treeView1.HideSelection = false;
                                            break;
                                        }
                                    }

                                }
                            }


                           
                        }
                    }
                }
                

            }
        }
    }
}
