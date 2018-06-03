using Symcol.Core.Graphics.Containers;
using touhou.sharp.Game.NeuralNetworking;

namespace touhou.sharp.Game.Gameplay.Playfield
{
    public class THSharpPlayfield : SymcolContainer
    {
        public readonly SymcolContainer GameField;

        public THSharpPlayfield()
        {
            Child = GameField = new SymcolContainer()
            {

            };
        }
    }
}
