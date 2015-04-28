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
                    RemoveDependencyParentFromCurrentPosition(orderedTasks, dep);
                    InsertDependencyParentBeforeItsChild(orderedTasks, dep);
                }
            }
            return orderedTasks;
        }

        private static void InsertDependencyParentBeforeItsChild(List<SchedulingTask> orderedTasks, Dependency dep)
        {
            orderedTasks.Insert(orderedTasks.FindIndex(x => x.Id == dep.Child.Id), dep.Parent);
        }

        private static void RemoveDependencyParentFromCurrentPosition(List<SchedulingTask> orderedTasks, Dependency dep)
        {
            orderedTasks.RemoveAt(orderedTasks.FindIndex(x => x.Id == dep.Parent.Id));
        }
    }
}