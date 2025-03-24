using Domain.Entities;
using Domain.Repositries;
using Moq;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace TaskTesting.Tests;

public class UnitTest1
{
    Mock<ITaskRepositry> mockTaskRepository = new Mock<ITaskRepositry>();
    private readonly TaskController controller;

    public UnitTest1()
    {
        controller = new TaskController(mockTaskRepository.Object);
    }

    [Fact]
    public void submitTest_Success_NotNull()
    {
        mockTaskRepository.Setup(repo=>repo.SubmitTask(It.IsAny<TaskDetails>()))
                          .Returns(1);
        var task=new TaskDetails
        {
            Name="Task4",
            Priority=1,
            Status=Domain.Entities.TaskStatus.NotStarted
        };
        var result=controller.SubmitTest(task);
        Xunit.Assert.NotNull(result);
    }

    [Fact]
    public void submitTest_Fail_InvalidInput()
    {
        //controller.ModelState.AddModelError("Priority");
       
       var invalidmodel=new TaskDetails
       {
              Name="Task56",
              Priority=10,
              Status=Domain.Entities.TaskStatus.NotStarted

       };

        var result=controller.SubmitTest(invalidmodel);
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Priority should be between 1 to 3", badRequestResult.Value);
    }

    [Fact]
    public void GetAllTaskMethod_ReturnTask()
    {
        //Arrange
         var _tasks = new List<TaskDetails>
        {
            new TaskDetails{ Name="Task1", Priority=0, Status = Domain.Entities.TaskStatus.NotStarted},
            new TaskDetails{ Name="Task2", Priority=1,Status=Domain.Entities.TaskStatus.InProgress}, 
            new TaskDetails{ Name="Task3", Priority=2,Status=Domain.Entities.TaskStatus.Completed},

        };
          mockTaskRepository.Setup(repo => repo.GetAllTask())
                             .Returns( _tasks); // Simulate no task found
          //Act
          var result=controller.GetAllTaskDetails(); 
          //Assert
          Xunit.Assert.NotNull(result);
    }

    [Fact]
    public void GetEditTask_success()
    {
        var _tasks = new TaskDetails
        {
            Name="Task3",
            Priority=1,
            Status=Domain.Entities.TaskStatus.InProgress
        };
        string taskName="Task3";
          mockTaskRepository.Setup(repo => repo.EditTask(taskName,_tasks))
                             .Returns(1); 
          //Act
          var result=controller.EditTask(taskName,_tasks); 

          //Assert
          Xunit.Assert.NotNull(result);
    }

    [Fact]
    public void DeleteTask_successsfullly()
    {
        int result=1;
        string taskName="Task3";
          mockTaskRepository.Setup(repo => repo.DeleteTask(taskName))
                             .Returns(result);

          //Act
          var result1=controller.DeleteTask(taskName); 

          //Assert
        var okResult = Assert.IsType<OkObjectResult>(result1);
        Assert.Equal("task is deleted successfully",okResult.Value);
    }

     [Fact]
    public void DeleteTask_TaskCannotBeDeleted()
    {
        // Arrange
        int result = 0;
        string taskName = "Task3";
        mockTaskRepository.Setup(repo => repo.DeleteTask(taskName))
                          .Returns(result);

        // Act
        var actionResult = controller.DeleteTask(taskName);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.Equal("Only completed Task can be deleted", badRequestResult.Value);
    }
}
