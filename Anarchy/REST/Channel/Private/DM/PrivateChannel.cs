﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord.Gateway;
using Discord.Voice;
using Newtonsoft.Json;

namespace Discord
{
    /// <summary>
    /// Represents a <see cref="DiscordChannel"/> specific to direct message channels
    /// </summary>
    public class PrivateChannel : DiscordChannel, IMessageChannel
    {
        public PrivateChannel()
        {
            OnClientUpdated += (sender, e) => Recipients.SetClientsInList(Client);
        }

        [JsonProperty("recipients")]
        internal List<DiscordUser> _recipients;


        public IReadOnlyList<DiscordUser> Recipients
        {
            get
            {
                return _recipients;
            }
        }


        [JsonProperty("last_message_id")]
        public ulong? LastMessageId { get; internal set; }


        protected void Update(PrivateChannel channel)
        {
            base.Update(channel);
            _recipients = channel._recipients;
        }


        public new async Task UpdateAsync()
        {
            Update((await Client.GetChannelAsync(Id)).ToDMChannel());
        }

        /// <summary>
        /// Updates the channel's info
        /// </summary>
        public new void Update()
        {
            UpdateAsync().GetAwaiter().GetResult();
        }


        public async Task LeaveAsync()
        {
            await Client.DeleteChannelAsync(Id);
        }

        /// <summary>
        /// Closes the DM
        /// </summary>
        public void Leave()
        {
            LeaveAsync().GetAwaiter().GetResult();
        }


        public async Task ChangeCallRegionAsync(string regionId)
        {
            await Client.ChangePrivateCallRegionAsync(Id, regionId);
        }

        public void ChangeCallRegion(string regionId)
        {
            ChangeCallRegionAsync(regionId).GetAwaiter().GetResult();
        }


        public async Task<DiscordVoiceSession> JoinAsync(bool muted = false, bool deafened = false)
        {
            if (Client.GetType() == typeof(DiscordSocketClient))
                return await ((DiscordSocketClient)Client).JoinVoiceChannelAsync(null, Id, muted, deafened);
            else
                throw new NotSupportedException("Only DiscordSocketClients can join voice channels");
        }

        public DiscordVoiceSession Join(bool muted = false, bool deafened = false)
        {
            return JoinAsync(muted, deafened).GetAwaiter().GetResult();
        }


        #region messages
        public async Task TriggerTypingAsync()
        {
            await Client.TriggerTypingAsync(Id);
        }

        /// <summary>
        /// Triggers a 'user typing...'
        /// </summary>
        public void TriggerTyping()
        {
            TriggerTypingAsync().GetAwaiter().GetResult();
        }


        public async Task<DiscordMessage> SendMessageAsync(string message, bool tts = false, DiscordEmbed embed = null)
        {
            return await Client.SendMessageAsync(Id, message, tts, embed);
        }

        /// <summary>
        /// Sends a message to the channel
        /// </summary>
        /// <param name="message">Content of the message</param>
        /// <param name="tts">Whether the message should be TTS or not</param>
        /// <returns>The message</returns>
        public DiscordMessage SendMessage(string message, bool tts = false, DiscordEmbed embed = null)
        {
            return SendMessageAsync(message, tts, embed).GetAwaiter().GetResult();
        }


        public async Task<DiscordMessage> SendFileAsync(string fileName, byte[] fileData, string message = null, bool tts = false)
        {
            return await Client.SendFileAsync(Id, fileName, fileData, message, tts);
        }

        public DiscordMessage SendFile(string fileName, byte[] fileData, string message = null, bool tts = false)
        {
            return SendFileAsync(fileName, fileData, message, tts).GetAwaiter().GetResult();
        }


        public async Task<DiscordMessage> SendFileAsync(string filePath, string message = null, bool tts = false)
        {
            return await Client.SendFileAsync(Id, filePath, message, tts);
        }

        public DiscordMessage SendFile(string filePath, string message = null, bool tts = false)
        {
            return SendFileAsync(filePath, message, tts).GetAwaiter().GetResult();
        }


        public async Task DeleteMessagesAsync(List<ulong> messages)
        {
            await Client.DeleteMessagesAsync(Id, messages);
        }

        /// <summary>
        /// Bulk deletes messages (this is a bot only endpoint)
        /// </summary>
        /// <param name="channelId">ID of the channel</param>
        /// <param name="messages">IDs of the messages</param>
        public void DeleteMessages(List<ulong> messages)
        {
            DeleteMessagesAsync(messages).GetAwaiter().GetResult();
        }


        public async Task<IReadOnlyList<DiscordMessage>> GetMessagesAsync(MessageFilters filters = null)
        {
            return await Client.GetChannelMessagesAsync(Id, filters);
        }

        /// <summary>
        /// Gets a list of messages from the channel
        /// </summary>
        /// <param name="filters">Options for filtering out messages</param>
        public IReadOnlyList<DiscordMessage> GetMessages(MessageFilters filters = null)
        {
            return GetMessagesAsync(filters).GetAwaiter().GetResult();
        }


        public async Task<IReadOnlyList<DiscordMessage>> GetPinnedMessagesAsync()
        {
            return await Client.GetPinnedMessagesAsync(Id);
        }

        /// <summary>
        /// Gets the channel's pinned messages
        /// </summary>
        public IReadOnlyList<DiscordMessage> GetPinnedMessages()
        {
            return GetPinnedMessagesAsync().GetAwaiter().GetResult();
        }


        public async Task PinMessageAsync(ulong messageId)
        {
            await Client.PinMessageAsync(Id, messageId);
        }

        /// <summary>
        /// Pins a message to the channel
        /// </summary>
        /// <param name="messageId">ID of the message</param>
        public void PinMessage(ulong messageId)
        {
            PinMessageAsync(messageId).GetAwaiter().GetResult();
        }


        public async Task UnpinMessageAsync(ulong messageId)
        {
            await Client.UnpinChannelMessageAsync(Id, messageId);
        }

        /// <summary>
        /// Unpins a message from the channel
        /// </summary>
        /// <param name="messageId">ID of the message</param>
        public void UnpinMessage(ulong messageId)
        {
            UnpinMessageAsync(messageId).GetAwaiter().GetResult();
        }
        #endregion
    }
}
