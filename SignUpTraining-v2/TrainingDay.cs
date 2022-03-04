namespace SignUpTraining_v2
{
    public class TrainingDay
    {
        public int Day { get; }
        public List<string> Selectors { get; }

        public TrainingDay(int day, List<string> selectors)
        {
            Day = day;
            Selectors = selectors;
        }
    }
}