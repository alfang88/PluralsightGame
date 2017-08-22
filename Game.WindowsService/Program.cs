using System;
using System.Threading.Tasks;
using Akka.Actor;
using Game.ActorModel.Actors;
using Topshelf;

namespace Game.WindowsService
{
    public class GameStateService
    {
        private ActorSystem _actorSystemInstance;

        public void Start()
        {
            _actorSystemInstance = ActorSystem.Create("GameSystem");

            var gameController = _actorSystemInstance.ActorOf<GameControllerActor>("GameController");
        }

        public void Stop()
        {
            AsyncStop().Wait(TimeSpan.FromSeconds(2));
        }

        private async Task AsyncStop()
        {
            await _actorSystemInstance.Terminate();
            Console.WriteLine("ActorSystem terminated...");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(gameService =>
            {
                gameService.Service<GameStateService>(s =>
                {
                    s.ConstructUsing(game => new GameStateService());
                    s.WhenStarted(game => game.Start());
                    s.WhenStopped(game => game.Stop());
                });

                gameService.RunAsLocalSystem();
                gameService.StartAutomatically();

                gameService.SetDescription("PSDemo Game Topshelf Service");
                gameService.SetDisplayName("PSDemoGame");
                gameService.SetServiceName("PSDemoGame");
            });

        }
    }
}
