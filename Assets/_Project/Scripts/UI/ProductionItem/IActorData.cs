using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public interface IActorData
    {
        public ActorView Prefab { get; }

        public Sprite Icon { get; }

        public LanguageTextsSet Name { get; }

        public LanguageTextsSet Description { get; }

        public float ConstructionTime { get; }

        public int Price { get; }
    }
}