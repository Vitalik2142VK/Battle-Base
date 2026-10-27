using BattleBase.Gameplay.AI.Deactevators;
using System;

namespace BattleBase.Gameplay.AI.Factories.Deactevator
{
    public interface IDeactivatorFactory
    {
        public Type DeactevatorType { get; }

        public IDeactivator Create(ITacticDeactivatorScore score);
    }
}