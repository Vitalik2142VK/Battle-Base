using System;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public interface IImproverINotifier
    {
        public event Action Improved;

        public int CurrentTier { get; }
    }
}