using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    [CreateAssetMenu(fileName = nameof(BuildTacticSetting),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(BuildTacticSetting))]
    public class BuildTacticSetting : TacticSetting, IBuildTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig _buildingConfig;
        [SerializeField] private TacticCategory _category = TacticCategory.Attack;
        [SerializeField][Range(1, 3)] private int[] _priorityLinesNumbers = new[] { 1 };
        [SerializeField][Range(1, 30)] private int _scoreForBuild = 5;
        [SerializeField][Range(1, 12)] private int _maxNumberBuildings = 3;

        public TacticCategory Category => _category;

        public string BuildingId => _buildingConfig.Data.Id;

        public IEnumerable<int> PriorityLinesNumbers => _priorityLinesNumbers;

        public int ScoreForBuild => _scoreForBuild;

        public int MaxBuildings => _maxNumberBuildings;
    }
}