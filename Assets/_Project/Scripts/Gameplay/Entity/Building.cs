using BattleBase.Gameplay.MiniMap;
using UnityEngine;

namespace BattleBase.Gameplay
{
    public abstract class Building : Entity, IMiniMapTrackable
    {
        [SerializeField] private Color _color;

        private Vector2? _cachedWorldSize;

        public Color Color => _color;

        public Vector2 WorldSize => GetSize();

        public Vector3 WorldPosition => transform.position;

        public float RotationY => transform.eulerAngles.y;

        private Vector2 GetSize()
        {
            if (_cachedWorldSize.HasValue)
                return _cachedWorldSize.Value;

            Vector2 localSize = ComputeLocalSize();
            Vector3 scale = transform.lossyScale;

            float width = localSize.x * Mathf.Abs(scale.x);
            float height = localSize.y * Mathf.Abs(scale.z);

            _cachedWorldSize = new Vector2(width, height);

            return _cachedWorldSize.Value;
        }

        private Vector2 ComputeLocalSize()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            Vector3 localMin;
            Vector3 localMax;

            if (renderers.Length == 0)
            {
                Collider collider = GetComponentInChildren<Collider>();

                if (collider != null)
                {
                    Bounds b = collider.bounds;
                    localMin = transform.InverseTransformPoint(b.min);
                    localMax = transform.InverseTransformPoint(b.max);

                    return new Vector2(localMax.x - localMin.x, localMax.z - localMin.z);
                }

                Debug.LogWarning($"Build {name} has no renderers or colliders, returning default size 1x1", this);
                return Vector2.one;
            }

            localMin = Vector3.positiveInfinity;
            localMax = Vector3.negativeInfinity;
            bool anyPoint = false;

            foreach (Renderer rend in renderers)
            {
                if (rend == null) 
                    continue;

                Bounds localBounds = rend.localBounds;
                Vector3[] corners = GetLocalCorners(localBounds);

                foreach (Vector3 corner in corners)
                {
                    Vector3 worldCorner = rend.transform.TransformPoint(corner);
                    Vector3 cornerInBuildingSpace = transform.InverseTransformPoint(worldCorner);

                    if (cornerInBuildingSpace.x < localMin.x) 
                        localMin.x = cornerInBuildingSpace.x;

                    if (cornerInBuildingSpace.x > localMax.x) 
                        localMax.x = cornerInBuildingSpace.x;

                    if (cornerInBuildingSpace.z < localMin.z) 
                        localMin.z = cornerInBuildingSpace.z;

                    if (cornerInBuildingSpace.z > localMax.z) 
                        localMax.z = cornerInBuildingSpace.z;

                    anyPoint = true;
                }
            }

            if (anyPoint == false)
                return Vector2.one;

            float width = localMax.x - localMin.x;
            float height = localMax.z - localMin.z;

            return new Vector2(width, height);
        }

        private Vector3[] GetLocalCorners(Bounds bounds)
        {
            Vector3 center = bounds.center;
            Vector3 extents = bounds.extents;

            return new Vector3[]
            {
                center + new Vector3(-extents.x, -extents.y, -extents.z),
                center + new Vector3(-extents.x, -extents.y,  extents.z),
                center + new Vector3(-extents.x,  extents.y, -extents.z),
                center + new Vector3(-extents.x,  extents.y,  extents.z),
                center + new Vector3( extents.x, -extents.y, -extents.z),
                center + new Vector3( extents.x, -extents.y,  extents.z),
                center + new Vector3( extents.x,  extents.y, -extents.z),
                center + new Vector3( extents.x,  extents.y,  extents.z)
            };
        }
    }
}