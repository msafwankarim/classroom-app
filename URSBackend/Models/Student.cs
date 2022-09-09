using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using URSBackend.Models;
namespace URSBackend.Models
{
    public class Student : User, ITransferable
    {
        
        public string Email { get; set; }
        public string Address { get; set; }
        public int CurrentSemester { get; set; }
        public int CreditHoursPassed { get; set; }
        public List<StudentClassroom> StudentsClassrooms { get; set; }


        public Student() { }

        public override string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void CopyFrom(object obj)
        {
            Student student = obj as Student;
            Username = student.Username;
            Password = student.Password;
            Name = student.Name;
            Email = student.Email;
            Address = student.Address;
            CurrentSemester = student.CurrentSemester;
            CreditHoursPassed = student.CreditHoursPassed;
            StudentsClassrooms = student.StudentsClassrooms;

        }
    }
}