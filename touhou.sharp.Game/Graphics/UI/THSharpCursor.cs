using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Cursor;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using OpenTK;

namespace touhou.sharp.Game.Graphics.UI
{
    public class THSharpCursor : CursorContainer
    {
        public static CircularContainer CenterCircle;

        public THSharpCursor()
        {
            Origin = Anchor.Centre;
            Size = new Vector2(32);
            Masking = false;
        }

        [BackgroundDependencyLoader]
        private void load(THSharpSkinElement textures)
        {
            Children = new Drawable[]
            {
                new Sprite
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(Size.X + Size.X / 3.5f),
                    Texture = textures.GetSkinTextureElement("ring")
                },
                CenterCircle = new CircularContainer
                {
                    Masking = true,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Size = new Vector2(Size.X / 5),
                    Child = new Box
                    {
                        RelativeSizeAxes = Axes.Both
                    }
                },
                new Container
                {
                    Masking = false,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,
                    Rotation = 45,

                    Children = new Drawable[]
                    {
                        new Container
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            CornerRadius = Size.X / 12,
                            Size = new Vector2(Size.X / 3, Size.X / 7),
                            Position = new Vector2(Size.X / 3, 0),
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        },
                        new Container
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            CornerRadius = Size.X / 12,
                            Size = new Vector2(Size.X / 3, Size.X / 7),
                            Position = new Vector2(-1 * Size.X / 3, 0),
                            Rotation = 180,
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        },
                        new Container
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            CornerRadius = Size.X / 12,
                            Size = new Vector2(Size.X / 3, Size.X / 7),
                            Position = new Vector2(0, Size.X / 3),
                            Rotation = 90,
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        },
                        new Container
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            CornerRadius = Size.X / 12,
                            Size = new Vector2(Size.X / 3, Size.X / 7),
                            Position = new Vector2(0, -1 * Size.X / 3),
                            Rotation = 270,
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        }
                    }
                },
                new Container
                {
                    Masking = false,
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    RelativeSizeAxes = Axes.Both,

                    Children = new Drawable[]
                    {
                        new CircularContainer
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(Size.X / 8),
                            Position = new Vector2(Size.X / 4, 0),
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        },
                        new CircularContainer
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(Size.X / 8),
                            Position = new Vector2(-1 * Size.X / 4, 0),
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        },
                        new CircularContainer
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(Size.X / 8),
                            Position = new Vector2(0, Size.X / 4),
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        },
                        new CircularContainer
                        {
                            Masking = true,
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Size = new Vector2(Size.X / 8),
                            Position = new Vector2(0, -1 * Size.X / 4),
                            Child = new Box
                            {
                                RelativeSizeAxes = Axes.Both
                            }
                        }
                    }
                }
            };
        }
    }
}