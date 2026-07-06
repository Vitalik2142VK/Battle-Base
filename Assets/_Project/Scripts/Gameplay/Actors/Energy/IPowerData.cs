using System;

namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerData
    {
        public event Action DataChanged;

        public int CurrentCapacity { get; }

        public int UsedEnergy { get; }
    }
}
