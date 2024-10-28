namespace WebServer.Models
{
    public class Token
    {
        public string TokenString { get; set; }

        public Token(string tokenString)
        {
            TokenString = tokenString;
        }
    }
}
