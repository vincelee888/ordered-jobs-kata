using System;
using System.Collections.Generic;
using System.Linq;

namespace NICE_SchedulingKata
{
    public class Dependency
    {
        public SchedulingTask Parent { get; private set; }
        public SchedulingTask Child { get; private set; }

        public Dependency(string parent, string child)
        {
            Parent = new SchedulingTask(parent);
            Child = new SchedulingTask(child);
        }

        public static void Validate(IEnumerable<Dependency> parsedDependencies)
        {
            foreach (var dependency in parsedDependencies)
            {
                var currRoot = new List<string> { dependency.Child.Id };
                var branch = GetDependencyBranch(dependency.Child.Id, parsedDependencies).ToList();
                currRoot = currRoot.Concat(branch).ToList();
                if (currRoot.Count(x => x == dependency.Child.Id) > 1)
                {
                    throw new ArgumentException("Cyclic Dependency");
                }
            }
        }

        private static IEnumerable<string> GetDependencyBranch(string child, IEnumerable<Dependency> parsedDependencies)
        {
            var dependenciesBranch = new List<string>();
            var index = 0;
            var nextDependency = parsedDependencies.FirstOrDefault(x => x.Parent.Id == child);
            while (nextDependency != null && index < parsedDependencies.Count())
            {
                dependenciesBranch.Add(nextDependency.Child.Id);
                nextDependency = parsedDependencies.FirstOrDefault(x => x.Parent.Id == nextDependency.Child.Id);
                index++;
            }
            return dependenciesBranch;
        }
    }
}