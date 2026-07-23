using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.Utils.Extensions
{
    public static class TransformExtensions
    {
        public static string GetPath(this Transform transform)
        {
            if (transform == null)
                return string.Empty;

            List<string> parts = new();
            Transform current = transform;

            while (current != null)
            {
                parts.Add(current.name);
                current = current.parent;
            }

            parts.Reverse();

            return "/" + string.Join("/", parts);
        }

        public static void ClearChilds(this Transform transform)
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Transform child = transform.GetChild(i);
                Object.Destroy(child.gameObject);
            }
        }
    }
}