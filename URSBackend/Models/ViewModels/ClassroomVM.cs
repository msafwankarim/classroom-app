using Newtonsoft.Json;
using System;

namespace URSBackend.Models.ViewModels
{
    public class ClassroomVM : ITransferable
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Instructor { get; set; }
        public string Coursename { get; set; }
        public int StudentCount { get; set; }
        public string Day1 { get; set; }
        public string Day2 { get; set; }
        public String Time1 { get; set; }
        public String Time2 { get; set; }

        public void CopyFrom(object obj)
        {
            ClassroomVM temp = obj as ClassroomVM;
            Id = temp.Id;
            Code = temp.Code;
            Instructor = temp.Instructor; ;
            Coursename = temp.Coursename;
            StudentCount = temp.StudentCount;
            Day1 = temp.Day1;
            Day2 = temp.Day2;
            Time1 = temp.Time1;
            Time2 = temp.Time2;
        }

        public ClassroomVM FromClassroom(Classroom param)
        {
            Id = param.Id;
            Code = param.ClassRoomCode;
            Instructor = param.Instructor;
            
                Coursename = $"{param.ClassCourse.CourseCode} {param.ClassCourse.CourseName}";
                StudentCount = param.StudentsClassrooms == null ? 0 : param.StudentsClassrooms.Count;
            
            Day1 = param.Day1;
            Day2 = param.Day2;
            Time1 = param.Time1.ToShortTimeString();
            Time2 = param.Time2.ToShortTimeString();
            return this;
        }

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}