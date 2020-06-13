using System;
using Aximo.Engine;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Windows;
using OpenToolkit.Mathematics;

namespace Pong
{
    class Ball : Actor
    {
        public static float Speed = 10.0f;
        private SphereComponent gfx;

        public Paddle FirstPlayer;
        public Paddle SecondPlayer;

        public Vector2 WorldSize { get; internal set; }

        private Vector2 direction;
        private bool startMovement;

        public Ball(Vector2 position, float radius)
        {
            var rand = new Random();
            addSphere(position, radius);
            direction = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble()).Normalized();
        }

        private void addSphere(Vector2 position, float radius)
        {
            gfx = new SphereComponent(4)
            {
                RelativeTranslation = new Vector3(position.X, position.Y, 0),
                // Because Scale is the diameter, the radius need to be doubled
                RelativeScale = new Vector3(radius * 2, radius * 2, radius * 2),
            };
            AddComponent(gfx);
        }

        public void StartBallMovement()
        {
            startMovement = true;
        }
        public override void UpdateFrame()
        {
            if (startMovement)
            {
                var delta = Application.Current.UpdateCounter.Elapsed.Milliseconds / 1000.0f;
                var movement = direction * Speed * delta;
                var updatedPosition = gfx.RelativeTranslation + new Vector3(movement.X, movement.Y, 0);
                if (updatedPosition.Y + gfx.RelativeScale.Y / 2 > WorldSize.Y / 2
                || updatedPosition.Y - gfx.RelativeScale.Y / 2 < -WorldSize.Y / 2)
                {
                    direction.Y = -direction.Y;
                }
                gfx.RelativeTranslation = updatedPosition;
                if (FirstPlayer.CollidesWithBall(updatedPosition.Xy, gfx.RelativeScale.X / 2)
                || SecondPlayer.CollidesWithBall(updatedPosition.Xy, gfx.RelativeScale.X / 2))
                {
                    direction.X = -direction.X;
                }

            }


        }
    }
}