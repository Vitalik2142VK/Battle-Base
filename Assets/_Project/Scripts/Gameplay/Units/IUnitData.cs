using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Units
{
    public interface IUnitData
    {
        public Sprite Icon { get; }

        public ILanguageVisitor Name { get; }

        public ILanguageVisitor Description { get; }
    }
}
