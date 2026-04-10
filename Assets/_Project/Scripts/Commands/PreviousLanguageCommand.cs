using BattleBase.Services.Localization;

namespace BattleBase.Commands
{
    public class PreviousLanguageCommand : CommandBase
    {
        public override void Execute() =>
            YandexGameLanguageSystemAdapter.Previous();
    }
}