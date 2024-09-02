using Microsoft.AspNetCore.Mvc;
using ToDo.API.Models;
using ToDo.API.Services.Interfaces;

namespace ToDo.API.Controllers
{
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskService _taskService;

        public TasksController(ILogger<TasksController> logger, ITaskService taskService)
        {
            _logger = logger;
            _taskService = taskService;
        }


        [HttpGet("GetTasks")]

        public IActionResult GetTasks()
        {
            try
            {
                var tasks = _taskService.GetAllTasks();
                _logger.LogInformation("Listing all tasks");
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving tasks");
                return StatusCode(500, "An error occurred while retrieving tasks.");
            }
        }

        [HttpPost("Create")]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            try
            {
                _taskService.CreateTask(task);
                _logger.LogInformation("Task created with ID {TaskId}", task.Id);
                return CreatedAtAction(nameof(GetTasks), new { id = task.Id }, task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating task.");
                return StatusCode(500, "An error occurred while creating the task.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, [FromBody] TaskItem updatedTask)
        {
            try
            {
                var existingTask = _taskService.GetTaskById(id);
                if (existingTask == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found", id);
                    return NotFound();
                }

                updatedTask.Id = id;
                _taskService.UpdateTask(updatedTask);
                _logger.LogInformation("Task with ID {TaskId} updated", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating task with ID {TaskId}.", id);
                return StatusCode(500, "An error occurred while updating the task.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            try
            {
                var task = _taskService.GetTaskById(id);
                if (task == null)
                {
                    _logger.LogWarning("Task with ID {TaskId} not found",id);
                    return NotFound();
                }

                _taskService.DeleteTask(id);
                _logger.LogInformation("Task with ID {TaskId} deleted.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting task with ID {TaskId}", id);
                return StatusCode(500, "An error occurred while deleting the task.");
            }
        }
    }
}