using System.Security.Cryptography.X509Certificates;
using Domain.Entities;
using Domain.Repositries;

namespace Data
{
    public class TaskRepositry : ITaskRepositry
    {
        private readonly List<TaskDetails> _tasks = new List<TaskDetails>
        {
            new TaskDetails{ Name="Task1", Priority=1, Status = Domain.Entities.TaskStatus.NotStarted},
            new TaskDetails{ Name="Task2", Priority=2,Status=Domain.Entities.TaskStatus.InProgress}, 
            new TaskDetails{ Name="Task3", Priority=3,Status=Domain.Entities.TaskStatus.Completed},

        };

              
        public List<TaskDetails>GetAllTask()
        {
            return _tasks;
        }
        public int SubmitTask(TaskDetails task)
        {
            task.Name=task.Name?.Trim();
            var IsTaskExist=_tasks.Any(x=>x.Name==task.Name?.Trim());
            if(!IsTaskExist)
            {
                _tasks.Add(task);
                return 1;
            }
             return 0;
        }
        public int EditTask(string name,TaskDetails task)
        {
            TaskDetails taskdetails=new TaskDetails();;
            var existingtask = _tasks.FirstOrDefault(x => x.Name==name);
            if(existingtask != null)
            {
                existingtask.Priority=task.Priority;
                existingtask.Status=task.Status;
                return 1;

            }
            else
            {
                return 0;
            }
        }

        public int DeleteTask(string name)
        {
            int result=0;
            var existingtask = _tasks.FirstOrDefault(x => x.Name==name);
            if(existingtask != null && existingtask.Status.Equals(Domain.Entities.TaskStatus.Completed))
            {
                result= _tasks.RemoveAll(x=>x.Name==name);

            }
                
            return result;
        }


    }
}