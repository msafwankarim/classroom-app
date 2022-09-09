using URSBackend.Models;
using System.Linq;
using System.Collections.Generic;
using URSBackend.Models.ViewModels;
using System;

namespace URSBackend.Services
{
    public class StudentService
    {
        private ApplicationDbContext _context;

        public StudentService(ApplicationDbContext context) => _context = context;

        public Student AddStudent(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return _context.Students.ToList().Last();
        }

        public List<Student> GetAllStudents() => _context.Students.ToList<Student>();
        
        //public List<ClassroomVM> GetTodayClasses(int id)
        //{
        //    var classrooms = _context.Students.Where(x=> x.Id == id).Select(n=> new StudentVM().Classrooms[0].)
        //        List<ClassroomVM> vms = new List<ClassroomVM>();
        //    foreach (var room in classrooms)
        //        vms.Add(room.GetViewModel());
        //    return vms;
        //}

        public Student GetStudent(int id)
            =>  _context.Students.FirstOrDefault(n => n.Id == id);

        public Student UpdateStudent(int id, Student student)
        {
            var _student = _context.Students.FirstOrDefault(n => n.Id == id);
            if (_student != null)
            {
                _student.CopyFrom(student);
                _context.SaveChanges();
            }
            return _student;
        }

        public bool RemoveStudent(int id)
        {
            var _student = _context.Students.FirstOrDefault(n => n.Id == id);
            if (_student != null)
            {
                _context.Students.Remove(_student);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }
        
    }
}