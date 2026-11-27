using UnityEditor;
using UnityEngine;

namespace EnvironmentPainterTool
{
    public partial class EnvironmentPainter : EditorWindow
    {
        [SerializeField] private BrushSettings _brushSettings = new BrushSettings();
        [SerializeField] private bool _placementEnabled = false;
        [SerializeField] private bool _useBrush = false;

        private EnvironmentBrush _brush = new EnvironmentBrush();

        [MenuItem("Tools/Environment Painter")]
        private static void OpenWindow()
        {
            GetWindow<EnvironmentPainter>("Environment Painter");
        }

        private void OnEnable()
        {
            InitializePrefabList();
            SceneView.duringSceneGui -= OnSceneGUI;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.duringSceneGui -= OnSceneGUI;
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField(
                "Environment Painter",
                EditorStyles.boldLabel);

            DrawPlacementButton();

            EditorGUILayout.Space();

            DrawPrefabList();

            EditorGUILayout.Space();

            DrawPlacementSettings();

            EditorGUILayout.Space();

            DrawRandomSettings();

            EditorGUILayout.Space();

            DrawLayerMask();
        }

        private void DrawPlacementSettings()
        {
            EditorGUILayout.LabelField("Placement", EditorStyles.boldLabel);

            _useBrush = EditorGUILayout.Toggle(
                "Use Brush",
                _useBrush);

            if (_useBrush)
            {
                if (_brushSettings == null)
                    _brushSettings = new BrushSettings();

                _brushSettings.DrawGUI();
            }
        }

        private void DrawPlacementButton()
        {
            string buttonText = _placementEnabled
                ? "Stop Placement"
                : "Start Placement";

            Color previousColor = GUI.backgroundColor;

            GUI.backgroundColor = _placementEnabled
                ? new Color(1f, 0.65f, 0.55f)
                : new Color(0.65f, 1f, 0.65f);

            if (GUILayout.Button(buttonText, GUILayout.Height(32f)))
            {
                _placementEnabled = !_placementEnabled;

                if (!_placementEnabled)
                {
                    _brush.EndStroke();
                    _brushUndoGroup = -1;
                }

                SceneView.RepaintAll();
            }

            GUI.backgroundColor = previousColor;
        }
    }
}
