using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IScoreModifierConfig
    {
        public IEnumerable<IModifier> Modifiers { get; }

        public ModifierType Type { get; }
    }
}