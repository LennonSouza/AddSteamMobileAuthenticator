using AddSteamMobileAuthenticator.Utils;
using MailKit.Net.Pop3;
using MailKit.Security;
using MimeKit;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace AddSteamMobileAuthenticator.Functions
{
    public class GetMail
    {
        public static bool GetCode(string acc, string typeRequest)
        {
            int counterMax = 2;
            for (int i = 0; i < counterMax; i++)
            {
                try
                {
                    Waiter.waitForSec(5);

                    using (Pop3Client client = new())
                    {
                        string mail = Frm_Main._frm_Main.txt_UrlMail.Text, email = acc.Split(":")[2], pass = acc.Split(":")[3];
                       
                        client.Connect($"pop.{mail}", 995, SecureSocketOptions.SslOnConnect);
                        client.Authenticate(email, pass);

                        MimeMessage message = client.GetMessage(client.Count - 1);

                        if (typeRequest == "url")
                        {
                            Regex regexRequest = null;
                            if (message.TextBody.Contains("Add Phone Number"))
                            {
                                regexRequest = new Regex("(Add Phone Number: (.*?)\r\n\r\n<)"); //Ingles
                            }
                            else
                            {
                                regexRequest = new Regex("(Adicionar número de telefone: (.*?)\r\n\r\n<)"); // Portugues
                            }

                            string matchRequest = regexRequest.Match(message.TextBody).Groups[2].Value;
                            if (!string.IsNullOrWhiteSpace(matchRequest))
                            {
                                AddMobile.urlConfirmation = matchRequest;

                                HttpClient clientHttp = new();

                                try
                                {
                                    HttpResponseMessage response = clientHttp.GetAsync(matchRequest).Result;
                                    if (response.IsSuccessStatusCode)
                                    {
                                        string responseBody = response.Content.ReadAsStringAsync().Result;

                                        if (responseBody.Contains("The phone can now be added to your account.")) return true;
                                        else if (responseBody.Contains("The link you clicked has expired. Please try again with a new link from a new email.")) ConsoleHelper.LogWarning("The link you clicked has expired. Please try again with a new link from a new email.");
                                        else ConsoleHelper.LogWarning("Email code not found.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Erro na solicitação HTTP. Código de status: " + response.StatusCode);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Erro ao fazer a solicitação HTTP: " + ex.Message);
                                }
                                finally
                                {
                                    client.Dispose();
                                }
                            }
                        }
                        else
                        {
                            string code = new Regex("\\w{5}(?=											<\\/td>)").Match(message.HtmlBody.ToString()).Value;

                            // TODO
                        }
                        client.Disconnect(true);
                    }
                }
                catch (Exception ex)
                {
                    ConsoleHelper.LogException($"Erro ao conectar: ", ex);
                }
            }
            return false;
        }
    }
}
