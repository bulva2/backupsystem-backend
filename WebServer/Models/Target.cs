using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Models
{
    [Table("target")]
    public class Target
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("Job")]
        [Column("job_id")]
        public int JobId { get; set; }
        [Column("directory")]
        public string Directory { get; set; }

        public Target()
        {
            Id = 0;
            JobId = 0;
            Directory = string.Empty;
        }

        public Target(int jobId, string directory)
        {
            Id = 0;
            JobId = jobId;
            Directory = directory;
        }
    }
}
