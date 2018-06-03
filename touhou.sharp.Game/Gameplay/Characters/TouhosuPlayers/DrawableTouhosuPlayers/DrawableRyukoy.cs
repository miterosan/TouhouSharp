using System;
using osu.Framework.Audio;
using osu.Framework.Configuration;
using osu.Framework.Timing;
using touhou.sharp.Game.NeuralNetworking;

namespace touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableRyukoy : DrawableTouhosuPlayer
    {
        #region Fields
        private double setPitch = 0.9d;

        private int level = 1;

        private readonly Bindable<int> abstraction;
        #endregion

        public DrawableRyukoy(Playfield.Playfield playfield, Bindable<int> abstraction) : base(playfield, new Ryukoy())
        {
            this.abstraction = abstraction;
            Abstraction = 3;

            Spell += input => abstraction.Value = level;
        }

        protected override void SpellUpdate()
        {
            base.SpellUpdate();

            if (SpellActive)
            {
                Energy -= Clock.ElapsedFrameTime / 1000 * TouhosuPlayer.EnergyDrainRate * (level * 0.25f);
                abstraction.Value = level;
            }
            else
                abstraction.Value = 0;
        }

        // ReSharper disable once UnusedMember.Local
        private void applyToClock(IAdjustableClock clock, double pitch)
        {
            if (clock is IHasPitchAdjust pitchAdjust)
            {
                pitchAdjust.PitchAdjust = pitch;

                if (setPitch > 1)
                    clock.Rate = 1 - (pitch - 1) / 2;
                else if (setPitch < 1)
                    clock.Rate = 1 + (pitch - 1) * -2;
                else
                    clock.Rate = 1;
            }
        }

        protected override void Pressed(THSharpAction action)
        {
            base.Pressed(action);

            if (action == THSharpAction.Increase)
                level = Math.Min(level + 1, 3);
            if (action == THSharpAction.Decrease)
                level = Math.Max(level - 1, 0);

            switch (level)
            {
                case 0:
                    setPitch = 1;
                    break;
                case 1:
                    setPitch = 0.9d;
                    break;
                case 2:
                    setPitch = 1.2d;
                    break;
                case 3:
                    setPitch = 0.8d;
                    break;
            }
        }
    }
}
