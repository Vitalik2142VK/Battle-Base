using System;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public interface IDeactivatorFactory
    {
        public Type DeactevatorType { get; }

        public IDeactivator Create(ITacticDeactivatorScore score);
    }
}