namespace VirtualCatalogAPI.Models.Auth
{
    public class SessionStatus
    {
        public bool IsActive { get; set; }
        public string Status { get; set; }

        public SessionStatus(bool isActive, string status)
        {
            IsActive = isActive;
            Status = status;
        }
    }
}
