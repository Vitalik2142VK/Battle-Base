using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [Serializable]
    public class UpgradeButtonInfo
    {        
        [field: SerializeField] public List<UpgradeLevelInfo> Levels;
        [field: SerializeField] public int CurrentLevel;
        public Action<ShopUpgradeButton> Clicked;

        public int MaximumLevel => Levels.Count - 1;
    }
}