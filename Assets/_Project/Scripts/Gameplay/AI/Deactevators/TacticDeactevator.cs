using System.Collections.Generic;

namespace BattleBase.Gameplay.AI.Deactevators
{
    public class TacticDeactevator : ITacticDeactevator
    {
        private List<IDeactevator> _deactevators;

        public bool IsDeactivate => IsGetDeactivate();

        public void AddDeactevator(IDeactevator deactevator)
        {
            if (deactevator == null)
                throw new System.ArgumentNullException(nameof(deactevator));

            if (_deactevators == null)
                _deactevators = new List<IDeactevator>();

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