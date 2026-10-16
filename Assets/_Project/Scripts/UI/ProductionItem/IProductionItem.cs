using UnityEngine;

namespace BattleBase.UI
{
    public interface IProductionItem
    {
        public void SetParent(Transform parent);

        public void ResetParent();
    }
}