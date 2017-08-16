using System;
using System.Threading.Tasks;
using Akka.Actor;
using Game.ActorModel.Actors;

namespace Game.Web.Models
{
    public static class GameActorSystem
    {
        private static ActorSystem ActorSystem;

        public static void Create()
        {
            ActorSystem = Akka.Actor.ActorSystem.Create("GameSystem");

            ActorReferences.GameController = ActorSystem.ActorOf<GameControllerActor>();
        }

        public static void Shutdown()
        {
            AsyncShutdown().Wait(TimeSpan.FromSeconds(1));
        }

        private static async Task AsyncShutdown()
        { 
            await ActorSystem.WhenTerminated;
        }

        public static class ActorReferences
        {
            public static IActorRef GameController { get; set; }
        }
    }
}