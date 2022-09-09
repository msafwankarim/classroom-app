using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URSBackend.Models
{
    public class Course : ITransferable
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        
        public int CreditHours { get; set; }

        public void CopyFrom(object obj)
        {
            Course course = obj as Course;
            CourseCode = course.CourseCode;
            CourseName = course.CourseName;
            CreditHours = course.CreditHours;
        }

       

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        
    }
}