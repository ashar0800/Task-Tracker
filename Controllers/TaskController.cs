using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TaskTrackingAPI.Models;

namespace TaskTrackingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private static List<Task> _tasks = new List<Task>();
        private static int _taskIdCounter = 1;

        [HttpPost]
        public ActionResult<Task> CreateTask(Task task)
        {
            task.Id = _taskIdCounter++;
            _tasks.Add(task);
            return CreatedAtAction(nameof(GetTask), new { id = task.Id }, task);
        }

        [HttpGet]
        public ActionResult<List<Task>> GetAllTasks()
        {
            return _tasks;
        }

        [HttpGet("{id}")]
        public ActionResult<Task> GetTask(int id)
        {
            var task = _tasks.Find(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id, Task updatedTask)
        {
            var taskToUpdate = _tasks.Find(t => t.Id == id);
            if (taskToUpdate == null)
            {
                return NotFound();
            }

            taskToUpdate.Title = updatedTask.Title;
            taskToUpdate.Description = updatedTask.Description;
            taskToUpdate.IsCompleted = updatedTask.IsCompleted;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var taskToRemove = _tasks.Find(t => t.Id == id);
            if (taskToRemove == null)
            {
                return NotFound();
            }

            _tasks.Remove(taskToRemove);
            return NoContent();
        }
    }
}
