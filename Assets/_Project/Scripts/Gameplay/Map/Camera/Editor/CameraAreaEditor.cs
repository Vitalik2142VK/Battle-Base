#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace BattleBase.Gameplay.Map.Editor
{
    [CustomEditor(typeof(CameraArea))]
    public class CameraAreaEditor : UnityEditor.Editor
    {
        private const float Two = 2f;

        private static readonly Color s_colliderBoundsColor = Color.blue;
        private static readonly Color s_overshootColor = Color.red;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawCameraAreaGizmos(CameraArea area, GizmoType gizmoType)
        {
            if (area == null) 
                return;

            Bounds bounds = area.ColliderBounds;
            Bounds overshootBounds = area.OvershootBounds;

            Gizmos.color = s_colliderBoundsColor;
            Gizmos.DrawWireCube(bounds.center, bounds.size);

            Gizmos.color = s_overshootColor;
            Gizmos.DrawWireCube(overshootBounds.center, overshootBounds.size);
        }
    }
}
#endif