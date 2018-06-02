using System;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Configuration;
using osu.Framework.Timing;

namespace touhou.sharp.Game.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableRyukoy : DrawableTouhosuPlayer
    {
        #region Fields
        private double setPitch = 0.9d;

        private int level = 1;

        private readonly Bindable<WorkingBeatmap> workingBeatmap = new Bindable<WorkingBeatmap>();

        private readonly Bindable<int> abstraction;
        #endregion

        public DrawableRyukoy(THSharpPlayfield playfield, THSharpNetworkingClientHandler vitaruNetworkingClientHandler, Bindable<int> abstraction) : base(playfield, new Ryukoy(), vitaruNetworkingClientHandler)
        {
            this.abstraction = abstraction;
            Abstraction = 3;

            Spell += (input) =>
            {
                abstraction.Value = level;
            };
        }

        [BackgroundDependencyLoader]
        private void load(OsuGameBase game)
        {
            workingBeatmap.BindTo(game.Beatmap);
        }

        protected override void SpellUpdate()
        {
            base.SpellUpdate();

            if (SpellActive)
            {
                Energy -= (Clock.ElapsedFrameTime / 1000) * TouhosuPlayer.EnergyDrainRate * (level * 0.25f);

                abstraction.Value = level;
                applyToClock(workingBeatmap.Value.Track, setPitch);
            }
            else
            {
                applyToClock(workingBeatmap.Value.Track, 1);
                abstraction.Value = 0;
            }
        }

        private void applyToClock(IAdjustableClock clock, double pitch)
        {
            if (clock is IHasPitchAdjust pitchAdjust)
            {
                pitchAdjust.PitchAdjust = pitch;

                if (setPitch > 1)
                    clock.Rate = (1 - (pitch - 1) / 2);
                else if (setPitch < 1)
                    clock.Rate = (1 + (pitch - 1) * -2);
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

        #region Touhosu Story Content
        public const string Background = "Being the elder sibling comes with many responsabilitys in the Hakurei family. " +
            "She has the weight of the Hakurei name to uphold as the next inline to be the keeper of their family shrine. " +
            "Her mother would tell her stories about her adventures with her friends to stop the evil fairies from claiming it, and how they always would succeed. " +
            "One day she would like to go on an adventure of her own she would think. \"Becareful what you wish for\" Reimu would tell her. " +
            "Now that she is almost a legal adult she has a very different view however, she is calm and level headed. " +
            "She doesn't actively seek trouble to solve or ways to cause trouble, she simply wishes for peace, quiet and an easy life as the next Hakurei Maiden.";
        #endregion
    }
}
