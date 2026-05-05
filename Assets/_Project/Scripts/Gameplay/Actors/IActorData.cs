using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public interface IActorData
    {
        public Sprite Icon { get; }

        public ILanguageVisitor Name { get; }

        public ILanguageVisitor Description { get; }

        public TeamType TeamType { get; }

        public float ConstructionTime { get; }
    }
}
