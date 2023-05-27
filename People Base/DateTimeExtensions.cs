using System;

namespace People_Base
{
    public static class DateTimeExtensions
    {
        public static int GetAge(this DateTime date)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            var now = DateTime.Now;

            TimeSpan span = now - date;
            return (zeroTime + span).Year - 1;
        }
    }
}