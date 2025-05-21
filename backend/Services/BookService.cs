using backend.Models;

namespace backend.Service
{
    public class BookService
    {
        private static List<Book> books = new List<Book>();
        private static int nextId = 1;

        public void CreateBook(BookDto bookDto)
        {
            var book = new Book
            {
                Id = nextId++,
                Title = bookDto.Title,
                Author = bookDto.Author,
                Isbn = bookDto.Isbn,
                PublicationDate = bookDto.PublicationDate
            };

            books.Add(book);
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book? GetBookById(int id)
        {
            return books.FirstOrDefault(x => x.Id == id);
        }

        public void UpdateBook(int id, BookDto bookDto)
        {
            var book = books.FirstOrDefault(x => x.Id == id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.Author = bookDto.Author;
                book.Isbn = bookDto.Isbn;
                book.PublicationDate = bookDto.PublicationDate;
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
