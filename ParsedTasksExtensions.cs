using System.Collections.Generic;
using System.Linq;

namespace NICE_SchedulingKata
{
    static internal class ParsedTasksExtensions
    {
        public static void InsertDependencyParentBeforeItsChild(this List<SchedulingTask> orderedTasks, Dependency dep)
        {
            orderedTasks.Insert(orderedTasks.FindIndex(x => x.Id == dep.Child.Id), dep.Parent);
        }

        public static void RemoveDependencyParentFromCurrentPosition(this List<SchedulingTask> orderedTasks, Dependency dep)
        {
            orderedTasks.RemoveAt(orderedTasks.FindIndex(x => x.Id == dep.Parent.Id));
        }

        public static List<Dependency> GetCorrespondingDependencyForTask(this List<SchedulingTask> parsedTasks, IEnumerable<Dependency> parsedDependencies)
        {
            return parsedTasks
                .Select(task => parsedDependencies.SingleOrDefault(x => x.Child.Id == task.Id))
                .Where(dep => dep != null)
                .ToList();
        }
    }
}