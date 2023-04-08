using System.ComponentModel.DataAnnotations;

namespace Fall22Demo.Models
{

    public class Child
    {
        public int ChildId { get; set; }

        public string? Name { get; set; }
        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }
    }
}
