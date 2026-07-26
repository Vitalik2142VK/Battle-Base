using BattleBase.Gameplay.Actors;
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
        [SerializeField] private TacticSetting[] _settings;
        [SerializeField] private TeamType _teamType;

        private List<ITacticSetting> _tacticSettings;

        public IEnumerable<ITacticSetting> TacticSetting => GetTacticSetting();

        public TeamType TeamType => _teamType;

        private IEnumerable<ITacticSetting> GetTacticSetting()
        {
            if (_tacticSettings != null)
                return _tacticSettings;

            _tacticSettings = new List<ITacticSetting>();

            foreach (var settings in _settings)
            {
                if (settings is ITacticSetting tacticSetting)
                    _tacticSettings.Add(tacticSetting);
            }

            return _tacticSettings;
        }
    }
}