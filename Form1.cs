using AddSteamMobileAuthenticator.Functions;
using AddSteamMobileAuthenticator.Utils;

namespace AddSteamMobileAuthenticator
{
    public partial class Frm_Main : Form
    {
        public static Frm_Main _frm_Main;

        public Frm_Main()
        {
            _frm_Main = this;
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            // Verifica se o folder está vazio ou contém algum arquivo
            string[] files = Directory.GetFiles(CheckDirectory.FolderAddGuard);
            if (files.Length == 0)
            {
                ConsoleHelper.LogWarning("There are no files in the 'Accounts' folder.");
                return;
            }

            // Exibe a quantidade de arquivos na pasta
            int numberOfFiles = files.Length;
            ConsoleHelper.LogInformation($"There are {numberOfFiles} files in the 'Accounts' folder.");

            foreach (string filePath in files)
            {
                string info = File.ReadAllText(filePath);

                AddMobile.Execute(info, filePath);
            }
        }
    }
}