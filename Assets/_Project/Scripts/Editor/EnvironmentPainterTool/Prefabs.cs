using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace EnvironmentPainterTool
{
    public partial class EnvironmentPainter
    {
        [SerializeField]
        private List<GameObject> _prefabs = new List<GameObject>();

        private ReorderableList _prefabList;
        private Vector2 _prefabScrollPosition;

        private void InitializePrefabList()
        {
            if (_prefabs == null)
                _prefabs = new List<GameObject>();

            _prefabList = new ReorderableList(
                _prefabs,
                typeof(GameObject),
                true,
                true,
                false,
                true);

            _prefabList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, "Prefab List");
            };

            _prefabList.drawElementCallback = (rect, index, active, focused) =>
            {
                if (index < 0 || index >= _prefabs.Count)
                    return;

                rect.y += 2f;
                rect.height = EditorGUIUtility.singleLineHeight;

                _prefabs[index] = (GameObject)EditorGUI.ObjectField(
                    rect,
                    _prefabs[index],
                    typeof(GameObject),
                    false);
            };

            _prefabList.onRemoveCallback = list =>
            {
                if (list.index < 0 || list.index >= _prefabs.Count)
                    return;

                _prefabs.RemoveAt(list.index);
                list.index = Mathf.Clamp(list.index, 0, _prefabs.Count - 1);
            };
        }

        private void DrawPrefabList()
        {
            if (_prefabList == null)
                InitializePrefabList();

            EditorGUILayout.LabelField("Prefab List", EditorStyles.boldLabel);

            _prefabScrollPosition = EditorGUILayout.BeginScrollView(
                _prefabScrollPosition,
                GUILayout.Height(220f));

            _prefabList.DoLayoutList();

            EditorGUILayout.EndScrollView();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Selected Prefabs"))
                AddSelectedPrefabs();

            if (GUILayout.Button("Clear"))
                _prefabs.Clear();

            EditorGUILayout.EndHorizontal();
        }

        private void AddSelectedPrefabs()
        {
            if (_prefabs == null)
                _prefabs = new List<GameObject>();

            foreach (Object selectedObject in Selection.objects)
            {
                GameObject prefab = selectedObject as GameObject;

                if (prefab == null)
                    continue;

                if (!AssetDatabase.Contains(prefab))
                    continue;

                if (PrefabUtility.GetPrefabAssetType(prefab) ==
                    PrefabAssetType.NotAPrefab)
                {
                    continue;
                }

                if (_prefabs.Contains(prefab))
                    continue;

                _prefabs.Add(prefab);
            }

            if (_prefabList != null)
                _prefabList.list = _prefabs;

            Repaint();
        }

        private GameObject GetRandomPrefab()
        {
            if (_prefabs == null || _prefabs.Count == 0)
                return null;

            List<GameObject> validPrefabs = new List<GameObject>();

            foreach (GameObject prefab in _prefabs)
            {
                if (prefab != null)
                    validPrefabs.Add(prefab);
            }

            if (validPrefabs.Count == 0)
                return null;

            int index = Random.Range(0, validPrefabs.Count);
            return validPrefabs[index];
        }
    }
}
