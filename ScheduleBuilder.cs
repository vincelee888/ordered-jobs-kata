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
                    orderedTasks.RemoveAt(FindIndexOfDependencyParent(orderedTasks, dep));
                    orderedTasks.Insert(FindIndexOfDependencyChild(orderedTasks, dep), dep.Parent);
                }

                //foreach (var dependency in parsedDependencies)
                //{
                //    if (dependency.Child.Id != task.Id) continue;
                //    var parentIndex = orderedTasks.FindIndex(x => x.Id == dependency.Parent.Id);
                //    orderedTasks.RemoveAt(parentIndex);
                //    var childIndex = orderedTasks.FindIndex(x => x.Id == dependency.Child.Id);
                //    orderedTasks.Insert(childIndex, dependency.Parent);
                //}
            }
            return orderedTasks;
        }

        private static int FindIndexOfDependencyChild(List<SchedulingTask> orderedTasks, Dependency dep)
        {
            return orderedTasks.FindIndex(x => x.Id == dep.Child.Id);
        }

        private static int FindIndexOfDependencyParent(List<SchedulingTask> orderedTasks, Dependency dep)
        {
            return orderedTasks.FindIndex(x => x.Id == dep.Parent.Id);
        }
    }
}