using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URSBackend.Models;
using URSBackend.Services;

namespace URSBackend.Controllers
{

    [Route("api/classrooms")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private ClassroomService ClassroomServiceProp { get; set; }

        public ClassroomController(ClassroomService service)
        {
            ClassroomServiceProp = service;
        }


        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(ClassroomServiceProp.GetAllClassrooms());
        }


        [HttpGet("get/{id}")]
        public IActionResult Get(int id)
        {
            return Ok(ClassroomServiceProp.GetClassroomVM(id));
        }

        [HttpGet("get-full/{id}")]
        public IActionResult GetFull(int id)
        {
            return Ok(ClassroomServiceProp.GetClassroom(id));
        }


        [HttpPost]
        public IActionResult AddClassroom([FromBody] Classroom classroom)
        {
            System.Diagnostics.Debug.WriteLine(Request.Body);
            Classroom temp = ClassroomServiceProp.AddClassroom(classroom);

            return Ok(temp);
        }

        [HttpPut("update-classroom/{id}")]
        public IActionResult UpdateClassroom(int id, [FromBody] Classroom classroom)
        {
            var temp = ClassroomServiceProp.UpdateClassroom(id, classroom);
            if (temp != null)
                return Ok(temp);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteClassroom(int id)
        {
            if (ClassroomServiceProp.RemoveClassroom(id))
                return Ok();
            else
                return NotFound();
        }
    }
}