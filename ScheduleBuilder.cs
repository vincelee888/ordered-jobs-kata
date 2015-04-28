using System.Collections.Generic;
using System.Linq;

namespace NICE_SchedulingKata
{
    public class ScheduleBuilder
    {
        public static IEnumerable<SchedulingTask> BuildSchedule(
            IEnumerable<SchedulingTask> parsedTasks, 
            IEnumerable<Dependency> parsedDependencies)
        {
            var orderedTasks = parsedTasks.ToList();

            foreach (var task in parsedTasks)
            {
                foreach (var dependency in parsedDependencies)
                {
                    if (dependency.Child.Id != task.Id) continue;
                    var parentIndex = orderedTasks.FindIndex(x => x.Id == dependency.Parent.Id);
                    orderedTasks.RemoveAt(parentIndex);
                    var childIndex = orderedTasks.FindIndex(x => x.Id == dependency.Child.Id);
                    orderedTasks.Insert(childIndex, dependency.Parent);
                }
            }
            return orderedTasks;
        }
    }
}