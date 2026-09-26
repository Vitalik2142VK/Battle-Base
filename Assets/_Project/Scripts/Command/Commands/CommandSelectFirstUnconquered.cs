using System;
using BattleBase.DI;
using BattleBase.Gameplay.Map;
using VContainer;

namespace BattleBase.Commands
{
    public sealed class CommandSelectFirstUnconquered : CommandBase, IInjectable
    {
        private TerritoriesModel _territoriesModel;

        [Inject]
        public void Construct(TerritoriesModel territoriesModel) =>
            _territoriesModel = territoriesModel ?? throw new ArgumentNullException(nameof(territoriesModel));

        public override void Execute()
        {
            if (_territoriesModel.TryGetFirstUnconqueredTerritoryIndex(out int index))
                _territoriesModel.SetSelectedTerritoryIndex(index);
        }
    }
}