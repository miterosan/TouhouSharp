using Symcol.Core.Graphics.Containers;

namespace touhou.sharp.Game.Gameplay.Playfield
{
    public class Playfield : SymcolContainer
    {
        public readonly AbstractionField GameField;

        public Playfield()
        {
            Child = GameField = new AbstractionField();
        }
    }
}
