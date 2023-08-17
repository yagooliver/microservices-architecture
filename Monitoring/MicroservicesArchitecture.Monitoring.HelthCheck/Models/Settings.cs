using MicroservicesArchitecture.Monitoring.HelthCheck.Models;

namespace TelenetBusiness.Safepoint.Healthcheck.Models
{
    /// <summary>
    /// config settings
    /// </summary>
    public class Settings
    {
        #region Public Properties
        public EventBus EventBus { get; set; }
        public IEnumerable<UrlCheck> UrlChecks { get; set; }
        public IEnumerable<DatabaseCheck> DatabaseChecks { get; set; }
        public string RedisConnectionString { get; set; }
        public string StatusUri { get; set; }

        #endregion Public Properties
    }
}