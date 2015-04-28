using System.Collections.Generic;

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
    }
}