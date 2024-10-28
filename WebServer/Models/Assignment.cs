using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebServer.Models
{
    [Table("assignment")]
    public class Assignment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [ForeignKey("Computer")]
        [Column("computer_uuid")]
        public Guid ComputerUUID { get; set; }
        [ForeignKey("Job")]
        [Column("job_id")]
        public int JobId { get; set; }
        [Column("assign_at")]
        public DateTime Assign_at { get; set; }

        public virtual Computer? Computer { get; set; }
        public virtual Job? Job { get; set; }

        public Assignment()
        {
            Id = 0;
            ComputerUUID = Guid.Empty;
            JobId = 0;
            Assign_at = DateTime.Now;
            Computer = null!;
            Job = null!;
        }

        public Assignment(int id, Guid computerUUID, int jobId, DateTime assign_at, Computer computer, Job job)
        {
            Id = id;
            ComputerUUID = computerUUID;
            JobId = jobId;
            Assign_at = assign_at;
            Computer = computer;
            Job = job;
        }
    }
}
