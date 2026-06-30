using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.Gameplay.Actors.DamageSystem.Removal
{
    [CreateAssetMenu(
    fileName = nameof(DemolitionSource),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(ActorConfig) + "/" + nameof(DemolitionSource))]
    public class DemolitionSource : ActorComponentSource, IDemolitionSource
    {
        [SerializeField] private DemolitionData _demolitionData;

        public IDemolitionData Data => _demolitionData;
    }
}