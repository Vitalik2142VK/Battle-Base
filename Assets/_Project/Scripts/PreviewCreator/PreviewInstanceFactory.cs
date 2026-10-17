using System;
using UnityEngine;

namespace BattleBase.PreviewCreatingSystem
{
    public sealed class PreviewInstanceFactory : IPreviewInstanceFactory
    {
        public GameObject Create(GameObject prefab)
        {
            if (prefab == null)
                throw new ArgumentNullException(nameof(prefab));

            GameObject instance = UnityEngine.Object.Instantiate(prefab);
            instance.SetActive(true);

            return instance;
        }
    }
}