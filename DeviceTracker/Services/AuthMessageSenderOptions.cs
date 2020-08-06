using System;
namespace DeviceTracker.Services
{
    public class AuthMessageSenderOptions
    {
        public string SendGridFrom { get; set; }
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}
