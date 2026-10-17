using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleBase.Gameplay.CameraNavigation.InputReader
{
    public class UIPointerChecker : IUIPointerChecker
    {
        private readonly List<RaycastResult> _raycastResults = new();

        private int _lastFrame = -1;
        private Vector2 _lastPosition;
        private bool _lastResult;

        public bool IsPointerOverUI(Vector2 screenPosition)
        {
            int currentFrame = Time.frameCount;

            if (currentFrame == _lastFrame && screenPosition == _lastPosition)
                return _lastResult;

            _lastFrame = currentFrame;
            _lastPosition = screenPosition;
            _lastResult = RaycastUI(screenPosition);

            return _lastResult;
        }

        private bool RaycastUI(Vector2 screenPosition)
        {
            if (EventSystem.current == null)
                return false;

            PointerEventData eventData = new(EventSystem.current) 
            { 
                position = screenPosition 
            };

            _raycastResults.Clear();
            EventSystem.current.RaycastAll(eventData, _raycastResults);

            foreach (RaycastResult result in _raycastResults)
            {
                if (result.module is not GraphicRaycaster raycaster)
                    continue;

                if (raycaster.enabled == false || raycaster.gameObject.activeInHierarchy == false)
                    continue;

                if (result.gameObject.GetComponentInParent<UIPointerBlockerMarker>() != null)
                    return true;
            }

            return false;
        }
    }
}