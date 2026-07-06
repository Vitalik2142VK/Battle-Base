namespace BattleBase.Gameplay.Actors.Energy
{
    public interface IPowerRegistry
    {
        public bool TryReserve(TeamType team, int power);

        public IPowerData GetPowerEvent(TeamType team);
    }
}
