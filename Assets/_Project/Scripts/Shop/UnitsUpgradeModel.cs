using System;
using System.Collections.Generic;
using System.Linq;
using BattleBase.SaveService;

namespace BattleBase.ShopSystem
{
    public class UnitsUpgradeModel : ISaveable
    {
        private readonly IShopSaver _saver;
        private readonly List<ShopUnitItemInfo> _infos = new();

        private ShopUnitItemInfo _selected;

        public UnitsUpgradeModel(IShopSaver saver, UnitsUpgradeConfig config)
        {
            _saver = saver ?? throw new ArgumentNullException(nameof(saver));

            if (config == null)
                throw new ArgumentNullException(nameof(config));

            foreach (IShopUnitItemInfo info in config.Infos)
                _infos.Add(new(info));

            Load();

            SelectUnit(_infos.First());
        }

        public event Action DamageLevelChanged;
        public event Action ArmorLevelChanged;
        public event Action BuildTimeLevelChanged;
        public event Action UnitSelectionChanged;

        public IReadOnlyList<IShopUnitItemInfo> Infos => _infos;

        public IShopUnitItemInfo Selected => _selected;

        public IShopUpgradePanelInfo PanelInfo => _selected.PanelInfo;

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

        public void SelectUnit(IShopUnitItemInfo unit)
        {
            _selected = unit as ShopUnitItemInfo ?? throw new ArgumentNullException(nameof(unit));
            UnitSelectionChanged?.Invoke();
        }


        public void Load()
        {
            //throw new NotImplementedException();
        }

        public void Save()
        {
            //throw new NotImplementedException();
        }
    }
}