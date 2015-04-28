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
            var schedulingTasks = parsedTasks as List<SchedulingTask> ?? parsedTasks.ToList();

            var orderedTasks = schedulingTasks.ToList();

            schedulingTasks.GetCorrespondingDependencyForTask(parsedDependencies)
                .ForEach(correspondingDependency =>
                {
                    orderedTasks.RemoveDependencyParentFromCurrentPosition(correspondingDependency);
                    orderedTasks.InsertDependencyParentBeforeItsChild(correspondingDependency);
                });

            return orderedTasks;
        }
    }
}