using System.ComponentModel.DataAnnotations;

namespace WatchdogDatabaseAccessLayer
{
    public class AlertType
    {
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        [StringLength(256)]
        public string Description { get; set; }
    }
}