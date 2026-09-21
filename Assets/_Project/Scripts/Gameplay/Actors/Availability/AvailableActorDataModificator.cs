using BattleBase.Localization;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Availability
{
    public class AvailableActorDataModificator : IActorData
    {
        private readonly IActorData _data;

        public AvailableActorDataModificator(IActorData data)
        {
            _data = data ?? throw new System.ArgumentNullException(nameof(data));

            IsAvailable = _data.IsAvailable;
        }

        public string Id => _data.Id;

        public ActorView Prefab => _data.Prefab;

        public int Power => _data.Power;

        public Sprite Icon => _data.Icon;

        public ILanguageTextsSet Name => _data.Name;

        public ILanguageTextsSet Description => _data.Description;

        public float ConstructionTime => _data.ConstructionTime;

        public int Price => _data.Price;

        public bool IsAvailable { get; private set; }

        public void SetAvailable(bool isAvailable) =>
            IsAvailable = isAvailable;
    }
}