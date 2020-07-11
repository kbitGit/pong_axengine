// This file is part of Pong, a Game written in C# with the Aximo Game Engine. Web: https://github.com/kbitGit/pong_axengine, https://github.com/AximoGames
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Aximo.Engine;
using Aximo.Engine.Windows;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;

namespace Pong
{

    internal class GameEntryPoint
    {
        private static void Main(string[] args)
        {

            var game = new PongGame();
            game.Start(new ApplicationConfig()
            {
                WindowSize = new Vector2i(1920, 1080),

            });
        }
    }

}
