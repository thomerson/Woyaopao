namespace Thomerson.Wechat.JSSDK.Models
{
    public class WXShare
    {
        public string appId { get; set; }
        public string nonce { get; set; }
        public string timestamp { get; set; }
        public string signature { get; set; }
        public string ticket { get; set; }
        public string url { get; set; }

        public string result { get; set; }
        public bool useCache { get; set; }
    }
}