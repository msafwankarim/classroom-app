using URSBackend.Models;
using System.Linq;
using System.Collections.Generic;
using URSBackend.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace URSBackend.Services
{
    public class ClassroomService
    {
        private ApplicationDbContext _context;

        public ClassroomService(ApplicationDbContext context) => _context = context;

        public Classroom AddClassroom(Classroom classroom)
        {
            classroom.ClassCourse = _context.Courses.FirstOrDefault(n => n.CourseCode == classroom.ClassCourse.CourseCode);
            _context.Classrooms.Add(classroom);
            _context.SaveChanges();
            return classroom;
        }


        public List<ClassroomVM> GetAllClassrooms()
        {
            List<ClassroomVM> classrooms = new List<ClassroomVM>();
            var list = _context.Classrooms.Include(x => x.ClassCourse).Include(x=> x.StudentsClassrooms);
            foreach(var item in list) { 
                classrooms.Add(item.GetViewModel()); }
            return classrooms;
        }

        public ClassroomVM GetClassroomVM(int id)
        {
            
            var classroomVm = _context.Classrooms.Include(x=>x.ClassCourse).Include(x=>x.StudentsClassrooms).Where(n => n.Id == id)
                .Select(param => new ClassroomVM()
                {
                    Id = param.Id,
                    Code = param.ClassRoomCode,
                    Instructor = param.Instructor,
                    Coursename = $"{param.ClassCourse.CourseCode} {param.ClassCourse.CourseName}",
                    StudentCount = param == null? 0 : param.StudentsClassrooms.Count,
                    Day1 = param.Day1,
                    Day2 = param.Day2,
                    Time1 = param.Time1.ToShortDateString(),
                    Time2 = param.Time2.ToShortDateString()
                }
            ).FirstOrDefault();
            
            return classroomVm;
        }

        public Classroom GetClassroom(int id)
        {

            var classroom = _context.Classrooms.Include(x => x.ClassCourse).Include(x => x.StudentsClassrooms).Where(n => n.Id == id)
                .Select(param => new Classroom()
                {
                    Id = param.Id,
                    ClassRoomCode = param.ClassRoomCode,
                    Instructor = param.Instructor,
                    ClassCourse = param.ClassCourse,
                    StudentsClassrooms = param.StudentsClassrooms ,
                    Day1 = param.Day1,
                    Day2 = param.Day2,
                    Time1 = param.Time1,
                    Time2 = param.Time2
                }
            ).FirstOrDefault();

            return classroom;
        }
        public Classroom UpdateClassroom(int id, Classroom classroom)
        {
            var temp = _context.Classrooms.Include(x => x.ClassCourse).Include(x => x.StudentsClassrooms).FirstOrDefault(n => n.Id == id);
            
            if (temp != null)
            {
                temp.CopyFrom(classroom);
                temp.ClassCourse = _context.Courses.FirstOrDefault(n => n.CourseCode == classroom.ClassCourse.CourseCode);
                _context.SaveChanges();
            }
            return classroom;
        }

        public bool RemoveClassroom(int id)
        {
            var classroom = _context.Classrooms.FirstOrDefault(n => n.Id == id);
            if (classroom != null)
            {
                _context.Classrooms.Remove(classroom);
                _context.SaveChanges();
                return true;
            }
            else return false;
        }

    }
}