using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.AI.Tactics
{
    public interface ITacticFactory
    {
        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic);
    }
}