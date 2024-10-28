using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Models
{
    [Table("report")]
    public class Report
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("computer_uuid")]
        public Guid UUID { get; set; }
        [Column("action")]
        public string Action { get; set; }

        public Report() 
        {
            Id = 0;
            UUID = Guid.Empty;
            Action = string.Empty;
        }


        public Report(int id, Guid uuid, string action)
        {
            Id = id;
            UUID = uuid;
            Action = action;
        }
    }
}
