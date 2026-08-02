using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.AI.Modifiers;
using BattleBase.Utils.Constants;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Gameplay.AI
{
    [CreateAssetMenu(
    fileName = nameof(BrainConfing),
    menuName = AssetMenuPaths.ScriptableObjects + nameof(BrainConfing) + "/" + nameof(BrainConfing))]
    public class BrainConfing : ScriptableObject, IBrainConfing
    {
        [SerializeField] private TacticSetting[] _tacticSettings;
        [SerializeField] private ScoreModifierConfig[] _modifierConfigs;
        [SerializeField] private TeamType _teamType;

        private List<ITacticSetting> _tacticSettingsList;
        private List<IScoreModifierConfig> _modifierConfigsList;

        public IEnumerable<ITacticSetting> TacticSetting => GetTacticSetting();

        public IEnumerable<IScoreModifierConfig> ScoreModifierConfigs => GetIScoreModifierConfig();

        public TeamType TeamType => _teamType;

        private IEnumerable<ITacticSetting> GetTacticSetting()
        {
            if (_tacticSettingsList != null)
                return _tacticSettingsList;

            _tacticSettingsList = new List<ITacticSetting>();

            foreach (var settings in _tacticSettings)
            {
                if (settings is ITacticSetting tacticSetting)
                    _tacticSettingsList.Add(tacticSetting);
            }

            return _tacticSettingsList;
        }

        private IEnumerable<IScoreModifierConfig> GetIScoreModifierConfig()
        {
            if (_modifierConfigsList != null)
                return _modifierConfigsList;

            _modifierConfigsList = new List<IScoreModifierConfig>();

            foreach (var config in _modifierConfigs)
            {
                if (config is IScoreModifierConfig modifierConfig)
                    _modifierConfigsList.Add(modifierConfig);
            }

            return _modifierConfigsList;
        }
    }
}