using System;

namespace BattleBase.Gameplay.Actors.Economy
{
    public interface IMaterialData
    {
        public event Action DataChanged;

        public int CurrentMaterials { get; }
    }
}