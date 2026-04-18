#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
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
        private const int MaterialIndex = 1;
        private const bool IsTwoWayDependency = true;

        private static Color SelectedColor = Color.green;
        private static Color AdjacentColor = Color.yellow;

        private readonly static HashSet<MeshRenderer> s_affectedRenderers = new();
        private readonly static FieldInfo s_adjacentsField;

        private static Territory s_currentSelectedTerritory;

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
                s_currentSelectedTerritory = territory;
                ApplyColors(territory);
            }
            else
            {
                s_currentSelectedTerritory = null;
            }
        }

        private static void OnSceneGUI(SceneView sceneView)
        {
            if (Application.isPlaying)
                return;

            if (s_currentSelectedTerritory == null)
                return;

            Event e = Event.current;

            if (e.type == EventType.MouseDown && e.button == 0 && e.control && !e.alt)
            {
                GameObject clickedObject = HandleUtility.PickGameObject(e.mousePosition, false);

                if (clickedObject != null && clickedObject.TryGetComponent(out Territory clickedTerritory))
                {
                    if (clickedTerritory == s_currentSelectedTerritory)
                        return;

                    ProcessAdjacent(s_currentSelectedTerritory, clickedTerritory);

                    ResetColors();
                    ApplyColors(s_currentSelectedTerritory);

                    EditorUtility.SetDirty(s_currentSelectedTerritory);
                    e.Use();
                }
            }
        }

        private static void ProcessAdjacent(Territory owner, Territory clicked)
        {
<<<<<<< HEAD
            Undo.RecordObject(owner, UndoMessage);
=======
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

            Undo.RecordObject(owner, "Toggle Adjacent Territory");
>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96

            if (IsTwoWayDependency)
                Undo.RecordObject(clicked, "Toggle Adjacent Territory");

            bool alreadyConnected = owner.Adjacents.Contains(clicked);

            if (alreadyConnected)
<<<<<<< HEAD
                owner.RemoveAdjacent(clicked);
            else
                owner.AddAdjacent(clicked);
=======
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
>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96

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
<<<<<<< HEAD
            MaterialPropertyBlock materialPropertyBlock = new();
            renderer.GetPropertyBlock(materialPropertyBlock, MaterialIndex);
            materialPropertyBlock.SetColor(ColorPropertyID, color);
            renderer.SetPropertyBlock(materialPropertyBlock, MaterialIndex);
            s_affectedRenderers.Add(renderer);
=======
            MaterialPropertyBlock mpb = new();
            renderer.GetPropertyBlock(mpb);
            mpb.SetColor(ColorProperty, color);
            renderer.SetPropertyBlock(mpb);
            _affectedRenderers.Add(renderer);
>>>>>>> 254fefc1707fdf056ae43f021bd40b057aec9a96
        }

        private static void ResetColors()
        {
            foreach (MeshRenderer renderer in s_affectedRenderers)
            {
                if (renderer != null)
                    renderer.SetPropertyBlock(null);
            }

            s_affectedRenderers.Clear();
        }
    }
}
#endif