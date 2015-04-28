using NUnit.Framework;

namespace NICE_SchedulingKata
{
    [TestFixture]
    public class SchedulerTests
    {
        private const string Empty = "[]";
        private const string TaskA = "a";
        private const string TaskB = "b";
        private const string TaskC = "c";
        private const string TaskD = "d";

        [Test]
        public void GivenEmptyTasksReturnEmptySchedule()
        {
            const string tasks = Empty;
            const string dependencies = Empty;
            var schedule = Scheduler.GetSchedule(tasks, dependencies);
            Assert.That(schedule, Is.EqualTo(Empty));
        }

        [Test]
        public void GivenTasksAndNoDependenciesTasksAreInOriginalOrder()
        {
            var tasks = AThenB();
            const string dependencies = Empty;
            var schedule = Scheduler.GetSchedule(tasks, dependencies);
            Assert.That(schedule, Is.EqualTo(AThenB()));
        }

        [Test]
        public void GivenTasksAndDependencyReturnDependentTaskFirst()
        {
            var tasks = AThenB();
            var dependencies = AIsDependentOnB();
            var schedule = Scheduler.GetSchedule(tasks, dependencies);
            Assert.That(schedule, Is.EqualTo(BThenA()));
        }

        [Test]
        public void GivenTasksAndMultipleDependenciesReturnDependentTaskFirstThenInOriginalOrder()
        {
            var tasks = AThenBThenCThenD();
            var dependencies = AIsDependentOnBAndCIsDependentOnD();
            var result = Scheduler.GetSchedule(tasks, dependencies);
            Assert.That(result, Is.EqualTo(BThenAThenDThenC()));
        }

        [Test]
        public void GivenTasksAndLinkedDependenciesReturnDependentTasksFirst()
        {
            var tasks = AThenBThenC();
            var dependencies = AIsDependentOnBAndBIsDependentOnC();
            var result = Scheduler.GetSchedule(tasks, dependencies);
            Assert.That(result, Is.EqualTo(CThenBThenA()));
        }

        [Test]
        public void GivenCyclicDependencyThenReturnError()
        {
            var tasks = AThenBThenCThenD();
            var dependencies = CyclicDependencyGraph();
            var result = Scheduler.GetSchedule(tasks, dependencies);
            Assert.That(result, Is.EqualTo("Error - this is a cyclic dependency"));
        }

        private static string AThenB()
        {
            return string.Format("[{0},{1}]", TaskA, TaskB);
        }

        private static string CThenBThenA()
        {
            return string.Format("[{0},{1},{2}]", TaskC, TaskB, TaskA);
        }

        private static string AThenBThenC()
        {
            return string.Format("[{0},{1},{2}]", TaskA, TaskB, TaskC);
        }

        private static string BThenAThenDThenC()
        {
            return string.Format("[{0},{1},{2},{3}]", TaskB, TaskA, TaskD, TaskC);
        }

        private static string AThenBThenCThenD()
        {
            return string.Format("[{0},{1},{2},{3}]", TaskA, TaskB, TaskC, TaskD);
        }

        private static string BThenA()
        {
            return string.Format("[{0},{1}]", TaskB, TaskA);
        }

        private static string AIsDependentOnB()
        {
            return string.Format("[{0}=>{1}]", TaskA, TaskB);
        }

        private static string AIsDependentOnBAndCIsDependentOnD()
        {
            return string.Format("[{0}=>{1},{2}=>{3}]", TaskA, TaskB, TaskC, TaskD);
        }

        private static string AIsDependentOnBAndBIsDependentOnC()
        {
            return string.Format("[{0}=>{1},{1}=>{2}]", TaskA, TaskB, TaskC);
        }

        private static string CyclicDependencyGraph()
        {
            return string.Format("[{0}=>{1},{1}=>{2},{2}=>{0}]", TaskA, TaskB, TaskC);
        }
    }
}
