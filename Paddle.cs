using System;
using Aximo;
using Aximo.Engine;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Windows;
using OpenToolkit.Mathematics;

namespace Pong
{
    class Paddle : Actor
    {
        public Vector2 Position { get; set; }
        public Vector3 Size { get; set; }

        public Paddle(ActorComponent component) : base(component)
        {
            setupPaddle();
        }
        public Paddle(Vector2 position, Vector3 size)
        {
            Position = position;
            Size = size;
            setupPaddle();
        }

        private void setupPaddle()
        {
            AddComponent(new CubeComponent()
            {
                Name = "gfx",
                RelativeTranslation = new Vector3(Position.X, 0, Position.Y),
                RelativeScale = Size,
                //Material = new Material() { Color = new Vector4(1, 1, 1, 1) }
            });
        }
    }
}