using BattleBase.Core;
using System;

namespace BattleBase.Gameplay.Actors.Production
{
    public class ProductionOption : IProductionOption
    {
        private readonly ICommand _command;

        public ProductionOption(ICommand command, IProductionData data, TypeProduction type = TypeProduction.Other)
        {
            _command = command ?? throw new ArgumentNullException(nameof(command));

            Data = data ?? throw new ArgumentNullException(nameof(data));
            Type = type;
        }

        public TypeProduction Type { get; }

        public IProductionData Data { get; }

        public void Execute() =>
            _command.Execute();
    }
}