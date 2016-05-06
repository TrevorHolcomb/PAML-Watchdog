namespace WatchdogWebAPI.Models
{
    public class MessageTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrpiton { get; set; }
        
    }

    public class MessageTypeDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descrpiton { get; set; }
        public string Schema { get; set; }
    }
}