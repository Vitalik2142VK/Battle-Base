using System;
using BattleBase.Localization;

namespace BattleBase.Shop
{
    public class Shop
    {

    }

    public readonly struct UpgradeButtonInfo
    {
        public readonly LanguageTextsSet Name;
        public readonly int Price;
        public readonly int MaximumLevel;
        public readonly int CurrentLevel;
        public readonly Action Clicked;

        public UpgradeButtonInfo(
            LanguageTextsSet name,
            int price,
            int maximumLevel,
            int currentLevel,
            Action clicked)
        {
            Name = name;
            Price = price;
            MaximumLevel = maximumLevel;
            CurrentLevel = currentLevel;
            Clicked = clicked;
        }

    }
}