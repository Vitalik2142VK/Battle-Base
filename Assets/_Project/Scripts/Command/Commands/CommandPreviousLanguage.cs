using BattleBase.Localization;

namespace BattleBase.Commands
{
    public class CommandPreviousLanguage : CommandBase
    {
        public override void Execute() =>
            YandexGameLanguageSystemAdapter.Previous(); 
    }
}