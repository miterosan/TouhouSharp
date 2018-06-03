using System;
using Symcol.Core.NeuralNetworking;
using touhou.sharp.Game.Gameplay;
using TensorFlow;

namespace touhou.sharp.Game.NeuralNetworking
{
    public class THSharpBrain : TensorFlowBrain<THSharpAction>
    {
        public override TFTensor GetTensor(TFSession session, THSharpAction t)
        {
            throw new NotImplementedException();
        }
    }
}
