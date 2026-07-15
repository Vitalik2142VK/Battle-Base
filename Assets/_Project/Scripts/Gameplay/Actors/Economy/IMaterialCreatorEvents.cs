using System;

namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialCreatorEvents
    {
        public event Action<int> MaterialsCreated;
    }
}