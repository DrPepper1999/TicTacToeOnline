namespace TicTacToeOnline.Infrastructure.BackgroundJobs
{
    public class BackgroundJobsSettings
    {
        public const string SectionName = "BackgroundJobsSettings";
        public int IntervalInSeconds { get; init; }
    }
}
