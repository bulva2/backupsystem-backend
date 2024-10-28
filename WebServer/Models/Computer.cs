using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Models
{
    [Table("computer")]
    public class Computer
    {
        [Key]
        [Column("uuid")]
        public Guid UUID { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("enabled")]
        public bool Enabled { get; set; }
    
        public Computer()
        {
            UUID = Guid.Empty;
            Name = string.Empty;
            Enabled = false;
        }

        public Computer(Guid uuid, string name, bool enabled)
        {
            UUID = uuid;
            Name = name;
            Enabled = enabled;
        }
    }
}
