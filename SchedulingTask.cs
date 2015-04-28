namespace NICE_SchedulingKata
{
    public class SchedulingTask
    {
        public string Id { get; private set; }

        public SchedulingTask(string id)
        {
            Id = id;
        }
    }
}