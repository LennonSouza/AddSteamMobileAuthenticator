namespace AddSteamMobileAuthenticator.Functions
{
    internal class PhoneNumber
    {
        public static string GetNumber()
        {
            switch (Frm_Main._frm_Main.txt_UrlPhone.Text)
            {
                case "5sim.net":
                    //return _5Sim.GetNumber();

                case "onlinesim.io":
                    return onlinesim.GetNum();

                default:
                    return null;
            }
        }

        public static string GetCode()
        {
            switch (Frm_Main._frm_Main.txt_UrlPhone.Text)
            {
                case "5sim.net":
                    //return _5Sim.Getcode();

                case "onlinesim.io":
                    return onlinesim.GetState();

                default:
                    return null;
            }
        }

        public static bool Cancel(string number)
        {
            switch (Frm_Main._frm_Main.txt_UrlPhone.Text)
            {
                case "5sim.net":
                    //return _5Sim.GetCancel(number) ? true : false;

                default:
                    return false;
            }
        }
    }
}
