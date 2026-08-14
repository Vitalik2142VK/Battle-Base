using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Production
{
    public interface IProductionData
    {
        public Sprite Icon { get; }

        public ILanguageTextsSet Name { get; }

        public ILanguageTextsSet Description { get; }

        public float ConstructionTime { get; }

        public int Price { get; }

        public bool IsHidden { get; }
    }
}