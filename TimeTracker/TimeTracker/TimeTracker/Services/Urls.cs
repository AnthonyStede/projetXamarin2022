namespace TimeTracker.Services
{
    public static class Urls
    {
        public const string API = "https://timetracker.julienmialon.ovh";
        private const string ROOT = "api/v1";

        public const string REGISTER = ROOT + "/register";
        public const string LOGIN = ROOT + "/login";
        public const string USER_PROFIL = ROOT + "/me";
        public const string SET_PASSWORD = ROOT + "/password";
        public const string PROJECT_LIST = ROOT + "/projects";
        public const string TASK = "/tasks";
    }
}