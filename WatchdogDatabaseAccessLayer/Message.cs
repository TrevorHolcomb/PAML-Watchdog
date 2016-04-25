using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WatchdogDatabaseAccessLayer
{
    public partial class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Server { get; set; }

        [Required]
        [StringLength(128)]
        public string Origin { get; set; }

        public int MessageTypeId { get; set; }

        public bool Processed { get; set; }

        public virtual MessageType MessageType { get; set; }

        [Required]
        [StringLength(1024)]
        public string Params { get; set; }
    }
}
