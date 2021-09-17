namespace SantafeApi.Models
{
    public class UserModel
    {
        public int CodCliente { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public bool HasAccess { get; set; }

    }
}