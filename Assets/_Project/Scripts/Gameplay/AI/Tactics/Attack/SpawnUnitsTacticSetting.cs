using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Attack
{
    [CreateAssetMenu(fileName = nameof(SpawnUnitsTacticSetting),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(SpawnUnitsTacticSetting))]
    public class SpawnUnitsTacticSetting : TacticSetting, ISpawnUnitsTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig[] _unitFactoriesConfigs;
        [SerializeField] private ActorConfig[] _forbiddenUnitsConfigs;
        [SerializeField][Range(5, 10)] private int _maxNumSpawn = 7;
        [SerializeField][Range(1, 5)] private int _minNumSpawn = 2;

        private string[] _unitFactoriesIds;
        private string[] _forbiddenUnitsIds;

        public IEnumerable<string> UnitFactoriesIds => GetUnitFactoriesIds();

        public IEnumerable<string> ForbiddenUnitsIds => GetForbiddenActorsIds();

        public TacticCategory Category => TacticCategory.Attack;

        public int MaxNumSpawn => _maxNumSpawn;

        public int MinNumSpawn => _minNumSpawn;

        private string[] GetForbiddenActorsIds()
        {
            if (_forbiddenUnitsIds != null && _forbiddenUnitsIds.Length > 0)
                return _forbiddenUnitsIds;

            _forbiddenUnitsIds = _forbiddenUnitsConfigs
                .Select(c => c.Data.Id)
                .ToArray();

            return _forbiddenUnitsIds;
        }

        private string[] GetUnitFactoriesIds()
        {
            if (_unitFactoriesIds != null && _unitFactoriesIds.Length > 0)
                return _unitFactoriesIds;

            _unitFactoriesIds = _unitFactoriesConfigs
                .Select(c => c.Data.Id)
                .ToArray();

            return _unitFactoriesIds;
        }
    }
}