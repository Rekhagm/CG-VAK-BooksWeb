
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CGVakBooks.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [Range(1,100, ErrorMessage="The Order display value in range of 1 to 100")]
        public int? OrderOfDisplay { get; set; }


        public DateTime? DateCreated { get; set; } = DateTime.Now;
    }
}
