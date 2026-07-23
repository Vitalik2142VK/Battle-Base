using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.Energy
{
    [CreateAssetMenu(
    fileName = nameof(PowerGeneratorSource),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(PowerGeneratorSource))]
    public class PowerGeneratorSource : ActorComponentSource, IPowerGeneratorSource
    {
        [SerializeField] private PowerByRank[] _addedPowerByRank;

        public IEnumerable<IPowerByRank> AddedPowerByRank => _addedPowerByRank;
    }
}
