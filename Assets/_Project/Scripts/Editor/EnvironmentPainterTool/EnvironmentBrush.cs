using System;
using UnityEditor;
using UnityEngine;

namespace EnvironmentPainterTool
{
    public class EnvironmentBrush
    {
        private bool _isPainting;
        private bool _hasLastPosition;
        private Vector3 _lastPosition;

        public void Draw(Vector3 position, Vector3 normal, BrushSettings settings)
        {
            if (settings == null)
                return;

            Color previousColor = Handles.color;

            Handles.color = new Color(0.2f, 0.8f, 1f, 0.9f);
            Handles.DrawWireDisc(position, normal, settings.Radius);

            Handles.color = previousColor;
        }

        public void BeginStroke(
            Vector3 position,
            Vector3 normal,
            BrushSettings settings,
            LayerMask layerMask,
            Func<GameObject> getRandomPrefab,
            Action<GameObject, Vector3, Vector3> spawnPrefab)
        {
            _isPainting = true;
            _hasLastPosition = true;
            _lastPosition = position;

            PaintAt(
                position,
                normal,
                settings,
                layerMask,
                getRandomPrefab,
                spawnPrefab);
        }

        public void ContinueStroke(
            Vector3 position,
            Vector3 normal,
            BrushSettings settings,
            LayerMask layerMask,
            Func<GameObject> getRandomPrefab,
            Action<GameObject, Vector3, Vector3> spawnPrefab)
        {
            if (!_isPainting)
                return;

            if (!_hasLastPosition)
            {
                _lastPosition = position;
                _hasLastPosition = true;

                PaintAt(
                    position,
                    normal,
                    settings,
                    layerMask,
                    getRandomPrefab,
                    spawnPrefab);

                return;
            }

            float distance = Vector3.Distance(_lastPosition, position);
            float spacing = Mathf.Max(0.1f, settings.Spacing);

            if (distance < spacing)
                return;

            int steps = Mathf.Max(1, Mathf.FloorToInt(distance / spacing));

            for (int i = 1; i <= steps; i++)
            {
                float t = (float)i / steps;
                Vector3 samplePosition = Vector3.Lerp(
                    _lastPosition,
                    position,
                    t);

                PaintAt(
                    samplePosition,
                    normal,
                    settings,
                    layerMask,
                    getRandomPrefab,
                    spawnPrefab);
            }

            _lastPosition = position;
        }

        public void EndStroke()
        {
            _isPainting = false;
            _hasLastPosition = false;
        }

        private void PaintAt(
            Vector3 center,
            Vector3 normal,
            BrushSettings settings,
            LayerMask layerMask,
            Func<GameObject> getRandomPrefab,
            Action<GameObject, Vector3, Vector3> spawnPrefab)
        {
            if (settings == null ||
                getRandomPrefab == null ||
                spawnPrefab == null)
            {
                return;
            }

            Vector3 tangent = Vector3.Cross(normal, Vector3.up);

            if (tangent.sqrMagnitude < 0.001f)
                tangent = Vector3.Cross(normal, Vector3.right);

            tangent.Normalize();

            Vector3 bitangent = Vector3.Cross(normal, tangent).normalized;

            const float rayHeight = 100f;
            int density = Mathf.Max(1, settings.Density);

            for (int i = 0; i < density; i++)
            {
                GameObject prefab = getRandomPrefab();

                if (prefab == null)
                    continue;

                float angle = UnityEngine.Random.Range(0f, Mathf.PI * 2f);
                float distance = Mathf.Sqrt(UnityEngine.Random.value) *
                                 settings.Radius;

                float x = Mathf.Cos(angle) * distance;
                float z = Mathf.Sin(angle) * distance;

                Vector3 samplePosition =
                    center + tangent * x + bitangent * z;

                Ray ray = new Ray(
                    samplePosition + normal * rayHeight,
                    -normal);

                if (!Physics.Raycast(
                    ray,
                    out RaycastHit hit,
                    rayHeight * 2f,
                    layerMask,
                    QueryTriggerInteraction.Ignore))
                {
                    continue;
                }

                spawnPrefab(prefab, hit.point, hit.normal);
            }
        }
    }
}
