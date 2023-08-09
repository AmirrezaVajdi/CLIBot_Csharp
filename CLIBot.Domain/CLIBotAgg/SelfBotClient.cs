using CLIBot.Domain.CLIBotAgg.Exceptions;
using CLIBot.Domain.CLIBotAgg.Login;
using System;
using System.ComponentModel;
using TL;
using TL.Methods;
using WTelegram;

namespace CLIBot.Domain.CLIBotAgg
{
    public partial class SelfBotClient
    {
        public delegate Task ForwardDelegate(InputPeer fromPeer, List<InputPeer> toPeers);
        public event ForwardDelegate ForwardEvent;
        private static Client _Client;
        private static Account _account;
        private string _loginCode;
        private string _proxy = string.Empty;
        private List<int> msgId = new();

        public SelfBotClient(Account account, string proxy = null)
        {
            _account = account;
            _proxy = proxy;

            if (_Client is not null)
                Dispose();

            _Client = new(Config);
            SetProxy();
            ForwardEvent += SelfBotClient_ForwardEvent; ;
        }

        private async Task SelfBotClient_ForwardEvent(InputPeer fromPeer, List<InputPeer> toPeers)
        {
            while (true)
            {
                await StartForwarding(fromPeer, toPeers);
                if (CheckDay(GetDateTime().Hour))
                {
                    Thread.Sleep(1800000);
                }
                else
                {
                    Thread.Sleep(3600000);
                }
            }


        }
        public void Dispose()
        {
            _Client.Dispose();
        }
        public User GetUser()
        {
            return _Client.User;
        }
        public async Task StartForward()
        {
            var peer = await CheckChannelIdAndGetPeerChannel(_account.ChannelId);
            msgId = await GetBannersId(peer);
            await ScheduleForwarding(peer);
        }

        private async Task<List<int>> GetBannersId(InputPeer peer)
        {
            List<int> msgId = new();
            for (int offset_id = 0; ;)
            {
                var messages = await _Client.Messages_GetHistory(peer, offset_id);
                if (messages.Messages.Length == 0) break;
                foreach (var msgBase in messages.Messages)
                {
                    msgId.Add(msgBase.ID);
                }
                offset_id = messages.Messages[^1].ID;
            }
            return msgId;
        }
        private async Task ScheduleForwarding(InputPeer fromPeer)
        {
            var allpeers = await GetAllPeer();
            await ForwardEvent(fromPeer, allpeers);
        }
        private async Task StartForwarding(InputPeer fromPeer, List<InputPeer> toPeers)
        {

            foreach (var topeer in toPeers)
            {
                try
                {
                    await _Client.Messages_ForwardMessages(
                     from_peer: fromPeer,
                       id: msgId.ToArray(),
                       random_id: RandomLong(),
                       to_peer: topeer,
                      drop_author: true
                     );
                }
                catch (RpcException)
                {
                    continue;
                }

            }


        }
        private async Task<List<InputPeer>> GetAllPeer()
        {
            List<InputPeer> inputPeers = new();
            var chats = await _Client.Messages_GetAllDialogs();
            foreach (var (k, chat) in chats.chats)
            {
                if (chat.IsGroup)
                {
                    inputPeers.Add(chat.ToInputPeer());
                }
            }
            return inputPeers;
        }
        private async Task<InputPeer> CheckChannelIdAndGetPeerChannel(long channelId)
        {
            var result = await _Client.Messages_GetAllChats();
            var channel = result.chats[channelId];

            if (channel is null)
            {
                throw new NotFoundChanneIDExcepton();
            }

            return channel.ToInputPeer();
        }
        private long[] RandomLong()
        {
            List<long> longs = new();
            foreach (var item in msgId)
            {
                longs.Add(item);
            }
            return longs.ToArray();
        }
        private bool CheckDay(int hour)
        {
            if (hour >= 0 && hour <= 8)
            {
                return false;
            }
            return true;
        }
        private DateTime GetDateTime()
        {
            DateTime timezone = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(DateTime.UtcNow, "Iran Standard Time");
            return timezone;
        }
        private string Config(string what)
        {
            return what switch
            {
                "session_pathname" => "WTelegram.session",
                "api_id" => _account.ApiId,
                "api_hash" => _account.ApiHash,
                "phone_number" => _account.PhoneNumber,
                "verification_code" => _loginCode,
                "password" => _account.Password2FA,
                _ => null
            };
        }
        private void SetProxy()
        {
            _Client.MTProxyUrl = _proxy;
        }
    }
}