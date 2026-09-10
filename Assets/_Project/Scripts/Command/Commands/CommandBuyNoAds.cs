using BattleBase.Utils.Constants;
using YG;

namespace BattleBase.Commands
{
    public class CommandBuyNoAds : CommandBase
    {
        public override void Execute() =>
            YG2.BuyPayments(PurchasesIds.NoAds);
    }
}