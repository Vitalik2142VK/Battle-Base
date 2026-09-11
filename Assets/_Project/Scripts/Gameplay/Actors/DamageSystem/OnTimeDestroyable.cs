using System;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public class OnTimeDestroyable : IOnTimeDestroyable
    {
        private bool _isEnable;

        public event Action Destroyed;

        public OnTimeDestroyable()
        {
            _isEnable = false;
        }

        public Type KeyType => typeof(IOnTimeDestroyable);

        public void Enable()
        {
            _isEnable = true;
        }

        public void Disable()
        {
            _isEnable = false;
        }

        public void Destroy()
        {
            if (_isEnable == false)
                Destroyed?.Invoke();
        }
    }
}