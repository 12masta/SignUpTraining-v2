namespace SignUpTraining_v2
{
    public class TrainingWeek
    {
        private List<TrainingDay> _trainingWeek = new()
        {
            new(1, new() {"id=d-1_g-17", "id=d-1_g-18", "id=d-1_g-19"}),
            new(2, new() {"id=d-2_g-17", "id=d-2_g-18", "id=d-2_g-19"}),
            new(3, new() {"id=d-3_g-17", "id=d-3_g-18", "id=d-3_g-19"}),
            new(4, new() {"id=d-4_g-17", "id=d-4_g-18", "id=d-4_g-19"}),
            new(5, new() {"id=d-5_g-17", "id=d-5_g-18"})
        };

        public List<string> GetTrainingDaySelectors()
        {
            var dayOfWeek = (int) DateTime.Today.DayOfWeek;
            return _trainingWeek.First(x => x.Day.Equals(dayOfWeek)).Selectors;
        }
    }
}