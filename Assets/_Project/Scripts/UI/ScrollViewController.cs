using System;
using UnityEngine;

namespace BattleBase.UI
{
    public class ScrollViewController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void UpdateVisable(int currentNumColumns, int maxVisibleColumns)
        {
            if (maxVisibleColumns <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxVisibleColumns));

            if (currentNumColumns < 0)
                throw new ArgumentOutOfRangeException(nameof(currentNumColumns));

            if (currentNumColumns > maxVisibleColumns)
                _canvasGroup.alpha = 1f;
            else
                _canvasGroup.alpha = 0;
        }
    }
}