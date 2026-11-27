using System;
using UnityEditor;
using UnityEngine;

namespace EnvironmentPainterTool
{
    [Serializable]
    public class BrushSettings
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _density = 5;
        [SerializeField] private float _spacing = 1f;

        public float Radius => _radius;
        public int Density => _density;
        public float Spacing => _spacing;

        public void DrawGUI()
        {
            EditorGUILayout.LabelField("Brush Settings", EditorStyles.boldLabel);

            _radius = EditorGUILayout.Slider(
                "Radius",
                _radius,
                0.1f,
                100f);

            _density = EditorGUILayout.IntSlider(
                "Objects per Stamp",
                _density,
                1,
                100);

            _spacing = EditorGUILayout.Slider(
                "Stroke Spacing",
                _spacing,
                0.1f,
                10f);
        }
    }
}
