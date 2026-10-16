using UnityEngine;

namespace BattleBase.UI.PopUps
{
    public class ProductionPanel : PopUp
    {
        [SerializeField] private DynamicScrollRect _dynamicScrollRect;
        [SerializeField] private Transform _content;

        public void ClearContext()
        {
            foreach(Transform child in _content)
                Destroy(child.gameObject);
        }

        public void AddItem(IProductionItem item)
        {
            item.SetParent(_content);

            _dynamicScrollRect.Refresh();
        }
    }
}