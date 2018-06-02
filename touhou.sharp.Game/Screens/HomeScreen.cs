using OpenTK;
using OpenTK.Graphics;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using Symcol.Core.Graphics.Containers;

namespace touhou.sharp.Game.Screens
{
    public class HomeScreen : THSharpMenuScreen
    {
        private readonly SymcolClickableContainer editorButton;

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
                },
                editorButton = new SymcolClickableContainer
                {
                    Anchor = Anchor.BottomRight,
                    Origin = Anchor.BottomRight,
                    Masking = true,

                    RelativeSizeAxes = Axes.Both,
                    Size = new Vector2(0.12f, 0.08f),
                    Position = new Vector2(-10),

                    CornerRadius = 16, 
                    BorderThickness = 4,

                    Children = new Drawable[]
                    {
                        new Box
                        {
                            Colour = Color4.Red,
                            RelativeSizeAxes = Axes.Both
                        },
                        new SpriteText
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,

                            Colour = Color4.White,
                            TextSize = 24,
                            Text = "Editor"
                        }
                    }
                }
            };

            editorButton.Action = () => Push(new EditorScreen());
        }
    }
}
