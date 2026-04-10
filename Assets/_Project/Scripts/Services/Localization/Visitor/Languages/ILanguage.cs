namespace BattleBase.Services.Localization
{
    public interface ILanguage
    {
        public void Accept(ILanguageVisitor visitor);
    }
}