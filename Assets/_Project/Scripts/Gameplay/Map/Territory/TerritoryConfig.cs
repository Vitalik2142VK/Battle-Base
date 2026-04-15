using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CreateAssetMenu(
        fileName = nameof(TerritoryConfig), 
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(TerritoryConfig))]
    public class TerritoryConfig : ScriptableObject, ITerritoryInfo
    {
        [SerializeField] private TerritoryOwnerType _owner = TerritoryOwnerType.Enemy;

        public TerritoryOwnerType Owner => _owner;

        // todo понадобится для передачи инфы о противнике. Owner здесь не нужен, просто валяется
    }
}