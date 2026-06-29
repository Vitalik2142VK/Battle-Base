using BattleBase.Core;
using System;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionOption
    {
        private readonly ICommand _command;

        public ProductionOption(ICommand command, IProductionData productionData)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            ProductionData = productionData ?? throw new ArgumentNullException(nameof(productionData));
        }

        public IProductionData ProductionData { get; }

        public void Execute() => 
            _command.Execute();
    }
}