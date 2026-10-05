using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Economy
{
    [CreateAssetMenu(fileName = nameof(DemolitionEconomyTacticSetting),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(DemolitionEconomyTacticSetting))]
    public class DemolitionEconomyTacticSetting : TacticSetting, IDemolitionEconomyTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig _materialFactoryConfig;
        [SerializeField][Range(0, 4)] private int _numberRemainingFactories = 3;
        [SerializeField][Min(5000)] private int _materialsForStart = 10000;

        public TacticCategory Category => TacticCategory.Economy;

        public string MaterialFactoryId => _materialFactoryConfig.Data.Id;

        public int NumberRemainingFactories => _numberRemainingFactories;

        public int MaterialsForStart => _materialsForStart;
    }
}