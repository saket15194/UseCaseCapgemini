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
        public IActionResult GetAllTaskDetails()
        {
            var task= _taskRepositry.GetAllTask();

            var tasklist = task.Select(x => new 
            {
                x.Name,
                x.Priority,
                Status = x.Status.ToString()  // Convert Status enum to string
            }).ToList();

            return Ok(tasklist);
        }

        [HttpPost("create-task")]
        public IActionResult SubmitTest([FromBody]TaskDetails taskDetails)
        {
            if (string.IsNullOrEmpty(taskDetails.Name))
            {
                return BadRequest("Task name cannot be empty.");
            }
            if(taskDetails.Priority<1 || taskDetails.Priority>3)
            {
                //ModelState.AddModelError("Priority", "Priority should be between 1 to 3");
                return BadRequest("Priority should be between 1 to 3");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            var result= _taskRepositry.SubmitTask(taskDetails);

            if(result==0)
            {
                return BadRequest("Task with this name already exists");
            }
            return Ok($"Task created successfully {taskDetails}");
        }

        [HttpPut("edit-task/{name}")]
        public IActionResult EditTask(string name,[FromBody] TaskDetails taskDetails )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Invalid Input");
            }
            var result= _taskRepositry.EditTask(name,taskDetails);

            if(result==0)
            {
                return BadRequest("Edit Task is not successful");
            }
            else
            {
                return Ok("Task is Edited successfully");
            }

        }

        [HttpDelete("delete-task")]
        public IActionResult DeleteTask([FromBody] string taskname)
        {
            
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