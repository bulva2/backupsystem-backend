namespace WebServer.Models
{
    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public Credentials()
        {
            Username = string.Empty;
            Password = string.Empty;
        }
        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
