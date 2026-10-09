using System;

namespace BattleBase.UI
{
    public interface IChangeableItem
    {
        public event Action Changed;
    }
}