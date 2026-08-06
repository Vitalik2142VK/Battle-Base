using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Energy
{
    [CreateAssetMenu(fileName = nameof(PowerTacticSetting),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(PowerTacticSetting))]
    public class PowerTacticSetting : TacticSetting, IPowerTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig _powerStationConfig;
        [SerializeField][Range(1, 3)] private int[] _lineNumbersForBuild = new[] { 2 };
        [SerializeField][Range(1, 30)] private int _scoreForBuild = 5;
        [SerializeField][Range(2, 4)] private int _maxNumberStations = 3;

        public TacticCategory Category => TacticCategory.Power;

        public string PowerStationId => _powerStationConfig.Data.Id;

        public int[] LineNumbersForBuild => _lineNumbersForBuild;

        public int ScoreForBuildStation => _scoreForBuild;

        public int MaxStations => _maxNumberStations;
    }
}