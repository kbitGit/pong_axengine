using Aximo.Engine;
using Aximo.Engine.Windows;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;

namespace Pong
{
    class GameEntryPoint
    {
        static void Main(string[] args)
        {

            var game = new PongGame();
            game.Start(new ApplicationConfig()
            {
                WindowSize = new Vector2i(1920, 1080),

            });
        }
    }
}
