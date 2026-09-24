using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;

namespace BattleBase.PreviewCreatingSystem
{
    public sealed class PreviewInstanceFactory : IPreviewInstanceFactory
    {
        private static readonly Dictionary<Type, PreviewExcludedAttribute> ExclusionCache = new();

        public GameObject Create(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));

            GameObject root = new(nameof(PreviewInstanceFactory));
            root.SetActive(false);
            GameObject instance = UnityEngine.Object.Instantiate(prefab, root.transform);
            ApplyExclusions(instance);
            DisableNavMeshAgentsIfExist(instance);
            instance.transform.SetParent(null);
            UnityEngine.Object.Destroy(root);
            instance.SetActive(true);

            return instance;
        }

        private static void ApplyExclusions(GameObject instance)
        {
            MonoBehaviour[] components = instance.GetComponentsInChildren<MonoBehaviour>(true);

            foreach (MonoBehaviour component in components)
            {
                if (component == null)
                    continue;

                PreviewExcludedAttribute attribute = GetExclusionAttribute(component);

                if (attribute == null)
                    continue;

                ApplyExclusion(component, attribute.Mode);
            }
        }

        private static void ApplyExclusion(MonoBehaviour component, PreviewExclusionMode mode)
        {
            switch (mode)
            {
                case PreviewExclusionMode.Disable:
                    component.enabled = false;
                    break;

                case PreviewExclusionMode.Remove:
                    UnityEngine.Object.DestroyImmediate(component);
                    break;

                case PreviewExclusionMode.Destroy:
                    UnityEngine.Object.DestroyImmediate(component.gameObject);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        private static void DisableNavMeshAgentsIfExist(GameObject instance)
        {
            NavMeshAgent[] agents = instance.GetComponentsInChildren<NavMeshAgent>(true);

            foreach (NavMeshAgent agent in agents)
            {
                if (agent != null)
                    agent.enabled = false;
            }
        }

        private static PreviewExcludedAttribute GetExclusionAttribute(MonoBehaviour component)
        {
            Type type = component.GetType();

            if (ExclusionCache.TryGetValue(type, out PreviewExcludedAttribute attribute))
                return attribute;

            attribute = type.GetCustomAttribute<PreviewExcludedAttribute>();
            ExclusionCache[type] = attribute;

            return attribute;
        }
    }
}