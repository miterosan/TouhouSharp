using System;
using osu.Framework.Graphics;
using OpenTK;

namespace touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableTomaji : DrawableTouhosuPlayer
    {
        private const double charge_time = 600;

        private const double blink_distance = 200;

        /// <summary>
        /// scale from 0 - 1 on how charged our blink is
        /// </summary>
        private double charge;

        public DrawableTomaji(Playfield.Playfield playfield) : base(playfield, new Tomaji())
        {
        }

        protected override void SpellUpdate()
        {
            base.SpellUpdate();

            if (SpellActive)
            {
                double fullChargeTime = SpellStartTime + charge_time;

                charge = Math.Min(1 - (fullChargeTime - Time.Current) / charge_time, 1);

                Energy -= Clock.ElapsedFrameTime / 1000 * TouhosuPlayer.EnergyDrainRate * charge;
            }
            else if (charge > 0)
            {
                double cursorAngle = MathHelper.RadiansToDegrees(Math.Atan2(Cursor.Position.Y - Position.Y, Cursor.Position.X - Position.X)) + Rotation - 12;
                double x = Position.X + charge * blink_distance * Math.Cos(MathHelper.DegreesToRadians(cursorAngle));
                double y = Position.Y + charge * blink_distance * Math.Sin(MathHelper.DegreesToRadians(cursorAngle));

                Hitbox.HitDetection = false;
                SpellEndTime = Time.Current + 200 * charge;
                Alpha = 0.25f;

                this.MoveTo(new Vector2((float)x, (float)y), 200 * charge, Easing.OutSine)
                    .FadeIn(200 * charge, Easing.InCubic);


                charge = 0;
            }

            if (Time.Current >= SpellEndTime)
                Hitbox.HitDetection = true;
        }
    }
}
