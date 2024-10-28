using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Models
{
    [Table("source")]
    public class Source
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("Job")]
        [Column("job_id")]
        public int JobId { get; set; }
        [Column("directory")]
        public string Directory { get; set; }

        public Source()
        {
            Id = 0;
            JobId = 0;
            Directory = string.Empty;
        }

        public Source(int jobId, string directory)
        {
            Id = 0;
            JobId = jobId;
            Directory = directory;
        }
    }
}
