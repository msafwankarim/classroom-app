using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using URSBackend.Models;
using URSBackend.Services;

namespace URSBackend.Controllers
{

    [Route("api/courses")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private CourseService CourseServiceProp { get; set; }

        public CourseController(CourseService service)
        {
            CourseServiceProp = service;
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult Get(string id)
        {
            return Ok(CourseServiceProp.GetCourse(id));
        }

        [HttpGet("get-all")]
        public IActionResult Get()
        {
            return Ok(CourseServiceProp.GetAllCourses());
        }

        [HttpPost]
        public IActionResult AddCourse([FromBody] Course course)
        {
            System.Console.Write(Request.Body);
            Course temp = CourseServiceProp.AddCourse(course);

            return Ok(temp);
        }

        [HttpPut("update-course/{id}")]
        public IActionResult UpdateStudent(string id, [FromBody] Course course)
        {
            Course temp = CourseServiceProp.UpdateCourse(id, course);
            if (temp != null)
                return Ok(temp);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("remove/{id}")]
        public IActionResult DeleteCourse(string id)
        {
            if (CourseServiceProp.RemoveCourse(id))
                return Ok();
            else
                return NotFound();
        }
    }
}