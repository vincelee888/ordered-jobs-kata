using System;
using System.Collections.Generic;
using System.Linq;

namespace NICE_SchedulingKata
{
    public interface IDependencyParser
    {
        IEnumerable<Dependency> Parse(string dependencies);
    }

    public class DependencyParser : IDependencyParser
    {
        public IEnumerable<Dependency> Parse(string dependencies)
        {
            var csvDependencies = dependencies.Substring(1, dependencies.Length - 2);
            return string.IsNullOrEmpty(csvDependencies) 
                ? new List<Dependency>() 
                : csvDependencies.Split(',').Select(ParseSingle);
        }

        private static Dependency ParseSingle(string x)
        {
            var noSpaces = x.Replace(" ", string.Empty);
            var csv = noSpaces.Replace("=>", ",");
            var tasks = csv.Split(',');
            return new Dependency(tasks[1], tasks[0]);
        }
    }
}