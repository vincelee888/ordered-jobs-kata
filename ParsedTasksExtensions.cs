using System.Collections.Generic;
using NICE_SchedulingKata;

static internal class ParsedTasksExtensions
{
    public static void InsertDependencyParentBeforeItsChild(List<SchedulingTask> orderedTasks, Dependency dep)
    {
        orderedTasks.Insert(orderedTasks.FindIndex(x => x.Id == dep.Child.Id), dep.Parent);
    }

    public static void RemoveDependencyParentFromCurrentPosition(List<SchedulingTask> orderedTasks, Dependency dep)
    {
        orderedTasks.RemoveAt(orderedTasks.FindIndex(x => x.Id == dep.Parent.Id));
    }
}