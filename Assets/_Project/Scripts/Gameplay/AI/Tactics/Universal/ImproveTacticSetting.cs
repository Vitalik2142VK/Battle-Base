using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    [CreateAssetMenu(fileName = nameof(ImproveTacticSetting),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(ImproveTacticSetting))]
    public class ImproveTacticSetting : TacticSetting, IImproveTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig[] _improvedActorsConfigs;
        [SerializeField] private TacticCategory _category = TacticCategory.Attack;
        [SerializeField][Range(1, 2)] private int _maxLevelImproved = 1;

        private string[] _improvedActorsIds;

        public IEnumerable<string> ImprovedActorsIds => GetImprovedActorsIds();

        public TacticCategory Category => _category;

        public int MaxLevelImproved => _maxLevelImproved;

        private string[] GetImprovedActorsIds()
        {
            if (_improvedActorsIds != null && _improvedActorsIds.Length > 0)
                return _improvedActorsIds;

            _improvedActorsIds = _improvedActorsConfigs
                .Select(c => c.Data.Id)
                .ToArray();

            return _improvedActorsIds;
        }
    }
}