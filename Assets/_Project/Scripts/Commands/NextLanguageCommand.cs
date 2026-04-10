using BattleBase.Services.Localization;

namespace BattleBase.Commands
{
    public class NextLanguageCommand : CommandBase
    {
        public override void Execute() =>
            YandexGameLanguageSystemAdapter.Next();
    }
}