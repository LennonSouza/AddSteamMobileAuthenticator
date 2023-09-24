namespace AddSteamMobileAuthenticator.Utils
{
    public class ConsoleHelper
    {
        private static readonly SemaphoreSlim _semaphoreSlim = new SemaphoreSlim(1);

        public static void LogSuccess(string message) { Log(Frm_Main._frm_Main.richtxt_Console, "Success", message, Color.Green); }
        public static void LogInformation(string message) { Log(Frm_Main._frm_Main.richtxt_Console, "Information", message, Color.SteelBlue); }
        public static void LogWarning(string message) { Log(Frm_Main._frm_Main.richtxt_Console, "Warning", message, Color.Yellow); }
        public static void LogDanger(string message) { Log(Frm_Main._frm_Main.richtxt_Console, "Error", message, Color.DarkRed); }
        public static void LogException(string message, Exception ex) { string errorMessage = $"{ex.Message}\n{ex.StackTrace}"; Log(Frm_Main._frm_Main.richtxt_Console, "Exception", errorMessage, Color.DarkRed); }

        private static async Task Log(RichTextBox console, string lvl, string msg, Color color)
        {
            lock (_semaphoreSlim)
            {
                console.Invoke(new Action(() => console.SelectionColor = color));
                console.Invoke(new Action(() => console.AppendText(FormatLogMessageData(msg, lvl) + Environment.NewLine)));
                console.Invoke(new Action(() => console.SelectionColor = console.ForeColor));
                console.Invoke(new Action(() => console.SelectionStart = console.Text.Length));
                console.Invoke(new Action(() => console.ScrollToCaret()));
            }

            await _semaphoreSlim.WaitAsync();
            try
            {
                string fileName = lvl == "Exception" ? CheckDirectory.ExceptionLogFileName : CheckDirectory.LogFileName;
                using (StreamWriter writer = File.AppendText(fileName)) writer.WriteLine(FormatLogMessageData(msg, lvl));
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        private static string FormatLogMessageData(string msg, string lvl)
        {
            return $"[{DateTime.Now:HH:mm:ss}] [{lvl}] {msg}";
        }
    }
}
