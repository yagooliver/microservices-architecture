namespace TelenetBusiness.Safepoint.Healthcheck.Models
{
    public class Webhook
    {
        public string Name { get; set; }
        public string Uri { get; set; }
        public string Payload { get; set; }
        public string RestoredPayload { get; set; }
        public string Filter { get; set; }
    }
}