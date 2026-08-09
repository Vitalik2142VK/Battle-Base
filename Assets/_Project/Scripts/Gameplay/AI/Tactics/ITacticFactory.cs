using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITacticFactory
    {
        public TacticCategory Category { get; }

        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic);
    }
}