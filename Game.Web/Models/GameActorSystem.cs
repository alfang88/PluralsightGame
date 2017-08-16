using System;
using System.Threading.Tasks;
using Akka.Actor;
using Game.ActorModel.Actors;
using Game.ActorModel.ExternalSystems;

namespace Game.Web.Models
{
    public static class GameActorSystem
    {
        private static ActorSystem _actorSystem;
        private static IGameEventsPusher _gameEventsPusher;

        public static void Create()
        {
            _gameEventsPusher = new SignalRGameEventPusher();

            _actorSystem = Akka.Actor.ActorSystem.Create("GameSystem");

            ActorReferences.GameController = _actorSystem.ActorOf<GameControllerActor>();

            ActorReferences.SignalRBridge = _actorSystem.ActorOf(
                Props.Create(() => new SignalRBridgeActor(_gameEventsPusher, ActorReferences.GameController)),
                "SignalRBridge"
                );
        }

        public static void Shutdown()
        {
            AsyncShutdown().Wait(TimeSpan.FromSeconds(1));
        }

        private static async Task AsyncShutdown()
        { 
            await _actorSystem.WhenTerminated;
            Console.WriteLine("ActorSystem terminated...");
        }

        public static class ActorReferences
        {
            public static IActorRef GameController { get; set; }
            public static IActorRef SignalRBridge { get; set; }
        }
    }
}