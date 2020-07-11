// This file is part of Pong, a Game written in C# with the Aximo Game Engine. Web: https://github.com/kbitGit/pong_axengine, https://github.com/AximoGames
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Aximo.Engine;
using Aximo.Engine.Audio;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Windows;
using OpenToolkit.Mathematics;

namespace Pong
{
    internal class Ball : Actor
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
            AddSphere(position, radius);
            direction = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble()).Normalized();
        }

        private void AddSphere(Vector2 position, float radius)
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

                var collision = false;

                if (updatedPosition.Y + (gfx.RelativeScale.Y / 2) > WorldSize.Y / 2
                    || updatedPosition.Y - (gfx.RelativeScale.Y / 2) < -WorldSize.Y / 2)
                {
                    direction.Y = -direction.Y;
                    collision = true;
                }

                gfx.RelativeTranslation = updatedPosition;

                if (FirstPlayer.CollidesWithBall(updatedPosition.Xy, gfx.RelativeScale.X / 2)
                    || SecondPlayer.CollidesWithBall(updatedPosition.Xy, gfx.RelativeScale.X / 2))
                {
                    direction.X = -direction.X;
                    collision = true;
                }

                if (collision)
                    AudioManager.Default.PlayAsync("Audio/collision.rack.json");
            }
        }

    }

}
