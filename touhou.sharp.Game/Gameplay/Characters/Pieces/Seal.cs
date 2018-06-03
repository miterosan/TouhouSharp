using osu.Framework.Allocation;
using osu.Framework.Extensions.Color4Extensions;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.MathUtils;
using osu.Framework.Platform;
using OpenTK;
using OpenTK.Graphics;
using touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers.DrawableTouhosuPlayers;
using touhou.sharp.Game.Gameplay.Characters.VitaruPlayers.DrawableVitaruPlayers;
using touhou.sharp.Game.Graphics;

namespace touhou.sharp.Game.Gameplay.Characters.Pieces
{
    public class Seal : Container
    {
        public Container Sign { get; private set; }

        private CircularContainer characterSigil;

        // ReSharper disable once NotAccessedField.Local
        private SpriteText rightValue;
        private SpriteText leftValue;

        private CircularProgress health;
        private CircularProgress energy;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private Character character;

        private Sprite gear1;
        private Sprite gear2;
        private Sprite gear3;
        private Sprite gear4;
        private Sprite gear5;

        public Seal(Character character)
        {
            this.character = character;
        }

        [BackgroundDependencyLoader]
        private void load(THSharpSkinElement textures)
        {
            if (character is DrawableTHSharpPlayer v)
            {
                Color4 lightColor = v.PrimaryColor.Lighten(0.5f);
                Color4 darkColor = v.PrimaryColor.Darken(0.5f);

                Size = new Vector2(90);

                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                AlwaysPresent = true;

                Children = new Drawable[]
                {
                    Sign = new Container
                    {
                        RelativeSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,

                        Size = new Vector2(0.6f),

                        Alpha = 0,
                        AlwaysPresent = true,

                        Children = new Drawable[]
                        {
                            characterSigil = new CircularContainer
                            {
                                RelativeSizeAxes = Axes.Both,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Masking = true,
                            },
                            new Sprite
                            {
                                RelativeSizeAxes = Axes.Both,
                                Size = new Vector2(2f),

                                Colour = v.PrimaryColor,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Texture = textures.GetSkinTextureElement("seal"),
                            }
                        }
                    },
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0.2f,
                        Size = new Vector2(1.5f),
                        Padding = new MarginPadding(-Blur.KernelSize(5)),
                        Child = (health = new CircularProgress
                        {
                            RelativeSizeAxes = Axes.Both,
                            InnerRadius = 0.05f,
                            Colour = v.ComplementaryColor
                        }).WithEffect(new GlowEffect
                        {
                            Colour = v.ComplementaryColor,
                            Strength = 2,
                            PadExtent = true
                        }),
                    },
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        RelativeSizeAxes = Axes.Both,
                        Alpha = 0.2f,
                        Size = new Vector2(1.75f),
                        Padding = new MarginPadding(-Blur.KernelSize(5)),

                        Child = (energy = new CircularProgress
                        {
                            RelativeSizeAxes = Axes.Both,
                            InnerRadius = 0.05f,
                            Colour = v.SecondaryColor
                        }).WithEffect(new GlowEffect
                        {
                            Colour = v.SecondaryColor,
                            Strength = 2,
                            PadExtent = true
                        }),
                    },
                    new Container
                    {
                        Position = new Vector2(-30, 0),
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreRight,

                        Child = (leftValue = new SpriteText
                        {
                            Anchor = Anchor.CentreLeft,
                            Origin = Anchor.CentreRight,
                            Colour = v.ComplementaryColor,
                            Font = "Venera",
                            TextSize = 16,
                            Alpha = 0.75f,
                        }).WithEffect(new GlowEffect
                        {
                            Colour = Color4.Transparent,
                            PadExtent = true,
                        }),
                    },
                    new Container
                    {
                        Position = new Vector2(30, 0),
                        Anchor = Anchor.CentreRight,
                        Origin = Anchor.CentreLeft,

                        Child = (rightValue = new SpriteText
                        {
                            Anchor = Anchor.CentreRight,
                            Origin = Anchor.CentreLeft,
                            Colour = v.SecondaryColor,
                            Font = "Venera",
                            TextSize = 16,
                            Alpha = 0.75f,
                        }).WithEffect(new GlowEffect
                        {
                            Colour = Color4.Transparent,
                            PadExtent = true,
                        }),
                    },
                };

                switch (v.Player.FileName)
                {
                    case "SakuyaIzayoi":
                        characterSigil.Children = new Drawable[]
                        {
                            gear1 = new Sprite
                            {
                                Colour = lightColor,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Texture = textures.GetSkinTextureElement("gearSmall"),
                                Position = new Vector2(-41, 10),
                            },
                            gear2 = new Sprite
                            {
                                Colour = v.PrimaryColor,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Texture = textures.GetSkinTextureElement("gearMedium"),
                                Position = new Vector2(-4, 16),
                            },
                            gear3 = new Sprite
                            {
                                Colour = darkColor,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Texture = textures.GetSkinTextureElement("gearLarge"),
                                Position = new Vector2(-16, -34),
                            },
                            gear4 = new Sprite
                            {
                                Colour = v.PrimaryColor,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Texture = textures.GetSkinTextureElement("gearMedium"),
                                Position = new Vector2(35, -40),
                            },
                            gear5 = new Sprite
                            {
                                Colour = lightColor,
                                Anchor = Anchor.Centre,
                                Origin = Anchor.Centre,
                                Texture = textures.GetSkinTextureElement("gearSmall"),
                                Position = new Vector2(33, 8),
                            },
                        };
                        break;
                }
            }
            else
            {
                Scale = new Vector2(0.6f);

                AutoSizeAxes = Axes.Both;
                Anchor = Anchor.Centre;
                Origin = Anchor.Centre;

                AlwaysPresent = true;

                Children = new Drawable[]
                {
                    Sign = new Container
                    {
                        AutoSizeAxes = Axes.Both,
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,

                        Alpha = 0,

                        Child = new Sprite
                        {
                            Anchor = Anchor.Centre,
                            Origin = Anchor.Centre,
                            Colour = character.PrimaryColor,
                            Texture = textures.GetSkinTextureElement("sign")
                        }
                    }
                };
            }
        }

        protected override void Update()
        {
            base.Update();

            Sign.RotateTo((float)(-Clock.CurrentTime / 1000 * 90) * 0.1f);

            if (character is DrawableTHSharpPlayer v)
            {
                if (v is DrawableTouhosuPlayer t)
                {
                    Sign.Alpha = (float)t.Energy / (float)(t.TouhosuPlayer.MaxEnergy * 2);
                    energy.Current.Value = t.Energy / t.TouhosuPlayer.MaxEnergy;
                }

                health.Current.Value = v.Health / v.MaxHealth;
                

                switch (v.Player.FileName)
                {
                    case "SakuyaIzayoi":
                        float speed = 0.25f;
                        gear1.RotateTo((float)(Clock.CurrentTime / 1000 * 90) * 1.25f * speed);
                        gear2.RotateTo((float)(-Clock.CurrentTime / 1000 * 90) * 1.1f * speed);
                        gear3.RotateTo((float)(Clock.CurrentTime / 1000 * 90) * speed);
                        gear4.RotateTo((float)(-Clock.CurrentTime / 1000 * 90) * 1.1f * speed);
                        gear5.RotateTo((float)(Clock.CurrentTime / 1000 * 90) * 1.25f * speed);
                        DrawableSakuya s = v as DrawableSakuya;
                        leftValue.Text = s.SetRate.ToString();
                        break;
                }
            }
        }
    }
}
