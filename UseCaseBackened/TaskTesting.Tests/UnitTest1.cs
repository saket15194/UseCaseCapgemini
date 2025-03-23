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
            Status="NotStarted"
        };
        var result=controller.SubmitTest(task);
        Xunit.Assert.NotNull(result);
    }

    [Fact]
    public void GetAllTaskMethod_ReturnTask()
    {
        //Arrange
         var _tasks = new List<TaskDetails>
        {
            new TaskDetails{ Name="Task1", Priority=0, Status = "NotStarted"},
            new TaskDetails{ Name="Task2", Priority=1,Status="InProgress" },
            new TaskDetails{ Name="Task3", Priority=2,Status="Completed"},

        };
          mockTaskRepository.Setup(repo => repo.GetAllTask())
                             .Returns( _tasks); // Simulate no task found
          //Act
          var result=controller.GetAllTaskDetails(); 
          //Assert
          Xunit.Assert.NotNull(result);
    }

    [Fact]
    public void GetAllTaskMethod_EditTask()
    {
        var _tasks = new TaskDetails
        {
            Name="Task3",
            Priority=1,
            Status="Inprogress"
        };
        string taskName="Task3";
          mockTaskRepository.Setup(repo => repo.EditTask(taskName,_tasks))
                             .Returns(_tasks); 
          //Act
          var result=controller.EditTask(taskName,_tasks); 

          //Assert
          Xunit.Assert.Equal(result,_tasks);
    }

    [Fact]
    public void DeleteTask_success()
    {
        int result=1;
        string taskName="Task3";
          mockTaskRepository.Setup(repo => repo.DeleteTask(taskName))
                             .Returns(result);

          //Act
          var result1=controller.DeleteTask(taskName); 

          //Assert
        var okResult = Assert.IsType<OkObjectResult>(result1);
        Xunit.Assert.Equal("task is deleted successfully",okResult.Value);
    }

     [Fact]
    public void DeleteTask_ReturnsBadRequest_WhenTaskNameIsNullOrEmpty()
    {
        // Arrange
        string taskName = string.Empty;

        // Act
        var actionResult = controller.DeleteTask(taskName);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        Assert.Equal("Task name is required.", badRequestResult.Value);
    }

     [Fact]
    public void DeleteTask_ReturnsBadRequest_WhenTaskCannotBeDeleted()
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
