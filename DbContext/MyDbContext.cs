using System.Configuration;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UserCourseEnrollApp.Models;

namespace UserCourseEnrollApp.DbContext
{
    public class MyDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> user { get; set; }

        public  DbSet<Course> course { get; set; }

        public DbSet<EnrollMent> enrollMent { get; set; }

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "AppDataBase.db" };
            //var connectionString = connectionStringBuilder.ToString();
            //var connection = new SqliteConnection(connectionString);

            //optionsBuilder.UseSqlite(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(key => {
                key.Property<int?>("Id");
                key.HasKey("Id");
                });
            modelBuilder.Entity<Course>(key =>
            {

                key.Property<int?>("CourseId");
                key.HasKey("CourseId");
                //key.HasNoKey();
            });

            modelBuilder.Entity<EnrollMent>(key =>
            {
                key.Property<int?>("Id");
                key.HasKey("Id");
                key.Property(x => x.CourseId).HasColumnType("List<int>").IsRequired();                
                //key.HasOne(x => x.Courses).WithMany().HasForeignKey(x => x.CourseId);
                
                //key.HasOne(x => x.Users).WithMany().HasForeignKey(x => x.UserID);

            });

            modelBuilder.Entity<List<Course>>().HasNoKey();
                        
            base.OnModelCreating(modelBuilder);
        }
    }
}
