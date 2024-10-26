using Microsoft.EntityFrameworkCore;

namespace MvcExercise.Models
{
    public class MvcExerciseDbContext:DbContext
    {
        public MvcExerciseDbContext(DbContextOptions<MvcExerciseDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<SavedJobs> SavedJobs { get; set; }
        public virtual DbSet<AppliedJobs> AppliedJobs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=priyanka;Initial Catalog=mvcExcercise;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
