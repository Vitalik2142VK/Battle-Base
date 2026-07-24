using BattleBase.Gameplay.Actors.Production;

namespace BattleBase.UI
{
    public interface IProductionItemFactory
    {
        public bool TryCreate(IProductionOption option, out IProductionItem item);
    }
}