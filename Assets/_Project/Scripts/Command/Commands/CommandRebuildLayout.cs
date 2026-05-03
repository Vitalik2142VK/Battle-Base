using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.Commands
{
    public class CommandRebuildLayout : CommandBase
    {
        [SerializeField] private List<RectTransform> _rectTransformList;

        public override void Execute()
        {
            foreach (RectTransform rectTransform in _rectTransformList)
            {
                if (rectTransform != null)
                    LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            }
        }

        public void Add(RectTransform rectTransform)
        {
            if(rectTransform == null)
                throw new ArgumentNullException(nameof(rectTransform));

            if (_rectTransformList.Contains(rectTransform) == false)
                _rectTransformList.Add(rectTransform);
        }

        public void Remove(RectTransform rectTransform)
        {
            if (rectTransform == null)
                throw new ArgumentNullException(nameof(rectTransform));

            if (_rectTransformList.Contains(rectTransform))
                _rectTransformList.Remove(rectTransform);
        }
    }
}