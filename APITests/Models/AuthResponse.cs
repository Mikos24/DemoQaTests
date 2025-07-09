namespace SeleniumTests.APITests.Models
{
    public class AuthResponse
    {
        public string token { get; set; }
        public string expires { get; set; }
        public string status { get; set; }
        public string result { get; set; }
    }
}