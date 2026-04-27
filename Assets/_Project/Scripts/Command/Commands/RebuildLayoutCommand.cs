using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.Commands
{
    public class RebuildLayoutCommand : CommandBase
    {
        [SerializeField] private RectTransform _rectTransform;

        public override void Execute() =>
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
    }
}