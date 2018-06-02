using touhou.sharp.Characters.Characters.TouhosuPlayers;
using touhou.sharp.Characters.Characters.THSharpPlayers;

namespace touhou.sharp.Characters.Characters.TouhosuPlayers
{
    public class TouhosuPlayer : THSharpPlayer
    {
        public virtual double MaxEnergy { get; } = 0;

        public virtual double EnergyCost { get; } = 0;

        public virtual double EnergyDrainRate { get; } = 0;

        public virtual string Spell { get; } = "None";

        public virtual Role Role { get; } = Role.Offense;

        public virtual Difficulty Difficulty { get; } = Difficulty.Easy;

        public virtual bool Implemented { get; }

        public static TouhosuPlayer GetTouhosuPlayer(string name)
        {
            switch (name)
            {
                default:
                    return new TouhosuPlayer();

                case "ReimuHakurei":
                    return new Reimu();
                case "RyukoyHakurei":
                    return new Ryukoy();
                case "TomajiHakurei":
                    return new Tomaji();

                case "SakuyaIzayoi":
                    return new Sakuya();
                case "RemiliaScarlet":
                    return new Remilia();
                case "FlandreScarlet":
                    return new Flandre();

                case "AliceLetrunce":
                    return new Alice();
                case "VasterLetrunce":
                    return new Vaster();

                case "MarisaKirisame":
                    return new Marisa();
            }
        }
    }

    public enum Role
    {
        Offense,
        Defense,
        Support
    }

    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Insane,
        Another,
        Extra,
    }
}
