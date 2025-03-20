using Domain.Entities;

namespace Domain.Repositries
{
    public interface ITaskRepositry
    {
        List<TaskDetails>GetAllTask();
        int SubmitTask(TaskDetails task);
        TaskDetails EditTask(string name,TaskDetails task);

        void DeleteTask(List<string> name);
    }
}