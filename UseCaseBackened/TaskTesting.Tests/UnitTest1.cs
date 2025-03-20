using Domain.Entities;
using Domain.Repositries;
using Moq;
using Api.Controllers;


namespace TaskTesting.Tests;

public class UnitTest1
{
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
          var mockTaskRepository = new Mock<ITaskRepositry>();
          mockTaskRepository.Setup(repo => repo.GetAllTask())
                             .Returns( _tasks); // Simulate no task found

          var controller = new TaskController(mockTaskRepository.Object);

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
        var mockTaskRepository = new Mock<ITaskRepositry>();
          mockTaskRepository.Setup(repo => repo.EditTask(taskName,_tasks))
                             .Returns(_tasks); 

        var controller = new TaskController(mockTaskRepository.Object);

          //Act
          var result=controller.EditTask(taskName,_tasks); 

          //Assert
          Xunit.Assert.Equal(result,_tasks);
    }
}
