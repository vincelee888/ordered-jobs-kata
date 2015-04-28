using System;
using System.Linq;

namespace NICE_SchedulingKata
{
    public class Scheduler
    {
        public static string GetSchedule(string tasks, string dependencies)
        {
            var parsedTasks = new TaskParser().Parse(tasks);
            var parsedDependencies = new DependencyParser().Parse(dependencies);

            try
            {
                Dependency.Validate(parsedDependencies);
            }
            catch
            {
                return "Error - this is a cyclic dependency";
            }

            var scheduledTasks = ScheduleBuilder.BuildSchedule(parsedTasks, parsedDependencies);

            return "[" + String.Join(",", scheduledTasks.Select(x => x.Id)) + "]";
        }
    }
}