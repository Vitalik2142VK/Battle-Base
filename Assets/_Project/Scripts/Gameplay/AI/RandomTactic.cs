using BattleBase.Gameplay.Actors;
using BattleBase.Gameplay.Actors.Building;
using BattleBase.Gameplay.Actors.Spawn;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.AI
{
    public partial class RandomTactic : ITactic
    {
        private readonly List<IRegisteredBuildingSite> _buildingSites;
        private readonly List<IActorData> _actorDatas;
        private readonly IBuildingSitesController _controller;
        private readonly Random _random;
        private readonly RandomTacticSetting _setting;

        private IActorSpawner _currentSpawner;

        public RandomTactic(IBuildingSitesController controller, RandomTacticSetting setting)
        {
            _controller = controller ?? throw new ArgumentNullException(nameof(controller));
            _setting = setting ?? throw new ArgumentNullException(nameof(setting));

            _buildingSites = new List<IRegisteredBuildingSite>();
            _actorDatas = new List<IActorData>();
            _random = new Random();
        }

        public bool CanAction()
        {
            if (_buildingSites.Count == 0)
            {
                var buildingSites = _controller.GetRegisteredBuildingSites(_setting.Team);
                _buildingSites.AddRange(buildingSites);
            }

            return TryGetRandomSpawner();
        }

        public ICommand GetCommand()
        {
            if (_currentSpawner == null)
                throw new InvalidOperationException("First, tactics must be checked for the possibility of action");

            IActorData actorData = GetRandomActorData();
            int count = _random.Next(_setting.MinNumSpawn, _setting.MaxNumSpawn);

            return new SpawnCommand(_currentSpawner, actorData, count);
        }

        private bool TryGetRandomSpawner()
        {
            int index;

            do
            {
                index = _random.Next(_buildingSites.Count);

                if (_buildingSites[index].TryGetActorSpawner(out _currentSpawner))
                {
                    return true;
                }
                else
                {
                    _buildingSites.RemoveAt(index);
                }
            }
            while (_buildingSites.Count > 0);

            return false;
        }

        private IActorData GetRandomActorData()
        {
            _actorDatas.Clear();
            _actorDatas.AddRange(_currentSpawner.ActorsData);

            int index = _random.Next(_actorDatas.Count);

            return _actorDatas[index];
        }
    }
}