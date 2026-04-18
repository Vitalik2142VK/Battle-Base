#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CustomEditor(typeof(CameraFrustumProjector))]
    public class CameraFrustumProjectorEditor : UnityEditor.Editor
    {
        private const float SphereRadius = 0.1f;
        private const int CornerCount = 4;

        private static readonly Color s_gizmoColor = Color.yellow;

        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
        private static void DrawCameraFrustumProjector(CameraFrustumProjector projector, GizmoType gizmoType)
        {
            if (projector == null)
                return;

            if (projector.Area == null)
                return;

            Camera cam = projector.GetComponent<Camera>();

            if (cam == null)
                return;

            List<Vector3> corners = new();
            projector.ProjectCornersOntoPlaneFromPosition(cam.transform.position, corners);

            if (corners.Count != CornerCount)
                return;

            Gizmos.color = s_gizmoColor;

            foreach (Vector3 corner in corners)
                Gizmos.DrawWireSphere(corner, SphereRadius);

            for (int i = 0; i < corners.Count; i++)
                Gizmos.DrawLine(corners[i], corners[(i + 1) % corners.Count]);
        }
    }
}
#endif