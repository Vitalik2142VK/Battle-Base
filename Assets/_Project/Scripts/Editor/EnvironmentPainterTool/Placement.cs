using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnvironmentPainterTool
{
    public partial class EnvironmentPainter
    {
        private const float RaycastDistance = 10000f;

        private int _brushUndoGroup = -1;

        private void OnSceneGUI(SceneView sceneView)
        {
            Event currentEvent = Event.current;

            if (!_placementEnabled)
            {
                _brush.EndStroke();
                _brushUndoGroup = -1;
                return;
            }

            if (currentEvent == null || currentEvent.alt)
                return;

            Ray ray = HandleUtility.GUIPointToWorldRay(
                currentEvent.mousePosition);

            bool hasHit = Physics.Raycast(
                ray,
                out RaycastHit hit,
                RaycastDistance,
                _layerMask,
                QueryTriggerInteraction.Ignore);

            if (_useBrush && hasHit)
            {
                _brush.Draw(
                    hit.point,
                    hit.normal,
                    _brushSettings);
            }

            if (hasHit)
            {
                HandleUtility.AddDefaultControl(
                    GUIUtility.GetControlID(FocusType.Passive));
            }

            if (currentEvent.type == EventType.MouseDown &&
                currentEvent.button == 0 &&
                hasHit)
            {
                if (_useBrush)
                {
                    _brushUndoGroup = BeginUndoGroup(
                        "Paint Environment Prefabs");

                    _brush.BeginStroke(
                        hit.point,
                        hit.normal,
                        _brushSettings,
                        _layerMask,
                        GetRandomPrefab,
                        (prefab, position, normal) =>
                            SpawnPrefab(prefab, position, normal));
                }
                else
                {
                    int undoGroup = BeginUndoGroup(
                        "Place Environment Prefab");

                    SpawnPrefab(
                        GetRandomPrefab(),
                        hit.point,
                        hit.normal);

                    Undo.CollapseUndoOperations(undoGroup);
                }

                currentEvent.Use();
                sceneView.Repaint();
                return;
            }

            if (currentEvent.type == EventType.MouseDrag &&
                currentEvent.button == 0 &&
                _useBrush)
            {
                if (_brushUndoGroup >= 0 && hasHit)
                {
                    _brush.ContinueStroke(
                        hit.point,
                        hit.normal,
                        _brushSettings,
                        _layerMask,
                        GetRandomPrefab,
                        (prefab, position, normal) =>
                            SpawnPrefab(prefab, position, normal));
                }

                currentEvent.Use();
                sceneView.Repaint();
                return;
            }

            if (currentEvent.type == EventType.MouseUp &&
                currentEvent.button == 0)
            {
                _brush.EndStroke();

                if (_brushUndoGroup >= 0)
                {
                    Undo.CollapseUndoOperations(_brushUndoGroup);
                    _brushUndoGroup = -1;
                }
            }
        }

        private bool SpawnPrefab(GameObject prefab, Vector3 position, Vector3 surfaceNormal)
        {
            if (!_placementEnabled)
                return false;

            if (prefab == null)
                return false;

            Scene activeScene = SceneManager.GetActiveScene();

            GameObject instance = PrefabUtility.InstantiatePrefab(
                prefab,
                activeScene) as GameObject;

            if (instance == null)
                return false;

            Undo.RegisterCreatedObjectUndo(
                instance,
                "Place Environment Prefab");

            instance.transform.position = position;

            Quaternion surfaceRotation = Quaternion.Euler(Vector3.up);

            if (_alignToSurface == false)
                surfaceRotation = Quaternion.FromToRotation(Vector3.up, surfaceNormal);

            instance.transform.rotation =
                surfaceRotation * GetRandomRotation();

            Vector3 randomScale = GetRandomScale();

            instance.transform.localScale = Vector3.Scale(
                instance.transform.localScale,
                randomScale);

            return true;
        }

        private int BeginUndoGroup(string undoName)
        {
            Undo.IncrementCurrentGroup();

            int group = Undo.GetCurrentGroup();

            Undo.SetCurrentGroupName(undoName);

            return group;
        }
    }
}
