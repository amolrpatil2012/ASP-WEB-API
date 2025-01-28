using Microsoft.EntityFrameworkCore;
using MVC.Models;

namespace MVC.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext( DbContextOptions<ApplicationDBContext> options):base(options) { }

        public DbSet<Student> Students { get; set; }    

    }
}
