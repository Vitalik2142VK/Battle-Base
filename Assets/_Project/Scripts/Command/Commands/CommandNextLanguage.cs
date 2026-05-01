using BattleBase.Localization;

namespace BattleBase.Commands
{
    public class CommandNextLanguage : CommandBase
    {
        public override void Execute() =>
            YandexGameLanguageSystemAdapter.Next(); 
    }
}