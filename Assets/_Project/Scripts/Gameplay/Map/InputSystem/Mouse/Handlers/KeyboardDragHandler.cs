using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class KeyboardDragHandler
    {
        private readonly float _keyboardSpeed;
        private readonly float _axisThreshold;

        public KeyboardDragHandler(IDragConfig config)
        {
            _keyboardSpeed = config.KeyboardSpeed;
            _axisThreshold = config.KeyboardAxisThreshold;
        }

        public Vector3? Update(float deltaTime)
        {
            float x = Input.GetAxisRaw(InputConstants.KeyboardAxisX);
            float z = Input.GetAxisRaw(InputConstants.KeyboardAxisY);

            if (Mathf.Abs(x) > _axisThreshold || Mathf.Abs(z) > _axisThreshold)
            {
                Vector3 move = _keyboardSpeed * deltaTime * new Vector3(x, 0, z);

                return -move;
            }
            else
            {
                return null;
            }
        }
    }
}