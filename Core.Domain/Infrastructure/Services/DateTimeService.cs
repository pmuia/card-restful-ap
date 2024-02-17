
namespace Core.Domain.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;
        public DateTime Today => Now.Date;
        public DateTime Tomorrow => Today.AddDays(1);
        public DateTime Yesterday => Today.AddDays(-1);
        public DateTime MaxYesterday => Today.AddMilliseconds(-1);
        public DateTime MaxToday => Today.AddDays(1).AddMilliseconds(-1);
    }
}
