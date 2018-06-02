using OpenTK.Input;
using osu.Framework.Input;

namespace touhou.sharp.Game.Screens
{
    public class THSharpMenuScreen : THSharpScreen
    {
        protected override bool OnKeyDown(InputState state, KeyDownEventArgs args)
        {
            if (args.Repeat || !IsCurrentScreen) return false;

            switch (args.Key)
            {
                case Key.Escape:
                    Exit();
                    return true;
            }

            return base.OnKeyDown(state, args);
        }
    }
}
