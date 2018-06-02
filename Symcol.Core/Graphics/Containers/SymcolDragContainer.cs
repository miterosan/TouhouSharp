using osu.Framework.Input;
using OpenTK;
using OpenTK.Input;
using osu.Framework.Graphics.Containers;

namespace Symcol.Core.Graphics.Containers
{
    public class SymcolDragContainer : Container
    {
        public override bool HandleMouseInput => true;

        protected override bool OnDragStart(InputState state) => true;

        public bool AllowLeftClickDrag { get; set; } = true;

        private bool leftDown;
        private bool rightDown;

        private Vector2 startPosition;

        protected override bool OnMouseDown(InputState state, MouseDownEventArgs args)
        {
            startPosition = Position;

            if (args.Button == MouseButton.Left && AllowLeftClickDrag)
                leftDown = true;

            if (args.Button == MouseButton.Right)
                rightDown = true;

            return base.OnMouseDown(state, args);
        }

        protected override bool OnDrag(InputState state)
        {
            if (leftDown | rightDown)
                Position = startPosition + state.Mouse.Position - state.Mouse.PositionMouseDown.GetValueOrDefault();

            return base.OnDrag(state);
        }

        protected override bool OnMouseUp(InputState state, MouseUpEventArgs args)
        {
            if (args.Button == MouseButton.Left && AllowLeftClickDrag)
                leftDown = false;

            if (args.Button == MouseButton.Right)
                rightDown = false;

            return base.OnMouseUp(state, args);
        }
    }
}
