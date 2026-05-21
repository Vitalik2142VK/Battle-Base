using System;

namespace BattleBase.Gameplay.Actors.DamageSystem
{
    public class OnTimeDestroyable : IOnTimeDestroyable
    {
        private bool _isEnamble;

        public event Action Destroyed;

        public OnTimeDestroyable()
        {
            _isEnamble = false;
        }

        public void Enable()
        {
            _isEnamble = true;
        }

        public void Disable()
        {
            _isEnamble = false;
        }

        public void Destroy()
        {
            if (_isEnamble == false)
                Destroyed?.Invoke();
        }
    }
}