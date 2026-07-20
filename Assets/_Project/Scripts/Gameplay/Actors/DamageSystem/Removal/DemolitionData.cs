using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    [System.Serializable]
    public class DemolitionData : IDemolitionData
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private LanguageTextsSet _name;
        [SerializeField] private LanguageTextsSet _description;
        [SerializeField][Min(0.5f)] private float _constructionTime = 5f;
        [SerializeField][Range(0.1f, 0.95f)] private float _returnedCoefficient = 0.5f;

        public Sprite Icon => _icon;

        public ILanguageTextsSet Name => _name;

        public ILanguageTextsSet Description => _description;

        public float ConstructionTime => _constructionTime;

        public int Price => 0;

        public bool IsSummable => false;

        public float ReturnedCoefficient => _returnedCoefficient;
    }
}