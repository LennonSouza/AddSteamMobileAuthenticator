namespace AddSteamMobileAuthenticator.Utils
{
    public class Waiter
    {
        public static void waitForMiliSec(int value)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(value));
        }

        public static async Task waitForMiliSecAsync(int value)
        {
            Thread.Sleep(TimeSpan.FromMilliseconds(value));
        }

        public static void waitForSec(int value)
        {
            Thread.Sleep(TimeSpan.FromSeconds(value));
        }

        public static async Task waitForSecAsync(int value)
        {
            Thread.Sleep(TimeSpan.FromSeconds(value));
        }

        public static void waitForMin(int value)
        {
            Thread.Sleep(TimeSpan.FromMinutes(value));
        }

        public static async Task waitForMinAsync(int value)
        {
            Thread.Sleep(TimeSpan.FromMinutes(value));
        }

        public static void waitForHour(int value)
        {
            Thread.Sleep(TimeSpan.FromHours(value));
        }

        public static async Task waitForHourAsync(int value)
        {
            Thread.Sleep(TimeSpan.FromHours(value));
        }

        public static void waitForDay(int value)
        {
            Thread.Sleep(TimeSpan.FromDays(value));
        }

        public static async Task waitForDayAsync(int value)
        {
            Thread.Sleep(TimeSpan.FromDays(value));
        }

        public static void waitForTick(int value)
        {
            Thread.Sleep(TimeSpan.FromTicks(value));
        }

        public static async Task waitForTickAsync(int value)
        {
            Thread.Sleep(TimeSpan.FromTicks(value));
        }
    }
}
