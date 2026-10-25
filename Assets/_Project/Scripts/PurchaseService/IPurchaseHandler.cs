namespace BattleBase.PurchaseService
{
    public interface IPurchaseHandler
    {
        public bool CanHandle(PurchaseConfig config);

        public void Grant(PurchaseConfig config);
    }
}