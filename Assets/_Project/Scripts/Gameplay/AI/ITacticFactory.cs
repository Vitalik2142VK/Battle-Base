using BattleBase.Gameplay.Actors;

namespace BattleBase.Gameplay.AI
{
    public interface ITacticFactory
    {
        public bool TryCreate(ITacticSetting setting, TeamType team, out ITactic tactic);
    }
}