using System;

namespace BattleBase.Gameplay
{
    public interface IBuildingSite
    {
        public event Action StateChanged;

        public bool IsPlayer { get; }

        public BuildingSiteState State { get; }

        public bool TrySelect();

        public void Unselect();
    }
}