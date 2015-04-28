using System.Collections.Generic;
using System.Linq;

namespace NICE_SchedulingKata
{
    public interface ITaskParser
    {
        IEnumerable<SchedulingTask> Parse(string tasks);
    }

    public class TaskParser : ITaskParser
    {
        public IEnumerable<SchedulingTask> Parse(string tasks)
        {
            var csvTasks = tasks.Substring(1, tasks.Length - 2);
            var taskArray = csvTasks.Split(',');
            return taskArray.Select(task => new SchedulingTask(task));
        }
    }
}
