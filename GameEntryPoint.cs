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
            var config = new RenderApplicationConfig
            {

                WindowTitle = "Pong",
                WindowSize = new Vector2i(1920, 1080),
                WindowBorder = WindowBorder.Resizable,
                IsMultiThreaded = true,
                RenderFrequency = 0,
                UpdateFrequency = 0,
                IdleRenderFrequency = 0,
                IdleUpdateFrequency = 0,
                VSync = VSyncMode.Off,
                // UseGtkUI = true,
                UseConsole = true,
            };
            new GameStartup<PongGame, GtkUI>(config).Start();
        }
    }
}
