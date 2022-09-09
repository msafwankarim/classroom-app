using URSBackend.Models;
using System.Linq;
using System.Collections.Generic;
using URSBackend.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace URSBackend.Services
{
    public class JoiningRequestService
    {
        private ApplicationDbContext _context;

        public JoiningRequestService(ApplicationDbContext context) => _context = context;

        public JoiningRequest AddRequest(JoiningRequestVM requestVM)
        {
            var student = _context.Students.Include(e=> e.StudentsClassrooms).FirstOrDefault(x => requestVM.StudentId == x.Id);
            var classroom = _context.Classrooms.Include(e=> e.StudentsClassrooms).FirstOrDefault(x => x.ClassRoomCode == requestVM.ClassCode);

            if (classroom == null)
                return null;

            var request = new JoiningRequest()
            {
                Status = RequestStatus.PENDING,
                Client = student,
                TargetClassroom = classroom
            };
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }

        public JoiningRequestVM ApproveRequest(int id)
        {
            
            var request = _context.Requests.Include(x=> x.Client).Include(x=> x.TargetClassroom.ClassCourse).FirstOrDefault(x => x.Id == id);
            var student = request.Client;
            var classroom = request.TargetClassroom;

            var studentObj = new StudentClassroom()
            {
                StudentId = student.Id,
                ClassroomId = classroom.Id
            };

            _context.StudentsClassrooms.Add(studentObj);
            request.Status = RequestStatus.APPROVED;
            _context.SaveChanges();
            return new JoiningRequestVM(request);
        }

        public JoiningRequestVM RejectRequest(int id)
        {
            var request = _context.Requests.Include(x => x.Client).Include(x => x.TargetClassroom.ClassCourse).FirstOrDefault(x => x.Id == id); ;
            request.Status = RequestStatus.REJECTED;
            _context.SaveChanges();
            return new JoiningRequestVM(request);
        }

        public List<JoiningRequestVM> GetAll()
        {
            var requests = _context.Requests.Include(x => x.Client).Include(x => x.TargetClassroom)
                .Include(x=> x.TargetClassroom.ClassCourse).ToList();
            var requestVMs = new List<JoiningRequestVM>();
            foreach (var request in requests)
                requestVMs.Add(new JoiningRequestVM(request));
            return requestVMs;
        }

        public List<JoiningRequestVM> GetNotifications(int id)
        {
            var requests = _context.Requests.Include(x=> x.Client).Include(x=> x.TargetClassroom.ClassCourse).Where(x=> x.Client.Id == id && x.Status != RequestStatus.PENDING).ToList();
            List<JoiningRequestVM> joinings = new List<JoiningRequestVM>();
            foreach(var request in requests)
            {
                joinings.Add(new JoiningRequestVM(request));
            }
            return joinings;
        }
    }
}