namespace Cards.WEB
{
    public static class GlobalSettings
    {
        static string baseServerEndpoint;

        static string version;
        static GlobalSettings()
        {
            baseServerEndpoint = "https://mazaopesa.azurewebsites.net/";

            //baseServerEndpoint = "https://localhost:44313/";

            version = "v1";
        }

        public static string Username = "";

        public static string Password = "";

        //Authentication
        public static string AuthEndpoint => $"{baseServerEndpoint}{version}/auth/token";

       
        //SMS
        public static string GetTextAlertEndpoint => $"{baseServerEndpoint}{version}/messages/textAlerts?";
        public static string CreateTextAlertEndpoint => $"{baseServerEndpoint}{version}/messages/createTextAlert";
        public static string GetTextAlertByIdEndpoint => $"{baseServerEndpoint}{version}/messages/textAlert/";
        public static string EditTextAlertEndpoint => $"{baseServerEndpoint}{version}/messages/editTextAlert";

        //Email
        public static string GetEmailAlertsEndpoint => $"{baseServerEndpoint}{version}/messages/emailAlerts?";
        public static string CreateEmailAlertEndpoint => $"{baseServerEndpoint}{version}/messages/createEmailAlert";
        public static string GetEmailAlertByIdEndpoint => $"{baseServerEndpoint}{version}/messages/emailAlert/";
        public static string EditEmailAlertEndpoint => $"{baseServerEndpoint}{version}/messages/editEmailAlert";

    }
}
