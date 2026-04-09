using BattleBase.Static;
using DG.Tweening;
using UnityEngine;

namespace BattleBase.UI.PopUp
{
    [RequireComponent(typeof(CanvasGroup))]
    public class PopUpAlphaAnimator : PopUpAnimatorBase
    {
        [SerializeField] private AlphaAnimationConfig _showConfig;
        [SerializeField] private AlphaAnimationConfig _hideConfig;

        private CanvasGroup _canvasGroup;

        public override void Init() =>
            _canvasGroup = GetComponent<CanvasGroup>();

        public override bool TryPlayShow(out Tweener tweener)
        {
            bool isSuccess = _showConfig != null;
            _canvasGroup.alpha = isSuccess ? _showConfig.StartAlpha : 0f;
            tweener = isSuccess ? _canvasGroup.PlayAlpha(_showConfig) : null;

            return isSuccess;
        }

        public override bool TryPlayHide(out Tweener tweener)
        {
            bool isSuccess = _hideConfig != null;
            tweener = isSuccess ? _canvasGroup.PlayAlpha(_hideConfig) : null;

            return isSuccess;
        }
    }
}