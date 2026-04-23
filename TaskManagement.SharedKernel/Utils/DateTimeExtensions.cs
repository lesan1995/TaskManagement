namespace TaskManagement.SharedKernel.Utils
{
    public static class DateTimeExtensions
    {
        public static string ToTimeAgo(this DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalSeconds < 60)
                return $"{timeSpan.Seconds} seconds ago";
            if (timeSpan.TotalMinutes < 2)
                return "about a minutes ago";
            if (timeSpan.TotalMinutes < 60)
                return $"{timeSpan.Minutes} minutes ago";
            if (timeSpan.TotalHours < 2)
                return "about an hours ago";
            if (timeSpan.TotalHours < 24)
                return $"{timeSpan.Hours} hours ago";
            if (timeSpan.TotalDays < 2)
                return "yesterday";
            if (timeSpan.TotalDays < 30)
                return $"{timeSpan.TotalDays} days ago";
            return dateTime.ToString("dd/MM/yyyy");
        }
    }
}
