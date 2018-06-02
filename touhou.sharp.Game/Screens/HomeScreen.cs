using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;

namespace touhou.sharp.Game.Screens
{
    public class HomeScreen : THSharpScreen
    {
        public HomeScreen()
        {
            Children = new Drawable[]
            {
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = Color4.Blue
                },
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,

                    Colour = Color4.White,
                    TextSize = 24,
                    Text = "There is no game yet, check back later!"
                }
            };
        }
    }
}
