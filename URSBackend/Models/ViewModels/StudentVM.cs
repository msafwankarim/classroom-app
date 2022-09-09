using System.Collections.Generic;

namespace URSBackend.Models.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ClassroomVM> Classrooms{ get; set; }

        public StudentVM(int id, string name, List<ClassroomVM> classrooms)
        {
            Id = id;
            this.Name = name;
            this.Classrooms = classrooms;
        }

        public StudentVM()
        {
        }

    }
}