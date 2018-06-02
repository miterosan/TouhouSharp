using System;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Configuration;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using osu.Framework.Timing;
using Symcol.Core.Graphics.Sprites;

namespace touhou.sharp.Game.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableSakuya : DrawableTouhosuPlayer
    {
        #region Fields
        protected AnimatedSprite Idle;
        protected AnimatedSprite Left;
        protected AnimatedSprite Right;

        public double SetRate { get; private set; } = 0.75d;

        private double originalRate;

        private double currentRate = 1;

        private readonly Bindable<WorkingBeatmap> workingBeatmap = new Bindable<WorkingBeatmap>();
        #endregion

        public DrawableSakuya(THSharpPlayfield playfield, THSharpNetworkingClientHandler vitaruNetworkingClientHandler) : base(playfield, new Sakuya(), vitaruNetworkingClientHandler)
        {
            Spell += (action) =>
            {
                if (originalRate == 0)
                    originalRate = (float)workingBeatmap.Value.Track.Rate;

                currentRate = originalRate * SetRate;
                applyToClock(workingBeatmap.Value.Track, currentRate);

                SpellEndTime = Time.Current + 1000;
            };
        }

        protected override void LoadAnimationSprites(TextureStore textures, Storage storage)
        {
            if (PlayerVisuals == GraphicsOptions.StandardV2)
            {

                SoulContainer.Alpha = 0;
                KiaiContainer.Alpha = 1;

                KiaiLeftSprite.Alpha = 0;
                KiaiRightSprite.Alpha = 0;
                KiaiStillSprite.Alpha = 0;

                KiaiContainer.AddRange(new Drawable[]
                {
                    Idle = new AnimatedSprite()
                    {
                        RelativeSizeAxes = Axes.Both,
                        UpdateRate = 100,
                        Textures = new Texture[]
                        {
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 0", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 1", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 2", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 3", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 4", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 5", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 6", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai 7", storage),
                        }
                    },
                    Left = new AnimatedSprite()
                    {
                        Alpha = 0,
                        RelativeSizeAxes = Axes.Both,
                        UpdateRate = 100,
                        Textures = new Texture[]
                        {
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 0", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 1", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 2", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 3", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 4", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 5", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 6", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Left 7", storage),
                        }
                    },
                    Right = new AnimatedSprite()
                    {
                        Alpha = 0,
                        RelativeSizeAxes = Axes.Both,
                        UpdateRate = 100,
                        Textures = new Texture[]
                        {
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 0", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 1", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 2", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 3", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 4", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 5", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 6", storage),
                            THSharpSkinElement.LoadSkinElement(Player.Name + " Kiai Right 7", storage),
                        }
                    }
                });
            }
            else
                base.LoadAnimationSprites(textures, storage);

        }

        protected override void MovementAnimations()
        {
            if (PlayerVisuals == GraphicsOptions.StandardV2)
            {
                if (Position.X > LastX && Right.Alpha < 1)
                {
                    Idle.Alpha = 0;
                    Left.Alpha = 0;
                    Right.Alpha = 1;
                    Right.Reset();
                }
                else if (Position.X < LastX && Left.Alpha < 1)
                {
                    Idle.Alpha = 0;
                    Left.Alpha = 1;
                    Right.Alpha = 0;
                    Left.Reset();
                }
                else if (Position.X == LastX && Idle.Alpha < 1)
                {
                    Idle.Alpha = 1;
                    Left.Alpha = 0;
                    Right.Alpha = 0;
                    Idle.Reset();
                }

                LastX = Position.X;
            }
            else
                base.MovementAnimations();
        }

        [BackgroundDependencyLoader]
        private void load(OsuGameBase game)
        {
            workingBeatmap.BindTo(game.Beatmap);
        }

        protected override void SpellUpdate()
        {
            base.SpellUpdate();

            if (SpellEndTime >= Time.Current)
                if (!SpellActive)
                {
                    currentRate += (float)Clock.ElapsedFrameTime / 100;
                    if (currentRate > originalRate)
                        currentRate = originalRate;
                    applyToClock(workingBeatmap.Value.Track, currentRate);
                    if (currentRate > 0 && SpellEndTime - 500 <= Time.Current)
                    {
                        currentRate = originalRate;
                        applyToClock(workingBeatmap.Value.Track, currentRate);
                    }
                    else if (currentRate < 0 && SpellEndTime + 500 >= Time.Current)
                    {
                        currentRate = originalRate;
                        applyToClock(workingBeatmap.Value.Track, currentRate);
                    }
                }
                else
                {
                    double energyDrainMultiplier = 0;
                    if (currentRate < 1)
                        energyDrainMultiplier = originalRate - currentRate;
                    else if (currentRate >= 1)
                        energyDrainMultiplier = currentRate - originalRate;

                    Energy -= (Clock.ElapsedFrameTime / 1000) * (1 / currentRate) * energyDrainMultiplier * TouhosuPlayer.EnergyDrainRate;

                    if (currentRate > 0)
                        SpellEndTime = Time.Current + 2000;
                    else
                        SpellEndTime = Time.Current - 2000;

                    currentRate = originalRate * SetRate;
                    applyToClock(workingBeatmap.Value.Track, currentRate);
                }
        }

        private void applyToClock(IAdjustableClock clock, double speed)
        {
            if (clock is IHasPitchAdjust pitchAdjust)
                pitchAdjust.PitchAdjust = speed;

            SpeedMultiplier = 1 / speed;
        }

        protected override void Pressed(THSharpAction action)
        {
            base.Pressed(action);

            if (action == THSharpAction.Increase)
            {
                if (Actions[THSharpAction.Slow])
                    SetRate = Math.Min(Math.Round(SetRate + 0.05d, 2), 2d);
                else
                    SetRate = Math.Min(Math.Round(SetRate + 0.25d, 2), 2d);
            }
            if (action == THSharpAction.Decrease)
            {
                if (Actions[THSharpAction.Slow])
                    SetRate = Math.Max(Math.Round(SetRate - 0.05d, 2), 0.25d);
                else
                    SetRate = Math.Max(Math.Round(SetRate - 0.25d, 2), 0.25d);
            }
        }
    }
}
