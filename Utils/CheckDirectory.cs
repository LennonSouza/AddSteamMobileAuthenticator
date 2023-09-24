namespace AddSteamMobileAuthenticator.Utils
{
    internal class CheckDirectory
    {
        // Folder
        private static readonly string BasePath = AppDomain.CurrentDomain.BaseDirectory;
        private static readonly string Databasefolder = Path.Combine(BasePath, "Database/");
        private static readonly string FolderLog = Path.Combine(Databasefolder, "Log/");

        // Sub-Folder
        public static readonly string FolderAddGuard = Path.Combine(Databasefolder, "Add_Guard/");
        public static readonly string FolderSteamGuard_Done = Path.Combine(Databasefolder, "SteamGuard_Done/");

        // txt file
        public static readonly string LogFileName = Path.Combine(FolderLog, "main.log");
        public static readonly string ExceptionLogFileName = Path.Combine(FolderLog, "Exceptions.log");


        public void Check()
        {
            // Folder
            if (!Directory.Exists(Databasefolder)) Directory.CreateDirectory(Databasefolder);

            // Sub-Folder
            if (!Directory.Exists(FolderAddGuard)) Directory.CreateDirectory(FolderAddGuard);
            if (!Directory.Exists(FolderSteamGuard_Done)) Directory.CreateDirectory(FolderSteamGuard_Done);

            // txt file
            if (!File.Exists(LogFileName)) File.Create(LogFileName);
            if (!File.Exists(ExceptionLogFileName)) File.Create(ExceptionLogFileName);
        }
    }
}
