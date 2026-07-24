using BattleBase.Utils;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Economy
{
    public class MaterialCreator : IMaterialCreator
    {
        private readonly List<IMaterialByRank> _addedMaterialsByRank;
        private readonly IMaterialCreatorConfig _config;
        private readonly IAdvancedMaterialRegistry _materialRegistry;
        private readonly Timer _timer;

        private ITeamable _teamable;
        private int _materialsPerTick;
        private int _currentRank;

        public event Action<int> MaterialsCreated;

        public MaterialCreator(IMaterialCreatorConfig config, IAdvancedMaterialRegistry materialRegistry)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _materialRegistry = materialRegistry ?? throw new ArgumentNullException(nameof(materialRegistry));

            _addedMaterialsByRank = new List<IMaterialByRank>(_config.AddedMaterialsByRank);
            _timer = new Timer();
            _materialsPerTick = 0;
            _currentRank = 0;
        }

        public Type KeyType => typeof(IMaterialCreator);

        public bool CanIncreaseProduction => _currentRank < _addedMaterialsByRank.Count;

        public void Init(ITeamable teamable)
        {
            _teamable = teamable ?? throw new ArgumentNullException(nameof(teamable));
        }

        public void Enable()
        {
            _timer.SetWaitTime(_config.AccrualTime);
            _timer.RestartTimer();

            IncreaseProduction();
        }

        public void Disable()
        {
            _materialsPerTick = 0;
            _currentRank = 0;
        }

        public void Update(float delta)
        {
            _timer.Tick(delta);

            if (_timer.IsTimeUp == false)
                return;

            _materialRegistry.AddMaterials(_teamable.TeamType, _materialsPerTick);
            _timer.RestartTimer();

            MaterialsCreated?.Invoke(_materialsPerTick);
        }

        public void IncreaseProduction()
        {
            if (CanIncreaseProduction == false)
                return;

            IMaterialByRank materialByRank = _addedMaterialsByRank[_currentRank++];
            _materialsPerTick += materialByRank.AddedMaterials;
        }
    }
}