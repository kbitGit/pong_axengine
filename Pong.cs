using System.Collections.Generic;
using Aximo;
using Aximo.Engine;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Components.Lights;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
using OpenToolkit.Windowing.Common.Input;

namespace Pong
{
    class PongGame : Application
    {
        private Vector2 camSize;

        private Paddle player1;
        private Paddle player2;
        protected override void SetupScene()
        {
            DefaultKeyBindings = false;

            camSize = new Vector2(18 * RenderContext.ScreenAspectRatio, 18);
            System.Console.WriteLine(camSize);

            player1 = new Paddle(new Vector2(-camSize.X / 2 + 0.5f, 0), new Vector2(1, 3) * new Vector2(0.5f))
            {
                Up = Key.W,
                Down = Key.S
            };

            player2 = new Paddle(new Vector2(camSize.X / 2 - 0.5f, 0), new Vector2(1, 3) * new Vector2(0.5f))
            {
                Up = Key.Up,
                Down = Key.Down
            };

            SceneContext.AddActor(player1);
            SceneContext.AddActor(player2);
            SceneContext.AddActor(new Actor(new DirectionalLightComponent()
            {
                Name = "StaticLight",
                RelativeTranslation = new Vector3(0, 0, 10),
                Direction = new Vector3(0, 0, -1), // Let the light come from the direction of the camera
            }));
            RenderContext.Camera = new OrthographicCamera(new Vector3(0, 0, 10))
            {
                Size = camSize,
                NearPlane = 0.001f,
                FarPlane = 1000.0f,
                LookAt = new Vector3(0, 0, 0),
                Up = new Vector3(0, 1, 0),
            };
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {

        }
    }
}
