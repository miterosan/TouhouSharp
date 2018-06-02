using osu.Framework.Graphics;

namespace touhou.sharp.Game.Characters.TouhosuPlayers.DrawableTouhosuPlayers
{
    public class DrawableAlice : DrawableTouhosuPlayer
    {
        public DrawableAlice(THSharpPlayfield playfield, THSharpNetworkingClientHandler vitaruNetworkingClientHandler) : base(playfield, new Alice(), vitaruNetworkingClientHandler)
        {
            Spell += (input) =>
            {

            };
        }

        protected override void SpellUpdate()
        {
            base.SpellUpdate();

            if (SpellActive)
            {
                foreach (Drawable drawable in THSharpPlayfield.GameField.Current)
                    if (drawable is DrawableTouhosuPlayer drawableTouhosuPlayer)
                    {
                        
                    }
            }
        }
    }
}
