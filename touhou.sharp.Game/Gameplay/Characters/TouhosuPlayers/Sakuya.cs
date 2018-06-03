using OpenTK.Graphics;

namespace touhou.sharp.Game.Gameplay.Characters.TouhosuPlayers
{
    public class Sakuya : TouhosuPlayer
    {
        public override string Name => "Sakuya Izayoi";

        public override string FileName => "SakuyaIzayoi";

        public override double MaxHealth => 80;

        public override double MaxEnergy => 32;

        public override double EnergyCost => 2;

        public override double EnergyDrainRate => 8;

        public override Color4 PrimaryColor => Color4.Navy;

        //public override Color4 SecondaryColor => OsuColour.FromHex("#92a0dd");

        //public override Color4 ComplementaryColor => OsuColour.FromHex("#d6d6d6");

        public override string Spell => "Time Keeper";

        public override Role Role => Role.Defense;

        public override Difficulty Difficulty => Difficulty.Normal;

        public override string Background => "Sakuya is no stranger to the oddities in the world, but never could they stop her from besting her enemies. " +
            "Her perfect record has only been tainted by one person, but The Hakureis are close friends of hers now. " +
            "They have put there differences aside once to fight off something bigger then all of them combined, but as the phrase goes: \"Greater than the sum of its parts\" they were able to hold the fort long enough to succeed.";

        public override bool Implemented => true;
    }
}
