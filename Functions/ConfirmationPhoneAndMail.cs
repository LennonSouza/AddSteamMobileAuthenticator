using AddSteamMobileAuthenticator.Functions.SteamAuth;
using AddSteamMobileAuthenticator.Utils;

namespace AddSteamMobileAuthenticator.Functions
{
    internal class ConfirmationPhoneAndMail
    {
        public static async Task<string> Linker(AuthenticatorLinker linker, string number, string info)
        {
            AuthenticatorLinker.LinkResult authenticatorLinker = AuthenticatorLinker.LinkResult.GeneralFailure;
            while (authenticatorLinker != AuthenticatorLinker.LinkResult.AwaitingFinalization)
            {
                try
                {
                    authenticatorLinker = await linker.AddAuthenticator();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.LogException("Error adding your authenticator: ", ex);
                    return "";
                }

                switch (authenticatorLinker)
                {
                    case AuthenticatorLinker.LinkResult.MustProvidePhoneNumber:

                        string phoneNumber = "";
                        while (!Okay(phoneNumber))
                        {
                            ConsoleHelper.LogInformation("Adding number...");
                            string phoneNumberForm = number;

                            phoneNumber = Filter(phoneNumberForm);
                        }
                        linker.PhoneNumber = phoneNumber;
                        break;

                    case AuthenticatorLinker.LinkResult.AuthenticatorPresent:
                        MessageBox.Show("This account already has an authenticator linked. You must remove that authenticator to add SDA as your authenticator.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return null;

                    case AuthenticatorLinker.LinkResult.FailureAddingPhone:
                        MessageBox.Show("Failed to add your phone number. Please try again or use a different phone number.", "Steam Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        linker.PhoneNumber = null;
                        break;

                    case AuthenticatorLinker.LinkResult.MustRemovePhoneNumber:
                        linker.PhoneNumber = null;
                        break;

                    case AuthenticatorLinker.LinkResult.MustConfirmEmail:

                        ConsoleHelper.LogInformation("Confirming email...");

                        bool success = GetMail.GetCode(info, "url");
                        if (success)
                        {
                            ConsoleHelper.LogSuccess("Confirmed email!");
                            break;
                        }
                        else
                        {
                            ConsoleHelper.LogDanger("Error confirming email.");
                            return null;
                        }

                    case AuthenticatorLinker.LinkResult.GeneralFailure:
                        MessageBox.Show("Error adding your authenticator.", "Steam Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                }
            }
            return authenticatorLinker.ToString();
        }

        public static bool Okay(string phoneNumber)
        {
            if (phoneNumber == null || phoneNumber.Length == 0) return false;
            if (phoneNumber[0] != '+') return false;
            return true;
        }

        public static string Filter(string phoneNumber)
        {
            return phoneNumber.Replace("-", "").Replace("(", "").Replace(")", "");
        }
    }
}
