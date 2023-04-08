using System.ComponentModel.DataAnnotations;

namespace Fall22Demo.Models
{
    [DisplayColumn("Name")]
    public class Parent
    {
        public int ParentId { get; set; }
        [Required]
        public string? Name { get; set; }
        public List<Child>? Children { get; set; }
    }
}
