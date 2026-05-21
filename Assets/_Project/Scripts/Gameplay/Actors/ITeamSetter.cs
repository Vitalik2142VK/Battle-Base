namespace BattleBase.Gameplay.Actors
{
    public interface ITeamSetter : ITeamable
    {
        public void SetTeam(TeamType teamType);
    }
}
