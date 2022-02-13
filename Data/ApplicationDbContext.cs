using Microsoft.EntityFrameworkCore;
using ToDoWebApplication.Models;

namespace ToDoWebApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Board> Boards { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
    }
}
