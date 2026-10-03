using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public class TacticDeactivator : ITacticDeactivator
    {
        private List<IDeactivator> _deactevators;

        public bool IsDeactivate => IsGetDeactivate();

        public void AddDeactevator(IDeactivator deactevator)
        {
            if (deactevator == null)
                throw new System.ArgumentNullException(nameof(deactevator));

            _deactevators ??= new List<IDeactivator>();
            _deactevators.Add(deactevator);
        }

        private bool IsGetDeactivate()
        {
            if (_deactevators == null)
                return false;

            foreach (var deactevator in _deactevators)
            {
                if (deactevator.IsActive == false)
                    return false;
            }

            return true;
        }
    }
}