// This file is part of Pong, a Game written in C# with the Aximo Game Engine. Web: https://github.com/kbitGit/pong_axengine, https://github.com/AximoGames
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Aximo;
using Aximo.Engine;
using Aximo.Engine.Audio;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Windows;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common.Input;

namespace Pong
{
    internal class Ball : Actor
    {
        public static float Speed = 23.0f;
        private SphereComponent gfx;

        public Paddle FirstPlayer;
        public Paddle SecondPlayer;

        public Vector2 WorldSize { get; internal set; }

        private Vector2 direction;
        private bool StartMovement;

        public Ball(float radius)
        {
            AddSphere(radius);
            Reset();
        }

        public void Reset()
        {
            StartMovement = false;
            var rand = new Random();
            direction = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble()).Normalized();
            //direction = new Vector2(1, 0); // Debug
            gfx.RelativeTranslation = new Vector3(-5, 0, 0);
        }

        private void AddSphere(float radius)
        {
            gfx = new SphereComponent(4)
            {
                Name = "Ball",
                // Because Scale is the diameter, the radius need to be doubled
                RelativeScale = new Vector3(radius * 2, radius * 2, radius * 2),
                Material = new Material
                {
                    Color = new Vector4(0.55f, 0.20f, 0.20f, 1),
                    Ambient = 0.3f,
                    Shininess = 32.0f,
                    SpecularStrength = 0.5f,
                    CastShadow = true,
                },
            };
            AddComponent(gfx);
        }

        public void StartBallMovement()
        {
            StartMovement = true;
        }
        public override void UpdateFrame()
        {
            var keyboardState = WindowContext.Current.Window.KeyboardState;
            if (keyboardState.IsKeyDown(Key.R))
                Reset();

            if (StartMovement)
            {
                var speed = Speed;
                if (keyboardState[Key.ControlLeft])
                    speed = speed * 0.1f;

                var delta = (float)Application.Current.UpdateCounter.Elapsed.TotalMilliseconds / 1000.0f;
                var movement = direction * speed * delta;
                var updatedPosition = gfx.RelativeTranslation + new Vector3(movement.X, movement.Y, 0);

                var collision = false;
                var ballRadius = gfx.RelativeScale.X / 2;

                if (updatedPosition.X + ballRadius > WorldSize.X / 2
                    || updatedPosition.X - ballRadius < -WorldSize.X / 2)
                {
                    Reset();
                    return;
                }

                if (updatedPosition.Y + ballRadius > WorldSize.Y / 2
                    || updatedPosition.Y - ballRadius < -WorldSize.Y / 2)
                {
                    direction.Y = -direction.Y;
                    collision = true;
                }

                bool firstPlayCollision = FirstPlayer.CollidesWithBall(updatedPosition.Xy, ballRadius);
                bool secondPlayerCollision = SecondPlayer.CollidesWithBall(updatedPosition.Xy, ballRadius);
                if (firstPlayCollision || secondPlayerCollision)
                {
                    if ((firstPlayCollision && direction.X < 0) || (secondPlayerCollision && direction.X > 0))
                    {
                        var player = firstPlayCollision ? FirstPlayer : SecondPlayer;
                        direction.X = -direction.X;
                        collision = true;

                        var normalizedBounds = player.Bounds;
                        normalizedBounds.Center = Vector2.Zero;
                        var translatedDiff = player.Bounds.Center.Y;

                        var translatedBallPos = gfx.RelativeTranslation.Y - translatedDiff;
                        var scaledPaddleCollisionPos = translatedBallPos / player.Bounds.HalfSize.Y; // 0..1
                        scaledPaddleCollisionPos *= 0.5f;
                        var posY = AxMath.SinNorm(scaledPaddleCollisionPos / 4f);
                        var posX = AxMath.CosNorm(scaledPaddleCollisionPos / 4f);
                        var pos = new Vector2(posX, posY).Normalized();
                        var newDirX = AxMath.SetSign(pos.X, direction.X);
                        //var newDirY = AxMath.SetSign(pos.Y, direction.Y);
                        direction = new Vector2(newDirX, pos.Y);
                    }
                }

                if (collision)
                    AudioManager.Default.PlayAsync("Audio/collision.rack.json");
                else
                    gfx.RelativeTranslation = updatedPosition;
            }
        }

    }

}
