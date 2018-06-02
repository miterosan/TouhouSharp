using System;
using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using OpenTK;

namespace touhou.sharp.Game.Characters
{
    public class Boss : Character
    {
        public bool Free = true;

        public override double MaxHealth => 20000;

        protected override string CharacterName => "Kokoro Hatano";

        protected override float HitboxWidth => 64;

        private Sprite dean;

        public Boss(THSharpPlayfield playfield) : base(playfield)
        {
            Position = new Vector2(256 , 384 / 2);
            AlwaysPresent = true;
            Abstraction = 3;
            Alpha = 0;
            Team = 1;
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            Hitbox.HitDetection = false;
        }

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
        {
            base.OnNewBeat(beatIndex, timingPoint, effectPoint, amplitudes);

            float amplitudeAdjust = Math.Min(1, 0.4f + amplitudes.Maximum);

            double beat_in_time = 60;

            Seal.ScaleTo(2 - 0.05f * amplitudeAdjust, beat_in_time, Easing.Out);
            using (Seal.BeginDelayedSequence(beat_in_time))
                Seal.ScaleTo(2, timingPoint.BeatLength * 2, Easing.OutQuint);

            if (effectPoint.KiaiMode)
            {
                Seal.FadeTo(0.25f * amplitudeAdjust, beat_in_time, Easing.Out);
                using (Seal.BeginDelayedSequence(beat_in_time))
                    Seal.FadeOut(timingPoint.BeatLength);
            }

            if (effectPoint.KiaiMode && Alpha == 0)
            {
                Hitbox.HitDetection = true;
                this.FadeInFromZero(timingPoint.BeatLength / 4);
                Seal.FadeTo(0.15f, timingPoint.BeatLength / 4);
            }
            if (!effectPoint.KiaiMode && Alpha == 1)
            {
                Hitbox.HitDetection = false;
                this.FadeOutFromOne(timingPoint.BeatLength);
                Seal.FadeTo(0f, timingPoint.BeatLength);
            }
        }

        protected override void MovementAnimations()
        {
            //base.MovementAnimations();

            if (Seal.Alpha > 0)
                Seal.RotateTo((float)((Clock.CurrentTime / 1000) * 90));
        }

        protected override void Update()
        {
            base.Update();

            if (BulletPiece.ExclusiveTestingHax && KiaiStillSprite.Alpha == 1)
            {
                KiaiStillSprite.Alpha = 0;
                Add(dean = new Sprite
                {
                    RelativeSizeAxes = Axes.Both,
                    Origin = Anchor.Centre,
                    Anchor = Anchor.Centre,
                    Texture = THSharpRuleset.THSharpTextures.Get("Dean"),
                });
            }
            else if (!BulletPiece.ExclusiveTestingHax && KiaiStillSprite.Alpha == 0)
            {
                KiaiStillSprite.Alpha = 1;
                Remove(dean);
                dean.Dispose();
            }

            if (BulletPiece.ExclusiveTestingHax)
                dean.Rotation += (float)Clock.ElapsedFrameTime / 4;
        }

        protected override void LoadAnimationSprites(TextureStore textures, Storage storage)
        {
            SoulContainer.Alpha = 0;
            KiaiContainer.Alpha = 1;

            KiaiLeftSprite.Alpha = 0;
            KiaiRightSprite.Alpha = 0;
            KiaiStillSprite.Alpha = 1;

            KiaiStillSprite.Texture = THSharpSkinElement.LoadSkinElement(CharacterName + " Kiai", storage);

            Size = new Vector2(128);
        }

        protected override void Death()
        {
            //base.Death();
            Hitbox.HitDetection = false;
        }
    }
}
