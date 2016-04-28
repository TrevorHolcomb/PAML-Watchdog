using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchdogDatabaseAccessLayer
{
    public partial class Message
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Server { get; set; }

        [Required]
        [StringLength(128)]
        public string Origin { get; set; }

        [Required]
        [ForeignKey("MessageType")]
        public int MessageTypeId { get; set; }

        [Required]
        public bool Processed { get; set; }

        public virtual MessageType MessageType { get; set; }

        [Required]
        [StringLength(1024)]
        public string Params { get; set; }
    }
}
