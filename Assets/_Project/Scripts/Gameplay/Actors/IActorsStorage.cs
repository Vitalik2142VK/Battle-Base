namespace BattleBase.Gameplay.Actors
{
    public interface IActorsStorage
    {
        public int GetActorPositionsOtherTeam(IActorPosition[] positions, TeamType team);
    }
}
