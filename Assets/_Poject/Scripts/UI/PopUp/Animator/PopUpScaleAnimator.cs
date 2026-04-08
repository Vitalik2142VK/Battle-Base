using BattleBase.Static;
using DG.Tweening;
using UnityEngine;

namespace BattleBase.UI.PopUp
{
    public class PopUpScaleAnimator : MonoBehaviour
    {
        [SerializeField] private ScaleAnimationConfig _showConfig;
        [SerializeField] private ScaleAnimationConfig _hideConfig;

        private Transform _transform;

        public void Init() =>
            _transform = transform;

        public bool TryPlayShow(out Tweener tweener)
        {
            bool isSuccess = _showConfig != null;
            _transform.localScale = isSuccess ? _showConfig.StartScale : Vector3.zero;
            tweener = isSuccess ? _transform.PlayScale(_showConfig) : null;

            return isSuccess;
        }

        public bool TryPlayHide(out Tweener tweener)
        {
            bool isSuccess = _hideConfig != null;
            tweener = isSuccess ? _transform.PlayScale(_hideConfig) : null;

            return isSuccess;
        }
    }
}