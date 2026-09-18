using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors
{
    public class MutationActorData : IActorData
    {
        public MutationActorData(IActorData data)
        {
            Id = data.Id;
            Prefab = data.Prefab;
            Power = data.Power;
            IsAvailable = data.IsAvailable;
            Icon = data.Icon;
            //todo пока конфиги незаполнены, выбивают ошибку, поэтому закомментил. Когда конфиги будут заполнены, раскомментировать
            //Name = data.Name;
            //Description = data.Description;
            ConstructionTime = data.ConstructionTime;
            Price = data.Price;
        }

        public string Id { get; }

        public ActorView Prefab { get; }

        public int Power { get; }

        public bool IsAvailable { get; private set; }

        public Sprite Icon { get; }

        public ILanguageTextsSet Name { get; }

        public ILanguageTextsSet Description { get; }

        public float ConstructionTime { get; }

        public int Price { get; }

        public void SetAvailable(bool isAvailable) =>
            IsAvailable = isAvailable;
    }
}