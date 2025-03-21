using Microsoft.AspNetCore.Mvc;
using Domain;
using Domain.Repositries;
using Domain.Entities;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepositry _taskRepositry;
        public TaskController(ITaskRepositry taskRepositry)
        {
            this._taskRepositry = taskRepositry;
        }

        [HttpGet("list-task")]
        public List<TaskDetails> GetAllTaskDetails()
        {
            return _taskRepositry.GetAllTask();
        }

        [HttpPost("create-task")]
        public IActionResult SubmitTast([FromBody]TaskDetails taskDetails)
        {
            var result= _taskRepositry.SubmitTask(taskDetails);
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            if(result==0)
            {
                return BadRequest("Name should be unique");
            }
            return Ok(taskDetails);
        }

        [HttpPut("edit-task/{name}")]
        public TaskDetails EditTask(string name,[FromBody] TaskDetails taskDetails )
        {
            return _taskRepositry.EditTask(name,taskDetails);
        }

        [HttpDelete("delete-task")]
        public IActionResult DeleteTask([FromBody] string taskname )
        {
            var result= _taskRepositry.DeleteTask(taskname);

            if(result==0)
            {
                return BadRequest("Task status should be completed");
            }
            else
            {
                return Ok("task is deleted");
            }

        }

    }
}