using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using URSBackend.Models.ViewModels;

namespace URSBackend.Models
{
    public class Classroom : ITransferable
    {
        public int Id { get; set; }
        
        public string ClassRoomCode { get; set; }
        public Course ClassCourse { get; set; }
        public string Instructor { get; set; }
        public List<StudentClassroom> StudentsClassrooms { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public DateTime Time1 { get; set; }
        public DateTime Time2 { get; set; }
        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void CopyFrom(object obj)
        {
            Classroom classroom = obj as Classroom;
            Id = classroom.Id;
            Instructor = classroom.Instructor;
            ClassRoomCode = classroom.ClassRoomCode;
            if (classroom.ClassCourse != null)
            {
                ClassCourse =
                classroom.ClassCourse;
            }
            Day1 = classroom.Day1;
            Day2 = classroom.Day2;
            Time1 = classroom.Time1;
            Time2 = classroom.Time2;
            
        }

        public ClassroomVM GetViewModel()
        {
            return new ClassroomVM().FromClassroom(this);
        }
    }
}