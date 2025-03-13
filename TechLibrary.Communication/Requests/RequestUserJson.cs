namespace TechLibrary.Communication.Requests
{
    public class RequestUserJson
    {
        // by default these strings will be null, this could cause errors when used in the controller
        // to avoid this, we set them to empty strings
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
