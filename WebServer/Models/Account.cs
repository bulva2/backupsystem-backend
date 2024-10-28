using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Models
{
    [Table("account")]
    public class Account
    {
        [Key]
        [Column("id")]
        public int ID { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role")]
        public string Role { get; set; }

        [Column("token")]
        public string? Token { get; set; }

        public Account()
        {
            ID = 0;
            Username = string.Empty;
            Password = string.Empty;
            Role = string.Empty;
            Token = null;
        }
    }
}
