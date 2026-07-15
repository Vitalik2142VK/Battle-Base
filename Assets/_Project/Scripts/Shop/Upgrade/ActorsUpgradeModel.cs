using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.SaveService;

namespace BattleBase.ShopSystem
{
    public class ActorsUpgradeModel : IActorsUpgradeModel, ISaveable
    {
        private readonly IShopSaver _saver;
        private readonly List<ShopUnitItemInfo> _infos = new();
        private readonly ActorsUpgradeConfig _config;

        private ShopUnitItemInfo _selected;

        public ActorsUpgradeModel(IShopSaver saver, ActorsUpgradeConfig config)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));
            _config = config != null ? config : throw new ArgumentNullException(nameof(config));

            Load();
            SelectUnit(_infos.First());
        }

        public event Action DamageLevelChanged;
        public event Action ArmorLevelChanged;
        public event Action BuildTimeLevelChanged;
        public event Action UnitSelectionChanged;

        public IReadOnlyList<IShopActorItemConfig> Infos => _infos;

        public IShopActorItemConfig Selected => _selected;

        public IShopUpgradeStatsInfo PanelInfo => _selected.PanelInfo;

        public void IncreaseDamageLevel()
        {
            _selected.IncreaseDamageLevel();
            DamageLevelChanged?.Invoke();
        }

        public void IncreaseArmorLevel()
        {
            _selected.IncreaseArmorLevel();
            ArmorLevelChanged?.Invoke();
        }

        public void IncreaseBuildTimeLevel()
        {
            _selected.IncreaseBuildTimeLevel();
            BuildTimeLevelChanged?.Invoke();
        }

        public void SelectUnit(IShopActorItemConfig unit)
        {
            _selected = unit as ShopUnitItemInfo ?? throw new ArgumentNullException(nameof(unit));
            UnitSelectionChanged?.Invoke();
        }

        public void Load()
        {
            IReadOnlyList<IUnitUpgradeData> datas = _saver.ShopData.UnitsUpgrades;
            int dataCount = datas.Count;

            _infos.Clear();

            foreach (IShopActorItemConfig info in _config.Infos)
                _infos.Add(new(info));

            for (int i = 0; i < _infos.Count; i++)
            {
                if (i <  dataCount)
                {
                    ShopUnitItemInfo info = _infos[i];

                    info.SetDamageLevel(datas[i].DamageLevel);
                    info.SetArmorLevel(datas[i].ArmorLevel);
                    info.SetBuildTimeLevel(datas[i].BuildTimeLevel);
                }
            }
        }

        public void Save()
        {
            List<UnitUpgradeData> newUnitData = new(_infos.Count);

            foreach (ShopUnitItemInfo info in _infos)
            {
                string unitName = info.UnitName.En.Text;
                IShopUpgradeStatsInfo panel = info.PanelInfo;

                UnitUpgradeData unitData = new(
                    unitName, 
                    panel.DamageInfo.CurrentLevel, 
                    panel.HealthInfo.CurrentLevel,
                    panel.BuildTimeInfo.CurrentLevel);

                newUnitData.Add(unitData);
            }

            ShopData data = new(_saver.ShopData);
            data.SetUnitsUpgrades(newUnitData);
            _saver.SetShopData(data);
        }
    }
}