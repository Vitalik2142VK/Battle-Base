using TMPro;
using UnityEngine;

namespace BattleBase.UI.PopUps
{
    [RequireComponent(typeof(PurchaseInfoView))]
    public class CreditsToRewardView : MonoBehaviour
    {
        private const string CoinIcon = "<sprite name=\"coins\">";

        [SerializeField] private TMP_Text _creditsToReward;

        private void Awake()
        {
            PurchaseInfoView purchaseInfoView = GetComponent<PurchaseInfoView>();

            if (purchaseInfoView.Config == null)
            {
                Debug.LogError($"{nameof(CreditsToRewardView)}: config is not assigned", this);

                return;
            }

            _creditsToReward.text = $"+{CoinIcon}{purchaseInfoView.Config.CoinsReward}";
        }
    }
}