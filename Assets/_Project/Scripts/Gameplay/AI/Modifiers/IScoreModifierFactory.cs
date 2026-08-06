using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.AI.Modifiers
{
    public interface IScoreModifierFactory
    {
        public ModifierType Type { get; }

        public IAdvancedScoreModifier Create(IScoreModifierConfig config, TeamType team);
    }
}