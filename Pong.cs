using System.ComponentModel;
using Aximo.Engine;
using Aximo.Engine.Windows;
using Aximo.Engine.Components.Geometry;
using Aximo.Engine.Components.Lights;
using Aximo.Engine.Components.UI;
using OpenToolkit.Windowing.Common;
using Aximo.Render;
using OpenToolkit.Mathematics;

namespace Pong
{
    class PongGame : RenderApplication
    {
        public PongGame(RenderApplicationConfig config) : base(config)
        {
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        protected override void SetupScene()
        {
            var camSize = new Vector2(9 * RenderContext.ScreenAspectRatio, 9);
            Renderer.Context.Camera = new OrthographicCamera(new Vector3(0, -1, 0))
            {
                LookAt = new Vector3(0, 0, 0),
                Size = camSize,
            };

        }
        public override void Init()
        {
            base.Init();
        }

        public override void Run()
        {
            base.Run();
        }

        public override void Stop()
        {
            base.Stop();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        protected override void AfterRenderFrame()
        {
            base.AfterRenderFrame();
        }

        protected override void AfterUpdateFrame()
        {
            base.AfterUpdateFrame();
        }

        protected override void BeforeRenderFrame()
        {
            base.BeforeRenderFrame();
        }

        protected override void BeforeUpdateFrame()
        {
            base.BeforeUpdateFrame();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnClosed()
        {
            base.OnClosed();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        protected override void OnMouseDown(MouseButtonArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseMoveArgs e)
        {
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseButtonArgs e)
        {
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
        }

        protected override void OnUnload()
        {
            base.OnUnload();
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            base.OnUpdateFrame(e);
        }


    }
}
