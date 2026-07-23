using BattleBase.Core;
using System;

namespace BattleBase.Gameplay.Actors.Production.Spawn
{
    public class SpawnProductionOption : ISpawnProductionOption
    {
        private readonly ICommand _spawnCommand;
        private readonly ICommand _cancelSpawnCommand;
        private readonly ISpawnProductionData _data;

        public SpawnProductionOption(ICommand spawnCommand, ICommand cancelSpawnCommand, ISpawnProductionData data)
        {
            _spawnCommand = spawnCommand ?? throw new ArgumentNullException(nameof(spawnCommand));
            _cancelSpawnCommand = cancelSpawnCommand ?? throw new ArgumentNullException(nameof(cancelSpawnCommand));
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public TypeProduction Type => TypeProduction.Spawn;

        public IProductionData Data => _data.ActorData;

        public ISpawnProductionData SpawnData => _data;

        public void Execute() => 
            _spawnCommand.Execute();

        public void CancelSpawn() => 
            _cancelSpawnCommand.Execute();
    }
}