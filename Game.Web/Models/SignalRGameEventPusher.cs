using Game.ActorModel.ExternalSystems;
using Microsoft.AspNet.SignalR;

namespace Game.Web.Models
{
    public class SignalRGameEventPusher : IGameEventsPusher
    {
        private static readonly IHubContext GameHubContext;

        static SignalRGameEventPusher()
        {
            GameHubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>();
        }

        public void PlayerJoined(string playerName, int playerHealth)
        {
            GameHubContext.Clients.All.playerJoined(playerName, playerHealth);
        }

        public void UpdatePlayerHealth(string playerName, int playerHealth)
        {
            GameHubContext.Clients.All.updatePlayerHealth(playerName, playerHealth);
        }
    }
}