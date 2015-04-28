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
                var dep = parsedDependencies.SingleOrDefault(x => x.Child.Id == task.Id);
                if (dep != null)
                {
                    orderedTasks.RemoveDependencyParentFromCurrentPosition(dep);
                    orderedTasks.InsertDependencyParentBeforeItsChild(dep);
                }
            }
            return orderedTasks;
        }
    }
}