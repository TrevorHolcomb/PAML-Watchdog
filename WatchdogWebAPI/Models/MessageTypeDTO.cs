using WatchdogDatabaseAccessLayer.Models;

namespace WatchdogWebAPI.Models
{
    public class MessageTypeDTO
    {
        public string Name { get; set; }
        public string Descrpiton { get; set; }
        
    }

    public class MessageTypeDetailDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public APIMessageParameter[] parameters { get; set; }
    }
}