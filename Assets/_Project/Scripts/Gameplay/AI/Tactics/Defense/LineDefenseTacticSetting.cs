using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Defense
{
    [CreateAssetMenu(
    fileName = nameof(LineDefenseTacticSetting),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(LineDefenseTacticSetting))]
    public class LineDefenseTacticSetting : TacticSetting, ILineDefenseTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig[] _defenseBuildingConfigs;
        [SerializeField][Range(1, 5)] private int _scoreForActorInArea = 3;
        [SerializeField][Range(1, 3)] private int _lineDefanceNumber = 1;

        public TacticCategory Category => TacticCategory.Defense;

        public int ScoreForActorInArea => _scoreForActorInArea;

        public int LineDefanceNumber => _lineDefanceNumber;

        public IEnumerable<string> GetDefenseBuildingIds() =>
            _defenseBuildingConfigs
                .Select(c => c.Data.Id)
                .ToList();
    }
}