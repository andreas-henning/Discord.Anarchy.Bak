using System.Text.Json.Serialization;

namespace Discord
{
    public class DiscordProfileUser : DiscordUser
    {
        [JsonConstructor]
        public DiscordProfileUser(ulong id, string username, uint discriminator, string _avatarHash, DiscordBadge _publicFlags, DiscordBadge _flags, bool _bot, string biography, string _bannerHash)
            : base(id, username, discriminator, _avatarHash, _publicFlags, _flags, _bot)
        {
            Biography = biography;
            this._bannerHash = _bannerHash;
        }

        [JsonPropertyName("bio")]
        public string Biography { get; private set; }

        [JsonPropertyName("banner")]
        private readonly string _bannerHash;
        public DiscordCDNImage Banner => _bannerHash == null ? null : new DiscordCDNImage(CDNEndpoints.Banner, Id, _bannerHash);
    }
}
