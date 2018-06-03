using osu.Framework.Graphics;

namespace touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableAlice : DrawableTouhosuPlayer
    {
        public DrawableAlice(Playfield.Playfield playfield) : base(playfield, new Alice())
        {

        }

        protected override void SpellUpdate()
        {
            base.SpellUpdate();

            if (SpellActive)
            {
                foreach (Drawable drawable in THSharpPlayfield.GameField.Current)
                    // ReSharper disable once UnusedVariable
                    if (drawable is DrawableTouhosuPlayer drawableTouhosuPlayer)
                    {
                        
                    }
            }
        }
    }
}
