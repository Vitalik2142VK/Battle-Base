using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BattleBase.Gameplay.Map.InputSystem
{
    public class UIPointerChecker : IUIPointerChecker
    {
        private readonly List<Canvas> _uiCanvases = new();
        private readonly List<RaycastResult> _raycastResults = new();

        public void AddCanvas(Canvas canvas)
        {
            if (canvas != null)
                _uiCanvases.Add(canvas);
        }

        public bool IsPointerOverUI(Vector2 screenPosition)
        {
            if (EventSystem.current == null)
                return false;

            foreach (Canvas canvas in _uiCanvases)
            {
                if (canvas == null || canvas.enabled == false || canvas.gameObject.activeInHierarchy == false)
                    continue;

                GraphicRaycaster raycaster = canvas.GetComponent<GraphicRaycaster>();

                if (raycaster == null)
                    continue;

                PointerEventData eventData = new(EventSystem.current)
                {
                    position = screenPosition
                };

                _raycastResults.Clear();
                raycaster.Raycast(eventData, _raycastResults);

                if (_raycastResults.Count > 0)
                    return true;
            }

            return false;
        }
    }
}