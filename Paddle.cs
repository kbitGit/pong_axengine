using System;
using Aximo;
using Aximo.Engine;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Windows;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common.Input;
namespace Pong
{
    class Paddle : Actor
    {

        public static double Speed { get; set; } = 20.0;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public KeyboardState keyboard { get; set; }

        public Key Up { get; set; }
        public Key Down { get; set; }

        private CubeComponent gfx;

        public Paddle(ActorComponent component) : base(component)
        {
            setupPaddle();
        }
        public Paddle(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
            setupPaddle();
        }

        private void setupPaddle()
        {
            gfx = new CubeComponent()
            {
                Name = "gfx",
                RelativeTranslation = new Vector3(Position.X, Position.Y, 0),
                RelativeScale = new Vector3(Size.X, Size.Y, 1),
                //Material = new Material() { Color = new Vector4(1, 1, 1, 1) }
            };
            AddComponent(gfx);
        }

        public override void UpdateFrame()
        {
            var delta = Application.Current.UpdateCounter.Elapsed.Milliseconds / 1000.0;
            System.Console.WriteLine(delta);
            var KeyboardState = WindowContext.Current.Window.KeyboardState;
            if (KeyboardState.IsKeyDown(Up))
            {
                gfx.RelativeTranslation = gfx.RelativeTranslation + new Vector3(0, (float)(Speed * delta), 0);
            }
            if (KeyboardState.IsKeyDown(Down))
            {
                gfx.RelativeTranslation = gfx.RelativeTranslation - new Vector3(0, (float)(Speed * delta), 0);

            }
        }

    }
}