using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Tactics;

namespace BattleBase.Gameplay.AI.Factories.Tactic
{
    public interface ITacticFactory
    {
        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic);
    }
}