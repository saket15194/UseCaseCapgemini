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
        public IActionResult SubmitTest([FromBody]TaskDetails taskDetails)
        {
            if (string.IsNullOrEmpty(taskDetails.Name))
            {
                return BadRequest("Task name cannot be empty.");
            }
            var result= _taskRepositry.SubmitTask(taskDetails);
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            if(result==0)
            {
                return BadRequest("Task with this name already exists");
            }
            return Ok($"Task created successfully {taskDetails}");
        }

        [HttpPut("edit-task/{name}")]
        public TaskDetails EditTask(string name,[FromBody] TaskDetails taskDetails )
        {
            return _taskRepositry.EditTask(name,taskDetails);
        }

        [HttpDelete("delete-task")]
        public IActionResult DeleteTask([FromBody] string taskname)
        {
            if (string.IsNullOrEmpty(taskname))
            {
                return BadRequest("Task name is required.");
            }
            var result= _taskRepositry.DeleteTask(taskname);

            if(result==0)
            {
                return BadRequest("Only completed Task can be deleted");
            }
            else
            {
                return Ok("task is deleted successfully");
            }

        }

    }
}