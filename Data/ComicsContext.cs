using Microsoft.EntityFrameworkCore;
using PracticandoExamenViernes.Models;

namespace PracticandoExamenViernes.Data
{
    public class ComicsContext:DbContext
    {

        public ComicsContext(DbContextOptions<ComicsContext> options) : base(options) { }

        public DbSet<Comic> comics { get; set; }
    }
}
