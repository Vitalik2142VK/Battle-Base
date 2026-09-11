namespace BattleBase.SaveService
{
    public interface IPurchasesSaver
    {
        public bool IsNoAds {  get; }

        public void SetNoAdsState(bool isOn);
    }
}