using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ToDoWebApplication.Models
{
    public class Board
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Board Name can't be empty")]
        [StringLength(100)]
        [Remote("NameExists", "Board", AdditionalFields = "Id", ErrorMessage = "Name already exists.")]
        public string Name { get; set; }

        public ICollection<ToDo> ToDos { get; set; }
    }
}
