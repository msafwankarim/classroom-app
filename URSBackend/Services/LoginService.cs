using URSBackend.Models;
using System.Linq;
using URSBackend.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace URSBackend.Services
{
    public class LoginService
    {
        private ApplicationDbContext _context;

        public LoginService(ApplicationDbContext context) => _context = context;

        public User AdminAuth(UserVM user) => _context.Users.FirstOrDefault(n => n.Username == user.Username && n.Password == user.Password);

        public StudentVM StudentAuth(UserVM user)
        {
            var student = _context.Students
                .Where(n => n.Username == user.Username && n.Password == user.Password)
                .Select(x => new StudentVM()
                {
                    Name = x.Name,
                    Id = x.Id,
                    Classrooms = x.StudentsClassrooms.Select(x => new ClassroomVM()
                    {
                        Id = x.ClassroomProp.Id,
                        Coursename = x.ClassroomProp.ClassCourse.CourseName,
                        Code = x.ClassroomProp.ClassRoomCode,
                        Instructor = x.ClassroomProp.Instructor,
                        StudentCount = x.ClassroomProp.StudentsClassrooms.Count,
                        Day1 = x.ClassroomProp.Day1,
                        Day2 = x.ClassroomProp.Day2,
                        Time1 = x.ClassroomProp.Time1.ToShortTimeString(),
                        Time2 = x.ClassroomProp.Time2.ToShortTimeString()
                    }).ToList()
                }).FirstOrDefault();
            return student;
         
        }
    }
}