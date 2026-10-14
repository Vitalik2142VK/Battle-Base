using System;

namespace BattleBase.Gameplay.Actors.Movement
{
    public class ModifiedMoveConfig : IMoveConfig
    {
        private readonly IMoveConfig _defaultConfig;

        public ModifiedMoveConfig(IMoveConfig moveConfig)
        {
            _defaultConfig = moveConfig ?? throw new ArgumentNullException(nameof(moveConfig));

            Reset();
        }

        public float AngularSpeed => _defaultConfig.AngularSpeed;

        public float Acceleration => _defaultConfig.Acceleration;

        public float StoppingDistance => _defaultConfig.StoppingDistance;

        public float Speed { get; private set; }

        public void Modify(MoveConfigModificator modificator)
        {
            if (modificator == null)
                throw new ArgumentNullException(nameof(modificator));

            Speed = _defaultConfig.Speed * modificator.SpeedCoefficient;
        }

        public void Reset()
        {
            Speed = _defaultConfig.Speed;
        }
    }
}
