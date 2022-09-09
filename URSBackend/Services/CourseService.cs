using URSBackend.Models;
using System.Linq;
using System.Collections.Generic;

namespace URSBackend.Services
{
    public class CourseService
    {
        private ApplicationDbContext _context;

        public CourseService(ApplicationDbContext context) => _context = context;

        public Course AddCourse(Course course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
            return course;
        }

        public List<Course> GetAllCourses() => _context.Courses.ToList<Course>();


        public Course GetCourse(string courseCode)
            => _context.Courses.FirstOrDefault(n => n.CourseCode == courseCode);

        public Course UpdateCourse(string courseCode, Course course)
        {
            var _course = _context.Courses.FirstOrDefault(n => n.CourseCode == courseCode);
            if (_course != null)
            {
                _course.CopyFrom(course);
                _context.SaveChanges();
            }
            return _course;
        }

        public bool RemoveCourse(string id)
        {
            var _course = _context.Courses.FirstOrDefault(n => n.CourseCode == id);
            if (_course != null)
            {
                _context.Courses.Remove(_course);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

    }
}