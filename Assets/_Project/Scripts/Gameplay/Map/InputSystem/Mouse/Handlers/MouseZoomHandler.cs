using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class MouseZoomHandler
    {
        private readonly float _zoomSensitivity;
        private readonly float _scrollThreshold;

        public MouseZoomHandler(IZoomConfig config)
        {
            _zoomSensitivity = config.ScrollSensitivity;
            _scrollThreshold = config.ScrollThreshold;
        }

        public float? Update()
        {
            float scroll = Input.GetAxis(InputConstants.MouseScrollWheel);

            if (Mathf.Abs(scroll) > _scrollThreshold)
                return scroll * _zoomSensitivity;

            return null;
        }
    }
}