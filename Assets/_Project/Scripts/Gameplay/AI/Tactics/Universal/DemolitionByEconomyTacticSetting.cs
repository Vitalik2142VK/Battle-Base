using BattleBase.Gameplay.Actors;
using BattleBase.Utils.Constants;
using System;
using UnityEngine;

namespace BattleBase.Gameplay.AI.Tactics.Universal
{
    [CreateAssetMenu(fileName = nameof(DemolitionByEconomyTacticSetting),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfig) + "/" + nameof(DemolitionByEconomyTacticSetting))]
    public class DemolitionByEconomyTacticSetting : TacticSetting, IDemolitionByEconomyTacticSetting
    {
        [Space]
        [Header("Unique")]
        [SerializeField] private ActorConfig _buildingConfig;
        [SerializeField][Range(0, 4)] private int _numberRemainingFactories = 3;
        [SerializeField][Min(1000)] private int _materialsForStart = 10000;
        [SerializeField] private bool _isRemoveAfterComplited = false;

        public TacticCategory Category => TacticCategory.Economy;

        public string BuildingId => _buildingConfig.Data.Id;

        public int NumberRemainingFactories => _numberRemainingFactories;

        public int MaterialsForStart => _materialsForStart;

        public bool IsRemoveAfterComplited => _isRemoveAfterComplited;
    }
}