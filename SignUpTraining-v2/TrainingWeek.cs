using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SignUpTraining_v2
{
    public class TrainingWeek
    {
        private readonly ServiceProvider _serviceProvider;

        public TrainingWeek(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private List<TrainingDay> _trainingWeek = new()
        {
            new(1, new() {"id=d-1_g-17", "id=d-1_g-18", "id=d-1_g-19"}),
            new(2, new() {"id=d-2_g-17", "id=d-2_g-18", "id=d-2_g-19"}),
            new(3, new() {"id=d-3_g-17", "id=d-3_g-18", "id=d-3_g-19"}),
            new(4, new() {"id=d-4_g-17", "id=d-4_g-18", "id=d-4_g-19"}),
            new(5, new() {"id=d-5_g-17", "id=d-5_g-18", "id=d-5_g-19"})
        };

        public List<string> GetTrainingDaySelectors()
        {
            var dayOfWeek = (int) GetLocalPolandTime().DayOfWeek;
            return _trainingWeek.First(x => x.Day.Equals(dayOfWeek)).Selectors;
        }

        private DateTimeOffset GetLocalPolandTime()
        {
            var logger = _serviceProvider.GetService<ILogger<TrainingWeek>>();
            logger.LogInformation("Creating playwright");
            DateTime now = DateTime.Now;
            var timeZone = "Central European Standard Time";
            TimeZoneInfo est;
            try
            {
                est = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                logger.LogError($"Unable to retrieve the {timeZone} zone.");
                throw;
            }
            catch (InvalidTimeZoneException)
            {
                logger.LogError($"Unable to retrieve the {timeZone} zone.");
                throw;
            }

            logger.LogInformation("Local time zone: {0}\n", TimeZoneInfo.Local.DisplayName);

            DateTimeOffset targetTime = TimeZoneInfo.ConvertTime(now, est);
            logger.LogInformation("Converted {0} to {1}.", now, targetTime);
            return targetTime;
        }
    }
}