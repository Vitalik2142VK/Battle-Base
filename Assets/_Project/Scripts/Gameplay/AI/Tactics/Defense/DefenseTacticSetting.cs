using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Defense
{
    [CreateAssetMenu(
    fileName = nameof(DefenseTacticSetting),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(DefenseTacticSetting))]
    public class DefenseTacticSetting : TacticSetting, IDefenseTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig[] _defenseBuildingConfigs;
        [SerializeField][Range(0, 3)] private int[] _lineNumbersForBuild = new[] { 0 };
        [SerializeField][Range(1, 30)] private int _scoreForBuild = 5;

        private string[] _defenseBuildingIds;

        public TacticCategory Category => TacticCategory.Defense;

        public IEnumerable<string> DefenseBuildingIds => GetDefenseBuildingIds();

        public IEnumerable<int> LineNumbersForBuild => _lineNumbersForBuild;

        public int ScoreForBuild => _scoreForBuild;

        private string[] GetDefenseBuildingIds()
        {
            if (_defenseBuildingIds != null)
                return _defenseBuildingIds;

            _defenseBuildingIds = _defenseBuildingConfigs
                .Select(c => c.Data.Id)
                .ToArray();

            return _defenseBuildingIds;
        }
    }
}