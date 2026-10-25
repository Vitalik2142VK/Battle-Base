using System;
using BattleBase.PurchaseService;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace BattleBase.UI.PopUps
{
    [RequireComponent(typeof(Button))]
    [RequireComponent(typeof(PurchaseInfoView))]
    public class PurchaseButton : MonoBehaviour
    {
        private PurchaseInfoView _purchaseInfoView;
        private Button _button;
        private IPurchaseService _purchaseService;

        [Inject]
        public void Construct(IPurchaseService purchaseService) =>
            _purchaseService = purchaseService ?? throw new ArgumentNullException(nameof(purchaseService));

        private void Awake()
        {
            _purchaseInfoView = GetComponent<PurchaseInfoView>();
            _button = GetComponent<Button>();
        }

        private void OnEnable() =>
            _button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
            if (_purchaseInfoView.Config == null)
            {
                Debug.LogError($"{nameof(PurchaseButton)}: config is not assigned on {name}", this);

                return;
            }

            _purchaseService.Buy(_purchaseInfoView.Config);
        }
    }
}