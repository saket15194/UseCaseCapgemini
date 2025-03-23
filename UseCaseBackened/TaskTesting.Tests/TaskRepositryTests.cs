using Api.Controllers;
using Domain.Entities;
using Domain.Repositries;
using Moq;

namespace TaskTesting.Tests;

    public class TaskRepositryTests
    {
        Mock<ITaskRepositry> _taskRepositry = new Mock<ITaskRepositry>();
         
        
        private readonly List<TaskDetails> _tasks = new List<TaskDetails>
        {
            new TaskDetails{ Name="Task1", Priority=0, Status = "NotStarted"},
            new TaskDetails{ Name="Task2", Priority=1,Status="InProgress" },
            new TaskDetails{ Name="Task3", Priority=2,Status="Completed"},

        };

        [Fact]
        public void GetAllTask_ReturnsAllTasks()
        {
            // Arrange
            var expectedTasks = new List<TaskDetails>
            {
                new TaskDetails { Name = "Task1", Priority = 0, Status = "NotStarted" },
                new TaskDetails { Name = "Task2", Priority = 1, Status = "InProgress" },
                new TaskDetails { Name = "Task3", Priority = 2, Status = "Completed" }
            };

            // Act
            var actualTasks = _taskRepositry.Object.GetAllTask();

            // Assert
            Assert.Equal(expectedTasks.Count, actualTasks.Count);
            for (int i = 0; i < expectedTasks.Count; i++)
            {
                Assert.Equal(expectedTasks[i].Name, actualTasks[i].Name);
                Assert.Equal(expectedTasks[i].Priority, actualTasks[i].Priority);
                Assert.Equal(expectedTasks[i].Status, actualTasks[i].Status);
            }
        }

        [Fact]
        public void SubmitTask_AddsNewTask()
        {
            // Arrange
            var newTask = new TaskDetails { Name = "Task4", Priority = 1, Status = "NotStarted" };

            // Act
            var result = _taskRepositry.Object.SubmitTask(newTask);

            // Assert
            Assert.Equal(1, result); // Assuming 1 indicates success
            var tasks = _taskRepositry.Object.GetAllTask();
            Assert.Contains(tasks, t => t.Name == newTask.Name && t.Priority == newTask.Priority && t.Status == newTask.Status);
        }

        [Fact]
        public void EditTask_UpdatesExistingTask()
        {
            // Arrange
            var existingTask = new TaskDetails { Name = "Task3", Priority = 1, Status = "InProgress" };
            var updatedTask = new TaskDetails { Name = "Task3", Priority = 2, Status = "Completed" };

            // Act
            var result = _taskRepositry.Object.EditTask(existingTask.Name, updatedTask);

            // Assert
            Assert.Equal(updatedTask, result);
            var tasks = _taskRepositry.Object.GetAllTask();
            Assert.Contains(tasks, t => t.Name == updatedTask.Name && t.Priority == updatedTask.Priority && t.Status == updatedTask.Status);
        }

        [Fact]
        public void DeleteTask_RemovesTask()
        {
            // Arrange
            var taskName = "Task3";

            // Act
            var result = _taskRepositry.Object.DeleteTask(taskName);

            // Assert
            Assert.Equal(1, result); // Assuming 1 indicates success
            var tasks = _taskRepositry.Object.GetAllTask();
            Assert.DoesNotContain(tasks, t => t.Name == taskName);
        }
    }
