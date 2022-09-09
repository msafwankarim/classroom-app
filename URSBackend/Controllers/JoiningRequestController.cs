using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using URSBackend.Models;
using URSBackend.Models.ViewModels;
using URSBackend.Services;

namespace URSBackend.Controllers
{

    [Route("api/requests")]
    [ApiController]
    public class JoiningRequestController : ControllerBase
    {
        private JoiningRequestService JRService { get; set; }

        public JoiningRequestController(JoiningRequestService service)
        {
            JRService = service;
        }

        [HttpGet("get-all")]
        public IActionResult Get()
        {
            return Ok(JRService.GetAll());
        }

        [HttpPost]
        public IActionResult PostRequest([FromBody] JoiningRequestVM jrVM)
        {
            System.Console.Write(Request.Body);
            JoiningRequest temp = JRService.AddRequest(jrVM);

            if (temp == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(temp);
            }
        }

        [HttpPut("approve-request/{id}")]
        public IActionResult ApproveRequest(int id)
        {
            JoiningRequestVM temp = JRService.ApproveRequest(id);
            if (temp != null)
                return Ok(temp);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpPut("reject-request/{id}")]
        public IActionResult RejectRequest(int id)
        {
            JoiningRequestVM temp = JRService.RejectRequest(id);
            if (temp != null)
                return Ok(temp);
            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("notifications/{id}")]
        public IActionResult GetNotifications(int id)
        {
            var list = JRService.GetNotifications(id);
            if (list != null)
                return Ok(list);
            else return NotFound();
        }
    }
}