using Domain.Entities;

namespace Domain.Repositries
{
    public interface ITaskRepositry
    {
        List<TaskDetails>GetAllTask();
        int SubmitTask(TaskDetails task);
        int EditTask(string name,TaskDetails task);

        int DeleteTask(string name);
    }
}