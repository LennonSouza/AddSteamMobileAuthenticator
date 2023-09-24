using AddSteamMobileAuthenticator.Functions.SteamAuth;
using AddSteamMobileAuthenticator.Utils;
using Newtonsoft.Json;
using SteamKit2;
using SteamKit2.Authentication;
using SteamKit2.Internal;

namespace AddSteamMobileAuthenticator.Functions
{
    internal class AddMobile
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        public static string codeEmail = "Disabled", urlConfirmation = "";
        public static SessionData Session;

        public static async void Execute(string info, string filePath)
        {
            semaphore.WaitAsync();

            // Start a new SteamClient instance
            SteamClient steamClient = new();

            // Connect to Steam
            steamClient.Connect();

            // Really basic way to wait until Steam is connected
            while (!steamClient.IsConnected) await Task.Delay(500);

            // Create a new auth session
            CredentialsAuthSession authSession;
            try
            {
                authSession = await steamClient.Authentication.BeginAuthSessionViaCredentialsAsync(new AuthSessionDetails
                {
                    Username = info.Split(":")[0],
                    Password = info.Split(":")[1],
                    IsPersistentSession = false,
                    PlatformType = EAuthTokenPlatformType.k_EAuthTokenPlatformType_MobileApp,
                    ClientOSType = EOSType.Android9
                });
            }
            catch (Exception ex)
            {
                ConsoleHelper.LogException("Steam Login Error", ex);
                return;
            }

            // Starting polling Steam for authentication response
            AuthPollResult pollResponse;
            try
            {
                pollResponse = await authSession.PollingWaitForResultAsync();
            }
            catch (Exception ex)
            {
                ConsoleHelper.LogException("Steam Login Error", ex);
                return;
            }

            // Build a SessionData object
            SessionData sessionData = new()
            {
                SteamID = authSession.SteamID.ConvertToUInt64(),
                AccessToken = pollResponse.AccessToken,
                RefreshToken = pollResponse.RefreshToken,
            };

            //Login succeeded
            Session = sessionData;

            //Login succeeded
            AuthenticatorLinker linker = new(Session);

            string number = PhoneNumber.GetNumber();
            if (string.IsNullOrWhiteSpace(number)) return;

            string confirmation = await ConfirmationPhoneAndMail.Linker(linker, number, info);

            try
            {
                if (confirmation != "GeneralFailure")
                {
                    if (confirmation == "AwaitingFinalization")
                    {
                        string code = PhoneNumber.GetCode();
                        if (string.IsNullOrWhiteSpace(code))
                        {
                            ConsoleHelper.LogDanger("Phone Code is null!");

                            bool cancel = PhoneNumber.Cancel(number);
                            return;
                        }

                        ConsoleHelper.LogSuccess($"SmS Code: {code}");
                        string fileName = $"{CheckDirectory.FolderSteamGuard_Done}{info.Split(':')[0]}.mafile";

                        try
                        {
                            string sgFile = JsonConvert.SerializeObject(linker.LinkedAccount, Formatting.Indented);
                            File.WriteAllText(fileName, sgFile);
                        }
                        catch (Exception e)
                        {
                            ConsoleHelper.LogException("EXCEPTION saving maFile. For security, authenticator will not be finalized.", e);
                        }

                        AuthenticatorLinker.FinalizeResult linkeresponse = await linker.FinalizeAddAuthenticator(code);
                        if (linkeresponse == AuthenticatorLinker.FinalizeResult.Success)
                        {
                            ConsoleHelper.LogSuccess($"Added Guard Success: {info.Split(':')[0]}");

                            string dados = $"Login: {info.Split(":")[0]}\n" +
                                           $"Pass: {info.Split(":")[1]}\n" +
                                           $"Mail: {info.Split(":")[2]}\n" +
                                           $"Mail Pass: {info.Split(":")[3]}\n" +
                                           $"Phone Number: {number}\n" +
                                           $"Rcode: {linker.LinkedAccount.RevocationCode}\n" +
                                           $"CodeSMS: {code}\n" +
                                           $"CodeMail: {codeEmail}\n" +
                                           $"urlConfirmation: {urlConfirmation}\n" +
                                           $"Date Add Guard: {DateTime.Now}";

                            File.WriteAllText($"{CheckDirectory.FolderSteamGuard_Done}{info.Split(':')[0]}.txt", dados);
                            File.Delete(filePath);
                        }
                        else
                        {
                            ConsoleHelper.LogWarning($"Error to Added Guard: {linkeresponse}");
                            try
                            {
                                File.Delete(fileName);
                            }
                            catch (Exception e)
                            {
                                ConsoleHelper.LogException("Error for deleting file: ", e);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.LogException("Error: ", e);
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
