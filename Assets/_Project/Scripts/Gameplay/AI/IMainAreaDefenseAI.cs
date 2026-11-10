namespace BattleBase.Gameplay.AI
{
    public interface IMainAreaDefenseAI : IAreaDefenseAI
    {
        public ILineAreaDefenseAI GetLineAreaDefenseAI(int lineDefenceNumber);
    }
}