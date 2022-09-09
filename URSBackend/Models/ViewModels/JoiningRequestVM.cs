using Newtonsoft.Json;
using System;

namespace URSBackend.Models.ViewModels
{
    public class JoiningRequestVM : ITransferable
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string ClassCode { get; set; }
        public string CourseName { get; set; }
        public RequestStatus Status { get; set; }
        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public void CopyFrom(object obj)
        {
            JoiningRequestVM requestVM = obj as JoiningRequestVM;
            Id = requestVM.Id;
            StudentId = requestVM.StudentId;
            StudentName = requestVM.StudentName;
            ClassCode = requestVM.ClassCode;
            CourseName = requestVM.CourseName;
            Status = requestVM.Status;
        }

        public JoiningRequestVM()
        {

        }
        public JoiningRequestVM(JoiningRequest request)
        {
            Id = request.Id;
            StudentId = request.Client.Id;
            StudentName = request.Client.Name;
            ClassCode = request.TargetClassroom.ClassRoomCode;
            CourseName = request.TargetClassroom.ClassCourse.CourseName;
            Status = request.Status;
            
        }
    }
}