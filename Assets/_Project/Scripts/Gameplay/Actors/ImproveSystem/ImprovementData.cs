using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    [System.Serializable]
    public class ImprovementData : IImprovementData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private LanguageTextsSet _name;
        [SerializeField] private LanguageTextsSet _description;
        [SerializeField][Min(0.5f)] private float _constructionTime = 5f;
        [SerializeField][Min(1f)] private float _priceCoefficient = 1.25f;
        [SerializeField][Min(1)] private int _priceIncrease = 20;

        public Sprite Icon => _icon;

        public LanguageTextsSet Name => _name;

        public LanguageTextsSet Description => _description;

        public float ConstructionTime => _constructionTime;

        public int Price => _priceIncrease;

        public bool IsSummable => false;

        public float PriceCoefficient => _priceCoefficient;
    }
}