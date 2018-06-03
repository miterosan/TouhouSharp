using Symcol.Core.Graphics.Containers;

namespace touhou.sharp.Game.Gameplay.Playfield
{
    public class GamePlayfield : SymcolContainer
    {
        public readonly AbstractionField GameField;

        public GamePlayfield()
        {
            Child = GameField = new AbstractionField();
        }
    }
}
