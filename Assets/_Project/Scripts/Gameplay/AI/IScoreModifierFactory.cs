using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Modifiers;

namespace BattleBase.Gameplay.AI
{
    public interface IScoreModifierFactory
    {
        public ModifierType Type { get; }

        public IAdvancedScoreModifier Create(IScoreModifierConfig config, TeamType team);
    }
}