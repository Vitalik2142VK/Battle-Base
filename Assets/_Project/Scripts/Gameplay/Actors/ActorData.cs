using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [System.Serializable]
    public class ActorData : IActorData
    {
        [SerializeField] private ActorView _prefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ActorNameConfig _nameConfig;
        [SerializeField][Min(0.5f)] private float _constructionTime = 5f;
        [SerializeField][Min(1)] private int _price = 20;
        [SerializeField][Range(0, 15)] private int _power = 0;
        [SerializeField] private bool _isSummable = true;

        public string Id => _prefab.name;

        public ActorView Prefab => _prefab;

        public Sprite Icon => _icon;

        public ILanguageTextsSet Name => _nameConfig.Name;

        public ILanguageTextsSet Description => _nameConfig.Description;

        public float ConstructionTime => _constructionTime;

        public int Price => _price;

        public int Power => _power;

        public bool IsHidden => _isSummable;
    }
}
