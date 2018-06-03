using System;
using osu.Framework.Graphics;
using OpenTK;

namespace touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableReimu : DrawableTouhosuPlayer
    {
        private const double leader_max = 2d;
        private const double leader_min = 1d;
        private const double leader_max_range = 64;
        private const double leader_min_range = 128;

        //private readonly List<DrawableTouhosuPlayer> leaderedPlayers = new List<DrawableTouhosuPlayer>();

        public DrawableReimu(Playfield.Playfield playfield) : base(playfield, new Reimu())
        {
        }

        protected override void SpellUpdate()
        {
            base.SpellUpdate();

            if (SpellActive)
            {
                foreach (Drawable drawable in THSharpPlayfield.GameField.Current)
                    if (drawable is DrawableTouhosuPlayer drawableTouhosuPlayer && drawableTouhosuPlayer.Team == Team)
                    {
                        Vector2 object2Pos = drawableTouhosuPlayer.ToSpaceOfOtherDrawable(Vector2.Zero, this) + new Vector2(6);
                        double distance = Math.Sqrt(Math.Pow(object2Pos.X, 2) + Math.Pow(object2Pos.Y, 2));

                        if (distance <= leader_min_range)
                        {
                            drawableTouhosuPlayer.HealingMultiplier = getLeaderDistanceMultiplier(distance);
                            drawableTouhosuPlayer.EnergyGainMultiplier = getLeaderDistanceMultiplier(distance);
                            Energy -= (Clock.ElapsedFrameTime / 1000) * TouhosuPlayer.EnergyDrainRate;
                        }
                        else
                        {
                            drawableTouhosuPlayer.HealingMultiplier = 1;
                            drawableTouhosuPlayer.EnergyGainMultiplier = 1;
                        }
                    }
            }
        }

        private double getLeaderDistanceMultiplier(double value)
        {
            double scale = (leader_max - leader_min) / (leader_max_range - leader_min_range);
            return leader_min + ((value - leader_min_range) * scale);
        }
    }
}
