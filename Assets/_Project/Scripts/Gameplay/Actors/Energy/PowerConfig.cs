using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Energy
{
    [CreateAssetMenu(
    fileName = nameof(PowerConfig),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(PowerConfig))]
    public class PowerConfig : ActorComponentSource, IPowerConfig
    {
        [SerializeField, Range(80, 250)] private int _maxCapacity = 100;

        public int MaxCapacity => _maxCapacity;
    }
}
