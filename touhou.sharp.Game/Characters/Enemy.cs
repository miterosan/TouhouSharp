using osu.Framework.Audio.Track;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Textures;
using osu.Framework.Platform;
using OpenTK;
using OpenTK.Graphics;

namespace touhou.sharp.Game.Characters
{
    public class Enemy : Character
    {
        public override double MaxHealth => 60;

        protected override string CharacterName => "enemy";

        public override Color4 PrimaryColor => characterColor;

        protected override float HitboxWidth => 48;

        private Color4 characterColor;

        public Enemy(THSharpPlayfield playfield, Pattern pattern, DrawablePattern drawablePattern) : base(playfield)
        {
            AlwaysPresent = true;

            Team = 1;
            characterColor = drawablePattern.AccentColour;
        }

        protected override void MovementAnimations()
        {
            if (LeftSprite.Texture == null && RightSprite != null)
            {
                LeftSprite.Texture = RightSprite.Texture;
                LeftSprite.Size = new Vector2(-RightSprite.Size.X, RightSprite.Size.Y);
            }
            if (KiaiLeftSprite.Texture == null && KiaiRightSprite != null)
            {
                KiaiLeftSprite.Texture = KiaiRightSprite.Texture;
                KiaiLeftSprite.Size = new Vector2(-KiaiRightSprite.Size.X, KiaiRightSprite.Size.Y);
            }
            if (Position.X > LastX)
            {
                if (LeftSprite.Texture != null)
                    LeftSprite.Alpha = 0;
                if (RightSprite?.Texture != null)
                    RightSprite.Alpha = 1;
                if (StillSprite.Texture != null)
                    StillSprite.Alpha = 0;
                if (KiaiLeftSprite.Texture != null)
                    KiaiLeftSprite.Alpha = 0;
                if (KiaiRightSprite?.Texture != null)
                    KiaiRightSprite.Alpha = 1;
                if (KiaiStillSprite.Texture != null)
                    KiaiStillSprite.Alpha = 0;
            }
            else if (Position.X < LastX)
            {
                if (LeftSprite.Texture != null)
                    LeftSprite.Alpha = 1;
                if (RightSprite?.Texture != null)
                    RightSprite.Alpha = 0;
                if (StillSprite.Texture != null)
                    StillSprite.Alpha = 0;
                if (KiaiLeftSprite.Texture != null)
                    KiaiLeftSprite.Alpha = 1;
                if (KiaiRightSprite?.Texture != null)
                    KiaiRightSprite.Alpha = 0;
                if (KiaiStillSprite.Texture != null)
                    KiaiStillSprite.Alpha = 0;
            }
            LastX = Position.X;
        }

        protected override void LoadAnimationSprites(TextureStore textures, Storage storage)
        {
            base.LoadAnimationSprites(textures, storage);
            RightSprite.Texture = THSharpSkinElement.LoadSkinElement(CharacterName, storage);
            KiaiRightSprite.Texture = THSharpSkinElement.LoadSkinElement(CharacterName + "Kiai", storage);
        }

        protected override void OnNewBeat(int beatIndex, TimingControlPoint timingPoint, EffectControlPoint effectPoint, TrackAmplitudes amplitudes)
        {
            base.OnNewBeat(beatIndex, timingPoint, effectPoint, amplitudes);

            if (effectPoint.KiaiMode && SoulContainer.Alpha == 1)
            {
                SoulContainer.FadeOutFromOne(timingPoint.BeatLength / 4);

                if (THSharpPlayfield.Boss == null)
                    KiaiContainer.FadeInFromZero(timingPoint.BeatLength / 4);
                else
                    Hitbox.HitDetection = false;
            }
            if (!effectPoint.KiaiMode && SoulContainer.Alpha == 0)
            {
                SoulContainer.FadeInFromZero(timingPoint.BeatLength);

                if (THSharpPlayfield.Boss == null)
                    KiaiContainer.FadeOutFromOne(timingPoint.BeatLength);
                else
                    Hitbox.HitDetection = true;
            }
        }

        protected override void Death()
        {
            Dead = true;
            Hitbox.HitDetection = false;
        }
    }
}
