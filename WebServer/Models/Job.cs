using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebServer.Models
{
    [Table("job")]
    public class Job
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("timing")]
        [JsonPropertyName("timing")]
        public string Timing { get; set; }

        [Column("method")]
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("retention")]
        public Retention Retention => new Retention(RetentionCount, RetentionSize);

        [Column("retention_count")]
        public int RetentionCount { get; set; }

        [Column("retention_size")]
        public int RetentionSize { get; set; }

        [JsonPropertyName("sources")]
        public virtual ICollection<Source> Sources { get; set; }

        [JsonPropertyName("targets")]
        public virtual ICollection<Target> Targets { get; set; }

        public Job()
        {
            Id = 0;
            Timing = string.Empty;
            Method = string.Empty;
            RetentionCount = 0;
            RetentionSize = 0;
            Sources = new List<Source>();
            Targets = new List<Target>();
        }

        public Job(int id, string timing, string method, int retentionCount, int retentionSize, ICollection<Source> sources, ICollection<Target> targets)
        {
            Id = id;
            Timing = timing;
            Method = method;
            RetentionCount = retentionCount;
            RetentionSize = retentionSize;
            Sources = sources;
            Targets = targets;
        }
    }

    public class Retention
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("size")]
        public int Size { get; set; }

        public Retention(int count, int size)
        {
            Count = count;
            Size = size;
        }
    }
}

