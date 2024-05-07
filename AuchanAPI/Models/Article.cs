using System.ComponentModel.DataAnnotations;

namespace AuchanAPI.Models
{
    public class Article
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
