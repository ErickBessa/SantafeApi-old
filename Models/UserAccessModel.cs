namespace SantafeApi.Models
{
    public class UserAccessModel
    {
        public int CodCliente { get; set; }
        public string UserId { get; set; }
        public bool HasAccess { get; set; }
    }
}