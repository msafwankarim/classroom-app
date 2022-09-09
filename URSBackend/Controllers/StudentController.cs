using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URSBackend.Models;
using URSBackend.Services;

namespace URSBackend.Controllers
{

    [Route("api/students")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private StudentService StudentServiceProp { get; set; }

        public StudentController(StudentService service)
        {
            StudentServiceProp = service;
        }

        //[HttpGet("{id}/today-classes")]
        //public IActionResult GetTodayClasses(int id)
        //{
        //    return Ok(StudentServiceProp.GetTodayClasses(id));
        //}

        [HttpGet("get-all")]
        public IActionResult Get()
        {
            return Ok(StudentServiceProp.GetAllStudents());
        }

        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            System.Console.Write(Request.Body);
            Student temp = StudentServiceProp.AddStudent(student);

            return Ok(temp);
        }

        [HttpPut("update-student/{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            Student temp = StudentServiceProp.UpdateStudent(id, student);
            if (temp != null)
                return Ok(temp);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (StudentServiceProp.RemoveStudent(id))
                return Ok();
            else
                return NotFound();
        }
    }
}