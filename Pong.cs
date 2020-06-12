using System.Collections.Generic;
using Aximo;
using Aximo.Engine;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Components.Lights;
using OpenToolkit.Mathematics;
using OpenToolkit.Windowing.Common;
namespace Pong
{
    class PongGame : Application
    {
        private Vector2 camSize;

        private Paddle player1;
        private Paddle player2;
        protected override void SetupScene()
        {


            camSize = new Vector2(9 * RenderContext.ScreenAspectRatio, 9);
            System.Console.WriteLine(camSize);
            player1 = new Paddle(new Vector2(-camSize.X / 2 + 0.5f, 0), new Vector3(1, 1, 3) * new Vector3(0.5f));
            player2 = new Paddle(new Vector2(camSize.X / 2 - 0.5f, 0), new Vector3(1, 1, 3) * new Vector3(0.5f));
            SceneContext.AddActor(player1);
            SceneContext.AddActor(player2);
            SceneContext.AddActor(new Actor(new DirectionalLightComponent()
            {
                //RelativeScale = new Vector3(3, 3, 3),
                Name = "StaticLight",
                RelativeTranslation = new Vector3(0, 0, 10),
                Direction = new Vector3(0, 1, 0), // Let the light come from the direction of the camera
                //RelativeRotation = new Vector3(0, -90, 0).ToQuaternion()
            }));
            /*RenderContext.Camera = new OrthographicCamera(new Vector3(0, -3, 0))
            {
                Size = camSize,
                NearPlane = 0.01f,
                FarPlane = 100.0f,
                LookAt = new Vector3(0, 0, 0)
            };*/
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {

        }
    }
}
