using UnityEngine;
using UnityEngine.UI;

namespace BattleBase.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonClickHandler : MonoBehaviour
    {
        private Button _button;

        private void Awake() =>
            _button = GetComponent<Button>();

        protected virtual void OnEnable() =>
            _button.onClick.AddListener(OnClick);

        protected virtual void OnDisable() =>
            _button.onClick.RemoveListener(OnClick);

        private void OnDestroy()
        {
            if (_button != null)
                _button.onClick.RemoveListener(OnClick);
        }

        protected abstract void OnClick();
    }
}