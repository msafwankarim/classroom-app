using Microsoft.EntityFrameworkCore;

namespace URSBackend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        DbSet<Student> students;
        DbSet<Course> courses;
        DbSet<Classroom> classrooms;
        //DbSet<Teacher> teachers;
        DbSet<User> users;

        public DbSet<User> Users { get => users; set => users = value; }

        public DbSet<Student> Students { get => students; set => students = value; }
        public DbSet<Course> Courses { get => courses; set => courses = value; }
        public DbSet<Classroom> Classrooms { get => classrooms; set => classrooms = value; }

        public DbSet<StudentClassroom> StudentsClassrooms { get; set; }
        public DbSet<JoiningRequest> Requests { get; set; }

        //public DbSet<Teacher> Teachers { get => teachers; set => teachers = value; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<StudentClassroom>()
                .HasOne(s => s.StudentProp)
                .WithMany(c => c.StudentsClassrooms)
                .HasForeignKey(si => si.StudentId);
            
            model.Entity<StudentClassroom>()
                .HasOne(s => s.ClassroomProp)
                .WithMany(c => c.StudentsClassrooms)
                .HasForeignKey(si => si.ClassroomId);
        }
    }
}