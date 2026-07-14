using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerGeneratorNotifier
    {
        public event Action PowerChanged;

        public int PowerCount { get; }
    }
}
