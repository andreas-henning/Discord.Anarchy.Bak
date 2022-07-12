namespace Discord
{
    public class LockedDiscordConfig
    {
        public SuperProperties SuperProperties { get; private set; }
        public uint RestConnectionRetries { get; private set; }
        public uint ApiVersion { get; private set; }
        public bool RetryOnRateLimit { get; private set; }

        public LockedDiscordConfig(ApiConfig config)
        {
            SuperProperties = config.SuperProperties;
            RestConnectionRetries = config.RestConnectionRetries;
            ApiVersion = config.ApiVersion;
            RetryOnRateLimit = config.RetryOnRateLimit;
        }
    }
}
