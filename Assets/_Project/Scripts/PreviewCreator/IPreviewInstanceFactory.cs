using UnityEngine;

namespace BattleBase.PreviewCreatingSystem
{
    public interface IPreviewInstanceFactory
    {
        public GameObject Create(GameObject prefab);
    }
}