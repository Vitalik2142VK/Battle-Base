using System;
using System.Collections.Generic;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    public class ProductionPanel : PopUp
    {
        [SerializeField] private DynamicScrollRect _dynamicScrollRect;
        [SerializeField] private Transform _content;

        public void ClearContext()
        {
            foreach (Transform child in _content)
            {
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
        }

        public void AddRangeItem(IEnumerable<IProductionItem> items)
        {
            foreach (var item in items)
                item.SetParent(_content);

            _dynamicScrollRect.Refresh();
        }
    }
}