using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoWebApplication.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        [StringLength(100)]
        [Required(ErrorMessage = "Title can't be empty")]
        [Remote("NameExists", "ToDo", AdditionalFields = "Id", ErrorMessage = "Title already exists.")]
        public string Title { get; set; }
        [Display(Name = "Board")]
        public int BoardId { get; set; }
        [Display(Name = "Complete")]
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public Board Board { get; set; }
    }
}
