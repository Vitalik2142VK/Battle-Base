using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.No
{
    [CreateAssetMenu(fileName = nameof(RandomTacticSetting),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(RandomTacticSetting))]
    public class RandomTacticSetting : TacticSetting, IRandomTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig[] _forbiddenActorConfigs;
        [SerializeField][Range(2, 5)] private int _maxNumSpawn = 3;
        [SerializeField][Range(1, 3)] private int _minNumSpawn = 1;

        private void OnValidate()
        {
            if (_maxNumSpawn < _minNumSpawn)
                _minNumSpawn = _maxNumSpawn - 1;
        }

        private string[] _forbiddenActorIds;

        public TacticCategory Category => TacticCategory.No;

        public IEnumerable<string> ForbiddenActorIds => GetForbiddenActorIds();

        public int MaxNumSpawn => _maxNumSpawn;

        public int MinNumSpawn => _minNumSpawn;

        private string[] GetForbiddenActorIds()
        {
            if (_forbiddenActorIds != null && _forbiddenActorIds.Length > 0)
                return _forbiddenActorIds;

            _forbiddenActorIds = _forbiddenActorConfigs
                .Select(c => c.Data.Id)
                .ToArray();

            return _forbiddenActorIds;
        }
    }
}