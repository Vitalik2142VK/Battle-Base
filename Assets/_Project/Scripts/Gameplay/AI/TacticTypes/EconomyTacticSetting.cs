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
        [SerializeField] private ActorConfig _materialFactoryConfig;
        [SerializeField][Range(1, 3)] private int[] _lineNumbersForBuild = new[] { 2 };
        [SerializeField][Range(2, 6)] private int _minFactories = 3;
        [SerializeField][Min(1000)] private int _materialsForStop = 3000;
        [SerializeField][Range(1, 5)] private int _numberActionsRow = 3;
        [SerializeField][Range(2, 5)] private int _maxNumberFactories = 3;

        public TacticType Type => TacticType.Economy;

        public string MaterialFactoryId => _materialFactoryConfig.Data.Id;

        public int[] LineNumbersForBuild => _lineNumbersForBuild;

        public int MaxFactories => _minFactories;

        public int MaterialsForStop => _materialsForStop;

        public int NumberActionsRow => _numberActionsRow;

        public int MaxNumberFactories => _maxNumberFactories;
    }
}