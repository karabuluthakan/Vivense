namespace Library.ApacheKafka.Events.Scheduler
{
    public enum ScheduleStrategy
    {
        StartNow,
        StartAt,
        DailyAtHourAndMinute,
        WithIntervalForever,
        WithIntervalEndAt,
        DayOfWeek
    }
}
