using OpenTK.Graphics;

namespace touhou.sharp.Game.Characters.VitaruPlayers
{
    public class THSharpPlayer
    {
        public virtual string Name { get; } = "Alex";

        public virtual string FileName { get; } = "Alex";

        public virtual double MaxHealth => 80;

        public virtual Color4 PrimaryColor { get; } = Color4.Green;

        public virtual Color4 SecondaryColor { get; } = Color4.LightBlue;

        public virtual Color4 ComplementaryColor { get; } = Color4.LightGreen;

        public virtual string Background { get; } = "Alex always had a thing for music.";

        public static THSharpPlayer GetTHSharpPlayer(string name)
        {
            switch (name)
            {
                default:
                    return new THSharpPlayer();
                case "Alex":
                    return new Alex();
            }
        }
    }
}
