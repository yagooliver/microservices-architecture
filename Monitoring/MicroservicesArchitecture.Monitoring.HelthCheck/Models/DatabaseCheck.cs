namespace TelenetBusiness.Safepoint.Healthcheck.Models
{
    /// <summary>
    /// Db check
    /// </summary>
    public class DatabaseCheck
    {
        #region Public Properties

        public string Name { get; set; }
        public string ConnectionString { get; set; }

        #endregion Public Properties
    }
}