using YG;

namespace BattleBase.SaveService
{
    public interface IPurchasesSaver
    {
        public int GetState(string key);

        public void SetState(string key, int value);
    }
}