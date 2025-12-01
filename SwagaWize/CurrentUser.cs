namespace FitnessCenterApp
{
    public static class CurrentUser
    {
        public static string Role { get; set; } = string.Empty;
        public static string Username { get; set; } = string.Empty;
        public static int? ClientID { get; set; }
        public static bool IsLoggedIn => !string.IsNullOrEmpty(Role);
    }
}