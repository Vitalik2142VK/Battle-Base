#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace FiXiK.SceneBrowserWindow.Editor
{
    public class SceneBrowserWindow : EditorWindow
    {
        private Texture2D _sceneTexture;
        private Texture2D _folderTexture;
        private Texture2D _openEyeTexture;
        private Texture2D _closedEyeTexture;

        private List<string> _hiddenScenes = new();
        private Vector2 _scrollPosition;
        private string[] _scenePaths;
        private bool _isShowHiddenScenes = false;

        [MenuItem(SceneBrowserConstants.MenuPath + SceneBrowserConstants.MenuName)]
        public static void ShowWindow() =>
            GetWindow<SceneBrowserWindow>(SceneBrowserConstants.WindowName);

        private void OnEnable()
        {
            EditorApplication.projectChanged += RefreshSceneList;
            LoadIcons();

            EditorApplication.delayCall += () =>
            {
                if (this == null) 
                    return;

                LoadHiddenScenes();
                RefreshSceneList();
                Repaint();
            };
        }

        private void OnDisable()
        {
            EditorApplication.projectChanged -= RefreshSceneList;
            SaveHiddenScenes();
        }

        private void OnGUI()
        {
            GUILayout.Label(SceneBrowserConstants.Tittle, EditorStyles.boldLabel);

            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);

            DrawVisibleScenes();

            _isShowHiddenScenes = EditorGUILayout.Foldout(_isShowHiddenScenes, SceneBrowserConstants.HiddenSceneBlockName, true);

            if (_isShowHiddenScenes && _hiddenScenes.Count > 0)
                DrawHiddenScenes();

            GUILayout.EndScrollView();

            DrawVersionLabel();
        }

        private void DrawVersionLabel()
        {
            GUILayout.BeginArea(new Rect(position.width - 80, 0, 80, 10));
            GUILayout.Label($"Версия: {SceneBrowserConstants.Version}", EditorStyles.miniLabel);
            GUILayout.EndArea();
        }

        private void DrawVisibleScenes()
        {
            foreach (string scenePath in _scenePaths)
                if (_hiddenScenes.Contains(scenePath) == false)
                    DrawSceneLine(scenePath, _openEyeTexture, SceneBrowserConstants.OpenEyeTextureTooltip, true);
        }

        private void DrawHiddenScenes()
        {
            foreach (string scenePath in _hiddenScenes.Where(SceneExists).ToList())
                DrawSceneLine(scenePath, _closedEyeTexture, SceneBrowserConstants.ClosedEyeTextureTooltip, false);
        }

        private void LoadIcons()
        {
            _sceneTexture = EditorGUIUtility.IconContent(SceneBrowserConstants.SceneIconName).image as Texture2D;
            _folderTexture = EditorGUIUtility.IconContent(SceneBrowserConstants.FolderIconName).image as Texture2D;
            _openEyeTexture = EditorGUIUtility.IconContent(SceneBrowserConstants.OpenEyeIconName).image as Texture2D;
            _closedEyeTexture = EditorGUIUtility.IconContent(SceneBrowserConstants.ClosedEyeIconName).image as Texture2D;
        }

        private void DrawSceneLine(string scenePath, Texture2D actionIcon, string tooltip, bool isVisible = true)
        {
            if (SceneExists(scenePath) == false)
                return;

            string sceneName = Path.GetFileNameWithoutExtension(scenePath);
            string folderTooltipWithPath = $"{SceneBrowserConstants.FolderTextureTooltip}\n{scenePath}";

            GUILayout.BeginHorizontal();

            GUILayout.Label(new GUIContent(_sceneTexture), GUILayout.Width(SceneBrowserConstants.LabelHeight), GUILayout.Height(SceneBrowserConstants.LabelHeight));

            if (GUILayout.Button(new GUIContent("", _folderTexture, folderTooltipWithPath), GUILayout.Width(SceneBrowserConstants.LabelHeight), GUILayout.Height(SceneBrowserConstants.LabelHeight)))
                ShowSceneInProject(scenePath);

            if (GUILayout.Button(sceneName, GUILayout.Height(SceneBrowserConstants.LabelHeight)))
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    EditorSceneManager.OpenScene(scenePath);

            if (GUILayout.Button(new GUIContent("", actionIcon, tooltip), GUILayout.Width(SceneBrowserConstants.LabelHeight), GUILayout.Height(SceneBrowserConstants.LabelHeight)))
                ToggleSceneVisibility(scenePath, isVisible);

            GUILayout.EndHorizontal();
        }

        private void ToggleSceneVisibility(string scenePath, bool isVisible)
        {
            if (isVisible)
                HideScene(scenePath);
            else
                UnhideScene(scenePath);
        }

        private void ShowSceneInProject(string scenePath)
        {
            Object sceneAsset = AssetDatabase.LoadAssetAtPath<Object>(scenePath);

            if (sceneAsset != null)
                EditorGUIUtility.PingObject(sceneAsset);
        }

        private void LoadHiddenScenes()
        {
            _hiddenScenes.Clear();
            string hiddenScenesData = EditorPrefs.GetString(SceneBrowserConstants.HiddenScenesKey, "");

            if (string.IsNullOrEmpty(hiddenScenesData)) 
                return;

            _hiddenScenes = hiddenScenesData.Split(';')
                .Where(s => !string.IsNullOrEmpty(s))
                .ToList();
        }

        private bool SceneExists(string scenePath) =>
            File.Exists(scenePath) && AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath) != null;

        private void SaveHiddenScenes() =>
            EditorPrefs.SetString(SceneBrowserConstants.HiddenScenesKey, string.Join(";", _hiddenScenes));

        private void RefreshSceneList()
        {
            if (EditorApplication.isPlaying && !AssetDatabase.IsValidFolder("Assets") == false)
                return;

            string[] searchFolders = new[] { SceneBrowserConstants.FolderName };

            _scenePaths = AssetDatabase.FindAssets(SceneBrowserConstants.SceneType, searchFolders)
                                      .Select(AssetDatabase.GUIDToAssetPath)
                                      .Where(path => path.EndsWith(".unity") && _hiddenScenes.Contains(path) == false)
                                      .OrderBy(path => Path.GetFileNameWithoutExtension(path))
                                      .ToArray();
        }

        private void HideScene(string scenePath)
        {
            if (_hiddenScenes.Contains(scenePath) == false)
            {
                _hiddenScenes.Add(scenePath);
                _hiddenScenes = _hiddenScenes.OrderBy(path => Path.GetFileNameWithoutExtension(path)).ToList();
                SaveHiddenScenes();
                RefreshSceneList();
            }
        }

        private void UnhideScene(string scenePath)
        {
            if (_hiddenScenes.Contains(scenePath))
            {
                _hiddenScenes.Remove(scenePath);
                SaveHiddenScenes();
                RefreshSceneList();
            }
        }
    }
}
#endif