using System;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class KeyboardDragHandler
    {
        private const string KeyboardAxisX = "Horizontal";
        private const string KeyboardAxisY = "Vertical";

        private readonly float _keyboardSpeed;
        private readonly float _axisThreshold;

        public KeyboardDragHandler(float keyboardSpeed, float axisThreshold)
        {
            if (keyboardSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(keyboardSpeed), keyboardSpeed, "Value must be positive");

            if (axisThreshold <= 0)
                throw new ArgumentOutOfRangeException(nameof(axisThreshold), axisThreshold, "Value must be positive");

            _keyboardSpeed = keyboardSpeed;
            _axisThreshold = axisThreshold;
        }

        public Vector3? Update(float deltaTime)
        {
            float x = Input.GetAxisRaw(KeyboardAxisX);
            float z = Input.GetAxisRaw(KeyboardAxisY);

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