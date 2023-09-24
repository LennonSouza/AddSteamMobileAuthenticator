using AddSteamMobileAuthenticator.Utils;
using Newtonsoft.Json;
using System.Text;

namespace AddSteamMobileAuthenticator.Functions
{
    internal class onlinesim
    {
        private static int OrderID = 0;
        public static string GetNum()
        {
            var payload = new
            {
                //reject = new string[] { "91", "92", "94", "95", "96", "97", "98", "99", "90", "938", "937", "936", "931", "499", "202", "358", "190" } RUSSIA
                reject = new string[] { "912", "950", "920" } // Philipinas
            };

            string url = $"https://{Frm_Main._frm_Main.txt_UrlPhone.Text}/api/getNum.php?apikey={Frm_Main._frm_Main.txt_API.Text}&service=steam&country=63&number=true";

            HttpClient client = new();

            // Converter o objeto anônimo em JSON
            string payloadJson = JsonConvert.SerializeObject(payload);

            HttpContent content = new StringContent(payloadJson, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = Task.Run(() => client.PostAsync(url, content)).Result;
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    NumObj? getNum = JsonConvert.DeserializeObject<NumObj>(responseBody);
                    if (getNum != null)
                    {
                        if (getNum.response == 1)
                        {
                            OrderID = getNum.tzid;
                            ConsoleHelper.LogSuccess($"Number: {getNum.number}");
                            return getNum.number;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Erro na solicitação HTTP. Código de status: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Trate exceções aqui, se ocorrerem
                Console.WriteLine($"Erro na requisição: {ex.Message}");
            }
            return null;
        }

        public static string GetState()
        {
            int counter = 6;
            for (int i = 0; i < counter; i++)
            {
                try
                {
                    // Crie uma instância do HttpClient
                    using (HttpClient client = new())
                    {
                        // Construa a URL com os parâmetros
                        string url = $"https://{Frm_Main._frm_Main.txt_UrlPhone.Text}/api/getState.php?apikey={Frm_Main._frm_Main.txt_API.Text}";
                        string queryString = $"tzid={OrderID}&message_to_code=1";
                        string fullUrl = $"{url}?{queryString}";

                        // Faça a requisição GET de forma síncrona
                        HttpResponseMessage response = client.GetAsync(fullUrl).Result;

                        // Verifique se a resposta foi bem-sucedida
                        if (response.IsSuccessStatusCode)
                        {
                            // Leitura do conteúdo da resposta como uma string
                            string responseContent = response.Content.ReadAsStringAsync().Result;

                            // Deserialize a resposta em uma lista de objetos StateObj
                            List<StateObj>? getState = JsonConvert.DeserializeObject<List<StateObj>>(responseContent);

                            // Verifique se getState não está vazio
                            if (getState.Count > 0)
                            {
                                // Verifique a resposta do primeiro objeto
                                if (getState[0].response == "TZ_NUM_WAIT")
                                {
                                    ConsoleHelper.LogWarning("Your code was not found, I will try in 30 seconds...");
                                }
                                else if (getState[0].response == "TZ_NUM_ANSWER")
                                {
                                    return getState[0].msg;
                                }
                            }

                            // Faça algo com o conteúdo da resposta
                            Console.WriteLine(responseContent);
                        }
                        else
                        {
                            // Trate erros de resposta aqui, se necessário
                            Console.WriteLine($"Error request: {response.StatusCode}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Trate exceções aqui, se ocorrerem
                    Console.WriteLine($"Error request: {ex.Message}");
                }
                Waiter.waitForSec(30);
            }
            return null;
        }

        public class NumObj
        {
            public int response { get; set; }
            public int tzid { get; set; }
            public string number { get; set; }
            public int country { get; set; }
            public int time { get; set; }
            public string service { get; set; }
            public string title { get; set; }
            public string response_text { get; set; }
        }

        public class StateObj
        {
            public int country { get; set; }
            public double sum { get; set; }
            public string service { get; set; }
            public string number { get; set; }
            public object webhook_url { get; set; }
            public string response { get; set; }
            public int tzid { get; set; }
            public int time { get; set; }
            public string form { get; set; }
            public string msg { get; set; }
        }
    }
}
