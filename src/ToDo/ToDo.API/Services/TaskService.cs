using ToDo.API.Models;
using ToDo.API.Services.Interfaces;

namespace ToDo.API.Services
{
    public class TaskService: ITaskService
    {
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        public IEnumerable<TaskItem> GetAllTasks()
        {
            return _tasks;
        }
        public TaskItem GetTaskById(int id)
        {
            return _tasks.FirstOrDefault(t => t.Id == id);
        }
        public void UpdateTask(TaskItem task)
        {
            var existingTask = GetTaskById(task.Id);
            if (existingTask != null)
            {
                existingTask.Name = task.Name;
                existingTask.IsCompleted = task.IsCompleted;
            }
        }

        public void DeleteTask(int id)
        {
            var task = GetTaskById(id);
            if (task != null)
            {
                _tasks.Remove(task);
            }
        }

        public void CreateTask(TaskItem task)
        {
            task.Id = _tasks.Count + 1;
            _tasks.Add(task);
        }
    }
}
