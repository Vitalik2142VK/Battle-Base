
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace EnvironmentPainterTool
{
    public partial class EnvironmentPainter
    {
        [SerializeField] private Vector3 _rotationMax = new(0f, 360f, 0f);
        [SerializeField] private bool _alignToSurface = false;
        [SerializeField] private bool _lockRotationX;
        [SerializeField] private bool _lockRotationY;
        [SerializeField] private bool _lockRotationZ;
        [SerializeField] private bool _uniformScale = true;

        [SerializeField] private Vector2 _uniformScaleRange = new(0.8f, 1.2f);

        [SerializeField] private Vector3 _scaleMin = Vector3.one * 0.8f;
        [SerializeField] private Vector3 _scaleMax = Vector3.one * 1.2f;

        [SerializeField] private LayerMask _layerMask = ~0;

        private void DrawRandomSettings()
        {
            EditorGUILayout.LabelField("Random Rotation", EditorStyles.boldLabel);

            EditorGUI.BeginChangeCheck();

            _alignToSurface = EditorGUILayout.Toggle("Align To Surface", _alignToSurface);

            EditorGUILayout.Space();

            _lockRotationX = EditorGUILayout.Toggle("Lock X", _lockRotationX);
            _lockRotationY = EditorGUILayout.Toggle("Lock Y", _lockRotationY);
            _lockRotationZ = EditorGUILayout.Toggle("Lock Z", _lockRotationZ);

            using (new EditorGUI.DisabledScope(_lockRotationX))
            {
                _rotationMax.x = EditorGUILayout.Slider(
                    "Max X",
                    _rotationMax.x,
                    0f,
                    360f);
            }

            using (new EditorGUI.DisabledScope(_lockRotationY))
            {
                _rotationMax.y = EditorGUILayout.Slider(
                    "Max Y",
                    _rotationMax.y,
                    0f,
                    360f);
            }

            using (new EditorGUI.DisabledScope(_lockRotationZ))
            {
                _rotationMax.z = EditorGUILayout.Slider(
                    "Max Z",
                    _rotationMax.z,
                    0f,
                    360f);
            }

            if (EditorGUI.EndChangeCheck())
                Repaint();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Random Scale", EditorStyles.boldLabel);

            _uniformScale = EditorGUILayout.Toggle(
                "Uniform Scale",
                _uniformScale);

            if (_uniformScale)
            {
                _uniformScaleRange = EditorGUILayout.Vector2Field(
                    "Scale Range",
                    _uniformScaleRange);

                _uniformScaleRange.x = Mathf.Max(0.01f, _uniformScaleRange.x);
                _uniformScaleRange.y = Mathf.Max(
                    _uniformScaleRange.x,
                    _uniformScaleRange.y);
            }
            else
            {
                _scaleMin = EditorGUILayout.Vector3Field(
                    "Scale Min",
                    _scaleMin);

                _scaleMax = EditorGUILayout.Vector3Field(
                    "Scale Max",
                    _scaleMax);

                _scaleMin.x = Mathf.Max(0.01f, _scaleMin.x);
                _scaleMin.y = Mathf.Max(0.01f, _scaleMin.y);
                _scaleMin.z = Mathf.Max(0.01f, _scaleMin.z);

                _scaleMax.x = Mathf.Max(_scaleMin.x, _scaleMax.x);
                _scaleMax.y = Mathf.Max(_scaleMin.y, _scaleMax.y);
                _scaleMax.z = Mathf.Max(_scaleMin.z, _scaleMax.z);
            }
        }

        private void DrawLayerMask()
        {
            string[] layerNames = InternalEditorUtility.layers;
            int displayedMask = 0;

            for (int i = 0; i < layerNames.Length; i++)
            {
                int layer = LayerMask.NameToLayer(layerNames[i]);

                if (layer >= 0 && (_layerMask.value & (1 << layer)) != 0)
                    displayedMask |= 1 << i;
            }

            displayedMask = EditorGUILayout.MaskField(
                "Raycast Layers",
                displayedMask,
                layerNames);

            int actualMask = 0;

            for (int i = 0; i < layerNames.Length; i++)
            {
                if ((displayedMask & (1 << i)) == 0)
                    continue;

                int layer = LayerMask.NameToLayer(layerNames[i]);

                if (layer >= 0)
                    actualMask |= 1 << layer;
            }

            _layerMask = actualMask;
        }

        private Quaternion GetRandomRotation()
        {
            float x = _lockRotationX
                ? 0f
                : Random.Range(0f, _rotationMax.x);

            float y = _lockRotationY
                ? 0f
                : Random.Range(0f, _rotationMax.y);

            float z = _lockRotationZ
                ? 0f
                : Random.Range(0f, _rotationMax.z);

            return Quaternion.Euler(x, y, z);
        }

        private Vector3 GetRandomScale()
        {
            if (_uniformScale)
            {
                float scale = Random.Range(
                    _uniformScaleRange.x,
                    _uniformScaleRange.y);

                return Vector3.one * scale;
            }

            return new Vector3(
                Random.Range(_scaleMin.x, _scaleMax.x),
                Random.Range(_scaleMin.y, _scaleMax.y),
                Random.Range(_scaleMin.z, _scaleMax.z));
        }
    }
}
