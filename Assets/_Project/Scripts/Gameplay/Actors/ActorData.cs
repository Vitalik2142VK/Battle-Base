using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    [System.Serializable]
    public class ActorData : IActorData
    {
        [SerializeField] private ActorView _prefab;
        [SerializeField] private Sprite _icon;
        [SerializeField] private LanguageTextsSet _name;
        [SerializeField] private LanguageTextsSet _description; 
        [SerializeField][Min(3f)] private float _constructionTime = 5f;
        [SerializeField][Min(1)] private int _price = 20;

        public ActorView Prefab => _prefab;

        public Sprite Icon => _icon;

        public LanguageTextsSet Name => _name;

        public LanguageTextsSet Description => _description;

        public float ConstructionTime => _constructionTime;

        public int Price => _price;
    }
}
