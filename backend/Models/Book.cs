using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
    public class Book
    {
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public DateOnly PublicationDate { get; set; }
    }

    public class BookDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        public DateOnly PublicationDate { get; set; }
    }
}
