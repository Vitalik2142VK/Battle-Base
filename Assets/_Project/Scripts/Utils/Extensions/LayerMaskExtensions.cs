using UnityEngine;

namespace BattleBase.Utils.Extensions
{
    public static class LayerMaskExtensions
    {
        public static int GetFirstLayerIndex(this LayerMask mask)
        {
            int value = mask.value;

            for (int i = 0; i < 32; i++)
            {
                if ((value & (1 << i)) != 0)
                    return i;
            }

            return 0;
        }

        public static void SetLayerRecursively(this GameObject obj, LayerMask mask)
        {
            int layerIndex = GetFirstLayerIndex(mask);
            obj.SetLayerRecursively(layerIndex);
        }

        private static void SetLayerRecursively(this GameObject obj, int layer)
        {
            obj.layer = layer;

            foreach (Transform child in obj.transform)
                child.gameObject.SetLayerRecursively(layer);
        }
    }
}