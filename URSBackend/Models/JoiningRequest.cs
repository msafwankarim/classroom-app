using System;
using URSBackend.Models.ViewModels;

namespace URSBackend.Models
{
    public enum RequestStatus
    {
        PENDING,
        APPROVED,
        REJECTED
    }
    public class JoiningRequest
    {
        public int Id { get; set; }
        public Student Client { get; set; }
        public Classroom TargetClassroom { get; set; }
        public RequestStatus Status { get; set; }
        public JoiningRequest()
        {

        }   
    }
}