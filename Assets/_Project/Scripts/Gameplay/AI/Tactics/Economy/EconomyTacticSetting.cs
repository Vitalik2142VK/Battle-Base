using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Economy
{
    [CreateAssetMenu(fileName = nameof(EconomyTacticSetting),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(EconomyTacticSetting))]
    public class EconomyTacticSetting : TacticSetting, IEconomyTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig _materialFactoryConfig;
        [SerializeField][Range(1, 3)] private int[] _lineNumbersForBuild = new[] { 2 };
        [SerializeField][Range(1, 30)] private int _scoreForBuild = 5;
        [SerializeField][Range(3, 5)] private int _maxNumberFactories = 3;
        [SerializeField][Min(1000)] private int _materialsForStop = 3000;

        public TacticCategory Category => TacticCategory.Economy;

        public string MaterialFactoryId => _materialFactoryConfig.Data.Id;

        public IEnumerable<int> LineNumbersForBuild => _lineNumbersForBuild;

        public int ScoreForBuildFactory => _scoreForBuild;

        public int MaterialsForStop => _materialsForStop;

        public int MaxFactories => _maxNumberFactories;
    }
}