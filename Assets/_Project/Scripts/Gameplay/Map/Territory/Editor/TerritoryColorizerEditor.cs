#if UNITY_EDITOR
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BattleBase.Gameplay.Map.Editor
{
    [InitializeOnLoad]
    public static class TerritoryColorizerEditor
    {
        private const string ColorProperty = "_BaseColor";
        private const string AdjacentsField = "_adjacents";
        private const string UndoMessage = "Toggle Adjacent Territory";

        private const int MaterialIndex = 1;
        private const bool IsTwoWayDependency = true;

        private static readonly int ColorPropertyID = Shader.PropertyToID(ColorProperty);

        private static readonly Color SelectedColor = Color.green;
        private static readonly Color AdjacentColor = Color.yellow;

        private readonly static HashSet<MeshRenderer> _affectedRenderers = new();
        private readonly static FieldInfo s_adjacentsField;

        private static Territory _currentSelectedTerritory;

        static TerritoryColorizerEditor()
        {
            s_adjacentsField = typeof(Territory).GetField(AdjacentsField,
                BindingFlags.NonPublic | BindingFlags.Instance);

            Selection.selectionChanged += OnSelectionChanged;
            SceneView.duringSceneGui += OnSceneGUI;
        }

        private static void OnSelectionChanged()
        {
            if (Application.isPlaying) return;

            ResetColors();
            GameObject selected = Selection.activeGameObject;

            if (selected != null && selected.TryGetComponent(out Territory territory))
            {
                _currentSelectedTerritory = territory;
                ApplyColors(territory);
            }
            else
            {
                _currentSelectedTerritory = null;
            }
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            if (Application.isPlaying)
                return;

            if (_currentSelectedTerritory == null)
                return;

            Event e = Event.current;

            if (e.type == EventType.MouseDown && e.button == 0 && e.control && !e.alt)
            {
                GameObject clickedObject = HandleUtility.PickGameObject(e.mousePosition, false);

                if (clickedObject != null && clickedObject.TryGetComponent(out Territory clickedTerritory))
                {
                    if (clickedTerritory == _currentSelectedTerritory)
                        return;

                    ProcessAdjacent(_currentSelectedTerritory, clickedTerritory);

                    ResetColors();
                    ApplyColors(_currentSelectedTerritory);

                    EditorUtility.SetDirty(_currentSelectedTerritory);
                    e.Use();
                }
            }
        }

        private static void ProcessAdjacent(Territory owner, Territory clicked)
        {
            if (s_adjacentsField?.GetValue(owner) is not List<Territory> ownerAdjacents)
                return;

            List<Territory> clickedAdjacents;

            if (IsTwoWayDependency)
            {
                if (s_adjacentsField?.GetValue(clicked) is List<Territory> list)
                    clickedAdjacents = list;
                else
                    return;
            }

            Undo.RecordObject(owner, UndoMessage);

            if (IsTwoWayDependency)
                Undo.RecordObject(clicked, UndoMessage);

            bool alreadyConnected = ownerAdjacents.Contains(clicked);

            if (alreadyConnected)
            {
                ownerAdjacents.Remove(clicked);

                if (IsTwoWayDependency && clickedAdjacents != null)
                    clickedAdjacents.Remove(owner);
            }
            else
            {
                ownerAdjacents.Add(clicked);

                if (IsTwoWayDependency && clickedAdjacents != null)
                    clickedAdjacents.Add(owner);
            }

            s_adjacentsField.SetValue(owner, ownerAdjacents);

            if (IsTwoWayDependency && clickedAdjacents != null)
                s_adjacentsField.SetValue(clicked, clickedAdjacents);

            EditorUtility.SetDirty(owner);

            if (IsTwoWayDependency)
                EditorUtility.SetDirty(clicked);
        }

        private static void ApplyColors(Territory territory)
        {
            if (territory.TryGetComponent(out MeshRenderer selectedRenderer))
                SetColor(selectedRenderer, SelectedColor);

            if (s_adjacentsField?.GetValue(territory) is List<Territory> adjacents)
            {
                foreach (Territory adjacent in adjacents)
                {
                    if (adjacent != null)
                    {
                        if (adjacent.TryGetComponent<MeshRenderer>(out var adjRenderer))
                            SetColor(adjRenderer, AdjacentColor);
                    }
                }
            }
        }

        private static void SetColor(MeshRenderer renderer, Color color)
        {
            MaterialPropertyBlock mpb = new();
            renderer.GetPropertyBlock(mpb, MaterialIndex);
            mpb.SetColor(ColorPropertyID, color);
            renderer.SetPropertyBlock(mpb, MaterialIndex);
            _affectedRenderers.Add(renderer);
        }

        private static void ResetColors()
        {
            foreach (MeshRenderer renderer in _affectedRenderers)
            {
                if (renderer != null)
                    renderer.SetPropertyBlock(null, MaterialIndex);
            }

            _affectedRenderers.Clear();
        }
    }
}
#endif