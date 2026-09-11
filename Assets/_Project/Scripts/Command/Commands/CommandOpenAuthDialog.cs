using YG;

namespace BattleBase.Commands
{
    public class CommandOpenAuthDialog : CommandBase
    {
        public override void Execute() =>
            YG2.OpenAuthDialog();
    }
}