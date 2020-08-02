// This file is part of Pong, a Game written in C# with the Aximo Game Engine. Web: https://github.com/kbitGit/pong_axengine, https://github.com/AximoGames
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using Aximo;
using Aximo.Engine;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Components.Lights;
using Aximo.Engine.Components.UI;
using Cairo;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;

namespace Pong
{
    internal class PongGame : Application
    {
        private Vector2 CamSize;

        private Paddle Player1;
        private Paddle Player2;

        private Ball Ball;
        protected override void SetupScene()
        {
            DefaultKeyBindings = false;

            CamSize = new Vector2(18 * RenderContext.ScreenAspectRatio, 18);
            System.Console.WriteLine(CamSize);

            Player1 = new Paddle(new Vector2((-CamSize.X / 2) + 1f, 0), new Vector2(1, 3))
            {
                Up = Key.W,
                Down = Key.S,
                WorldSize = CamSize,
            };

            Player2 = new Paddle(new Vector2((CamSize.X / 2) - 1f, 0), new Vector2(1, 3))
            {
                Up = Key.Up,
                Down = Key.Down,
                WorldSize = CamSize,
            };

            Ball = new Ball(0.5f)
            {
                FirstPlayer = Player1,
                SecondPlayer = Player2,
                WorldSize = CamSize,
            };

            SceneContext.AddActor(Player1);
            SceneContext.AddActor(Player2);
            SceneContext.AddActor(Ball);
            SceneContext.AddActor(new Actor(new PointLightComponent()
            {
                Name = "StaticLight",
                RelativeTranslation = new Vector3(0, 0, 10),
                Quadric = 0.0125f,
                //Direction = new Vector3(0, 0, -1), // Let the light come from the direction of the camera
                //Color = new Vector4(1, 1, 0, 1),
            }));
            SceneContext.AddActor(new Actor(new DirectionalLightComponent()
            {
                Name = "StaticLight_",
                RelativeTranslation = new Vector3(0, 0, 10),
                Direction = new Vector3(0, 0, -1), // Let the light come from the direction of the camera
                //Color = new Vector4(1, 1, 0, 1),
                CastShadow = false,
            }));

#if DEBUG
            //UISlider slider;
            //SceneContext.AddActor(new Actor(slider = new UISlider()
            //{
            //    Size = new Vector2(200, 200),
            //    MinValue = 0,
            //    MaxValue = 1,
            //}));
            //slider.SliderValueChanged += (e) =>
            //{
            //    SceneContext.GetActor("StaticLight").GetComponent<PointLightComponent>().Quadric = e.NewValue;
            //};
#endif

            var gen = new Aximo.Generators.Voronoi.VoronoiGeneratorOptions
            {
                Color1 = new Vector4(0.4f, 0.6f, 0.6f, 1),
                Color2 = new Vector4(0.4f, 0.4f, 0.6f, 1),
                MinDelta = 50,
                Seed = 100,
                Points = 8,
                Size = new Vector2i(320, 240),
            };
            var groundMaterial = new Material()
            {
                DiffuseTexture = Texture.GetFromFile(AssetManager.GetAssetsPath("Textures/Voronoi/.png", gen)),
                //Ambient = 0.3f,
                //Shininess = 32.0f,
                //SpecularStrength = 0.5f,
                CastShadow = false,
            };
            SceneContext.AddActor(new Actor(new CubeComponent
            {
                RelativeScale = new Vector3(CamSize.X, CamSize.Y, 1),
                RelativeTranslation = new Vector3(0, 0, -1),
                Material = groundMaterial,
            }));

            SceneContext.AddActor(new Actor(new StatsComponent()));

            RenderContext.Camera = new OrthographicCamera(new Vector3(0, 0, 10))
            {
                Size = CamSize,
                NearPlane = 0.001f,
                FarPlane = 1000.0f,
                LookAt = new Vector3(0, 0, 0),
                Up = new Vector3(0, 1, 0),
            };
            RenderContext.BackgroundColor = new Vector4(0, 0, 0, 1);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (KeyboardState[Key.Escape])
                Stop();

            if (KeyboardState[Key.AltRight] && KeyboardState[Key.K])
                DefaultKeyBindings = !DefaultKeyBindings;

            if (KeyboardState[Key.Space])
            {
                Ball.StartBallMovement();
            }
        }
    }
}
