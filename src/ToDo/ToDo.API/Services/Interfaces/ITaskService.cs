using ToDo.API.Models;

namespace ToDo.API.Services.Interfaces
{
    public interface ITaskService
    {
        IEnumerable<TaskItem> GetAllTasks();
        TaskItem GetTaskById(int id);
        void CreateTask(TaskItem task);
        void UpdateTask(TaskItem task);
        void DeleteTask(int id);
    }
}
