using System.Net;

namespace AddSteamMobileAuthenticator.Functions.SteamAuth
{
    internal class CookieAwareWebClient : WebClient
    {
        public CookieContainer CookieContainer { get; set; } = new();
        public CookieCollection ResponseCookies { get; set; } = new();

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest)base.GetWebRequest(address);
            request.CookieContainer = CookieContainer;
            return request;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            var response = (HttpWebResponse)base.GetWebResponse(request);
            this.ResponseCookies = response.Cookies;
            return response;
        }
    }
}
