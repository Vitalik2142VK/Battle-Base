using BattleBase.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    [System.Serializable]
    public class ImproverData : IImproverData
    {
        [SerializeField][Min(1f)] private int[] _improvePrices;
        [SerializeField] private Sprite _icon;
        [SerializeField] private LanguageTextsSet _name;
        [SerializeField] private LanguageTextsSet _description;
        [SerializeField][Min(0.5f)] private float _constructionTime = 5f;

        public IEnumerable<int> ImprovePrices => _improvePrices;

        public Sprite Icon => _icon;

        public LanguageTextsSet Name => _name;

        public LanguageTextsSet Description => _description;

        public float ConstructionTime => _constructionTime;

        public int Price => _improvePrices[0];

        public bool IsSummable => false;
    }
}