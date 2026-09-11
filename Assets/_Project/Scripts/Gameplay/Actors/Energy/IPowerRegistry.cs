namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerRegistry
    {
        public bool CanReserve(TeamType team, int power);

        public IPowerData GetPowerData(TeamType team);
    }
}
