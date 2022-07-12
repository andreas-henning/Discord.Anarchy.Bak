﻿namespace Discord
{
    public class ApiConfig
    {
        public SuperProperties SuperProperties { get; set; } = new SuperProperties();
        public uint RestConnectionRetries { get; set; } = 0;
        public uint ApiVersion { get; set; } = 9;
        public bool RetryOnRateLimit { get; set; } = true;
    }
}
