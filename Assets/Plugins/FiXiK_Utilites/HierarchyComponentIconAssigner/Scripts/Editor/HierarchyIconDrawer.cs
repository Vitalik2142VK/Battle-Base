#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace FiXiK.HierarchyComponentIconAssigner
{
    public static class HierarchyIconDrawer
    {
        private static Dictionary<Type, Texture2D> s_Cache;
        private static HashSet<Type> s_EnabledTypes;
        private static Config s_CurrentSettings;
        private static bool s_IsSubscribed;

        [InitializeOnLoadMethod]
        private static void OnScriptsReloaded() =>
            EditorApplication.delayCall += Initialize;

        public static void Reload()
        {
            RefreshCache();
            EditorApplication.RepaintHierarchyWindow();
        }

        private static void Initialize()
        {
            RefreshCache();
            Subscribe();
        }

        private static void Subscribe()
        {
            if (s_IsSubscribed)
                return;

            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyGUI;
            EditorApplication.quitting += Unsubscribe;
            AssemblyReloadEvents.beforeAssemblyReload += Unsubscribe;
            s_IsSubscribed = true;
        }

        private static void Unsubscribe()
        {
            if (s_IsSubscribed == false)
                return;

            EditorApplication.hierarchyWindowItemOnGUI -= OnHierarchyGUI;
            EditorApplication.quitting -= Unsubscribe;
            AssemblyReloadEvents.beforeAssemblyReload -= Unsubscribe;

            if (s_CurrentSettings != null)
                s_CurrentSettings.Changed -= OnSettingsChanged;

            s_IsSubscribed = false;
        }

        private static void RefreshCache()
        {
            if (ConfigLoader.Config == null)
            {
                Debug.LogWarning(Constants.HierarchyIcon.MessageNoDataReceived);

                return;
            }

            if (s_CurrentSettings != ConfigLoader.Config)
            {
                if (s_CurrentSettings != null)
                    s_CurrentSettings.Changed -= OnSettingsChanged;

                s_CurrentSettings = ConfigLoader.Config;
                s_CurrentSettings.Changed += OnSettingsChanged;
            }

            s_EnabledTypes = new();
            s_Cache = new();
            HashSet<Type> seen = new();

            if (ConfigLoader.Config.ComponentIconList == null)
                return;

            bool configDirty = false;

            foreach (ComponentIcon component in ConfigLoader.Config.ComponentIconList)
            {
                if (component == null)
                {
                    Debug.LogWarning(Constants.HierarchyIcon.MessageComponentIsNull);

                    continue;
                }

                Type componentType = component.Type;
                Texture2D texture = component.Icon;

                if (componentType == null)
                    continue;

                if (seen.Add(componentType))
                {
                    s_EnabledTypes.Add(componentType);
                    if (texture != null)
                        s_Cache[componentType] = texture;
                }
                else
                {
                    component.Clear();
                    configDirty = true;
                }
            }

            if (configDirty)
                EditorUtility.SetDirty(ConfigLoader.Config);
        }

        private static void OnHierarchyGUI(int instanceID, Rect selectionRect)
        {
            if (Event.current.type != EventType.Repaint)
                return;

            if (ConfigLoader.Config == null || ConfigLoader.Config.Enabled == false)
                return;

#if UNITY_2023_2_OR_NEWER
            GameObject gameObject = EditorUtility.EntityIdToObject(instanceID) as GameObject;
#else
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;
#endif

            if (gameObject == null)
                return;

            Vector2 iconSize = Constants.HierarchyIcon.IconSize;

            foreach (Type enabledType in s_EnabledTypes)
            {
                if (gameObject.GetComponent(enabledType) != null)
                {
                    if (s_Cache.TryGetValue(enabledType, out Texture2D texture) == false || texture == null)
                        continue;

                    Rect iconRect = new(
                        selectionRect.x,
                        selectionRect.y + selectionRect.height - iconSize.y,
                        iconSize.x,
                        iconSize.y);

                    GUI.DrawTexture(iconRect, texture);

                    break;
                }
            }
        }

        private static void OnSettingsChanged()
        {
            RefreshCache();
            EditorApplication.RepaintHierarchyWindow();
        }
    }
}
#endif