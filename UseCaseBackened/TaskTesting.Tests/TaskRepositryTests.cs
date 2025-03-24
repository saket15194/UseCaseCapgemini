using Api.Controllers;
using Data;
using Domain.Entities;
using Domain.Repositries;
using Moq;

namespace TaskTesting.Tests;

    public class TaskRepositryTests
    {
        Mock<ITaskRepositry> _taskRepositry = new Mock<ITaskRepositry>();

        private readonly TaskRepositry taskRepositry;

        public TaskRepositryTests()
        {
            this.taskRepositry = new TaskRepositry();
        }
         
        
        private readonly List<TaskDetails> _tasks = new List<TaskDetails>
        {
            new TaskDetails{ Name="Task1", Priority=0, Status = Domain.Entities.TaskStatus.NotStarted},
            new TaskDetails{ Name="Task2", Priority=1,Status=Domain.Entities.TaskStatus.InProgress},
            new TaskDetails{ Name="Task3", Priority=2,Status=Domain.Entities.TaskStatus.Completed},

        };

        [Fact]
        public void GetAllTask_ReturnsAllTasks()
        {
            
            // Act
            var actualTasks = taskRepositry.GetAllTask();

            // Assert
             Assert.NotNull(actualTasks);
        }

        [Fact]
        public void SubmitTask_AddsNewTask()
        {
            // Arrange
            var newTask = new TaskDetails { Name = "Task4", Priority = 1, Status = Domain.Entities.TaskStatus.NotStarted };

            // Act
            var result = taskRepositry.SubmitTask(newTask);

            // Assert
            Assert.Equal(1, result); // Assuming 1 indicates success
            var tasks = taskRepositry.GetAllTask();
            Assert.Contains(tasks, t => t.Name == newTask.Name && t.Priority == newTask.Priority && t.Status == newTask.Status);
        }

        [Fact]
        public void EditTask_UpdatesExistingTask()
        {
            // Arrange
            var existingTask = new TaskDetails { Name = "Task3", Priority = 1, Status = Domain.Entities.TaskStatus.Completed };
            var updatedTask = new TaskDetails { Name = "Task3", Priority = 2, Status = Domain.Entities.TaskStatus.InProgress };

            // Act
            var result = taskRepositry.EditTask(existingTask.Name, updatedTask);

            // Assert
            Assert.Equal(1, result);
            var tasks = taskRepositry.GetAllTask();
            Assert.Contains(tasks, t => t.Name == updatedTask.Name && t.Priority == updatedTask.Priority && t.Status == updatedTask.Status);
        }

        [Fact]
        public void DeleteTask_RemovesTask()
        {
            // Arrange
            var taskName = "Task3";

            // Act
            var result = taskRepositry.DeleteTask(taskName);

            // Assert
            Assert.Equal(1, result); // Assuming 1 indicates success
            var tasks = taskRepositry.GetAllTask();
            Assert.DoesNotContain(tasks, t => t.Name == taskName);
        }
    }
