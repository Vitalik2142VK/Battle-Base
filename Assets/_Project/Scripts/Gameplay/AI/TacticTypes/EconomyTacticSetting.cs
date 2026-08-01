using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.AI.TacticTypes
{
    [CreateAssetMenu(fileName = nameof(EconomyTacticSetting),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(EconomyTacticSetting))]
    public class EconomyTacticSetting : TacticSetting, IEconomyTacticSetting
    {
        [SerializeField][Range(1, 30)] private int _scoreForAction = 5;
        [SerializeField] private ActorConfig _materialFactoryConfig;
        [SerializeField][Range(1, 3)] private int[] _lineNumbersForBuild = new[] { 2 };
        [SerializeField][Min(1000)] private int _materialsForStop = 3000;
        [SerializeField][Range(2, 5)] private int _maxNumberFactories = 3;

        public TacticCategory Category => TacticCategory.Economy;

        public string MaterialFactoryId => _materialFactoryConfig.Data.Id;

        public int[] LineNumbersForBuild => _lineNumbersForBuild;

        public int ScoreForBuildFactory => _scoreForAction;

        public int MaterialsForStop => _materialsForStop;

        public int MaxFactories => _maxNumberFactories;
    }
}