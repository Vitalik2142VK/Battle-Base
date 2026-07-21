using BattleBase.Core;
using BattleBase.Gameplay.Actors.Production;
using System;

namespace BattleBase.Gameplay.AI
{
    public class MultiActionCommand : ICommand
    {
        private readonly IProductionOption _productionOption;
        private readonly int _count;

        public MultiActionCommand(IProductionOption productionOption, int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            _productionOption = productionOption ?? throw new ArgumentNullException(nameof(productionOption));
            _count = ++count;
        }

        public void Execute()
        {
            for (int i = 0; i < _count; i++)
                _productionOption.Execute();
        }
    }
}