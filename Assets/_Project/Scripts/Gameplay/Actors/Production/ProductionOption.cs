using BattleBase.Core;
using System;
using System.Collections.Generic;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionOption : IProductionOption
    {
        private readonly List<ICommand> _commands;
        private readonly ICommand _command;

        public ProductionOption(ICommand command, IProductionData productionData, TypeProduction type)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));
            Data = productionData ?? throw new ArgumentNullException(nameof(productionData));
            Type = type;

            _commands = null;
        }

        public TypeProduction Type { get; }

        public IProductionData Data { get; }

        public int NumberComamnds { get; private set; }

        public void AddCommand(ICommand command)
        {
            
        }

        public void Execute(int commnadIndex = 0)
        {
            _command.Execute();
        }
    }
}