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

        protected override void MovementAnimations()
        {
            //base.MovementAnimations();

            if (Seal.Alpha > 0)
                Seal.RotateTo((float)((Clock.CurrentTime / 1000) * 90));
        }

        protected override void LoadAnimationSprites(TextureStore textures, Storage storage)
        {
            SoulContainer.Alpha = 0;
            KiaiContainer.Alpha = 1;

            KiaiLeftSprite.Alpha = 0;
            KiaiRightSprite.Alpha = 0;
            KiaiStillSprite.Alpha = 1;

            //KiaiStillSprite.Texture = THSharpSkinElement.LoadSkinElement(CharacterName + " Kiai", storage);

            Size = new Vector2(128);
        }

        protected override void Death()
        {
            //base.Death();
            Hitbox.HitDetection = false;
        }
    }
}
