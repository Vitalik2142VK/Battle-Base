namespace BattleBase.Gameplay.AI
{
    public interface ILineAreaDefenseAI : IAreaDefenseAI
    {
        public int MainSiteId { get; }
    }
}