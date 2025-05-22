using backend.Models;

namespace backend.Service
{
    public class BookService
    {
        private static List<Book> books = new List<Book>();
        private static int nextId = 1;

        public void CreateBook(Book book)
        {
            if (string.IsNullOrWhiteSpace(book.Title) ||
                string.IsNullOrWhiteSpace(book.Author) ||
                string.IsNullOrWhiteSpace(book.Isbn))
            {
                throw new ArgumentException("Title, Author, and ISBN are required");
            }

            if (books.Any(b => b.Isbn == book.Isbn))
            {
                throw new InvalidOperationException("A book with this ISBN already exists");
            }

            var addBook = new Book
            {
                Id = nextId++,
                Title = book.Title,
                Author = book.Author,
                Isbn = book.Isbn,
                PublicationDate = book.PublicationDate
            };

            books.Add(addBook);
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book? GetBookById(int id)
        {
            return books.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateBook(int id, Book bookUpdate)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                if (string.IsNullOrWhiteSpace(bookUpdate.Title) ||
                    string.IsNullOrWhiteSpace(bookUpdate.Author) ||
                    string.IsNullOrWhiteSpace(bookUpdate.Isbn))
                {
                    throw new ArgumentException("Title, Author, and ISBN are required");
                }

                if (books.Any(b => b.Isbn == bookUpdate.Isbn && b.Id != id))
                {
                    throw new InvalidOperationException("A book with this ISBN already exists");
                }

                book.Title = bookUpdate.Title;
                book.Author = bookUpdate.Author;
                book.Isbn = bookUpdate.Isbn;
                book.PublicationDate = bookUpdate.PublicationDate;
            }
        }

        public void DeleteBook(int id)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                books.Remove(book);
            }
        }
    }
}
