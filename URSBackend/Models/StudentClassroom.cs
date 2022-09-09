namespace URSBackend.Models
{
    public class StudentClassroom
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student StudentProp { get; set; }
        public int ClassroomId { get; set; }
        public Classroom ClassroomProp { get; set; }

    }
}