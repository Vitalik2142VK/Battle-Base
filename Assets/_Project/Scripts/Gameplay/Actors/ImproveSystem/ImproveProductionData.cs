using BattleBase.Gameplay.Actors.Economy;
using BattleBase.Gameplay.Actors.Production;
using BattleBase.Gameplay.Actors.Production.Improve;
using BattleBase.Localization;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.ImproveSystem
{
    public class ImproveProductionData : IImproveProductionData
    {
        private readonly IProductionData _data;
        private readonly IMaterialRegistry _materialRegistry;

        private ITeamable _teamable;

        public ImproveProductionData(IProductionData data, IMaterialRegistry materialRegistry)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));
        }

        public Sprite Icon => _data.Icon;

        public ILanguageTextsSet Name => _data.Name;

        public ILanguageTextsSet Description => _data.Description;

        public IMaterialData MaterialData => _materialRegistry.GetMaterialData(_teamable.TeamType);

        public float ConstructionTime => _data.ConstructionTime;

        public bool IsSummable => _data.IsSummable;

        public int Price { get; private set; }

        public bool CanBuy => CanBayImprove();

        public void Init(ITeamable teamable)
        {
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public void SetPrice(int price)
        {
            if (price <= 0)
                throw new ArgumentOutOfRangeException(nameof(price));

            Price = price;
        }

        private bool CanBayImprove()
        {
            IMaterialData data = _materialRegistry.GetMaterialData(_teamable.TeamType);

            return data.CurrentMaterials >= Price;
        }
    }
}