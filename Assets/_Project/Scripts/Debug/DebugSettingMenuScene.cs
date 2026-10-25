#if UNITY_EDITOR
using BattleBase.DI;
using BattleBase.SaveService;
using UnityEngine;
using VContainer;

namespace BattleBase.Utils
{
    //todo remove on release
    public class DebugSettingMenuScene : MonoBehaviour, IInjectable
    {
        private static DebugSettingMenuScene Single;

        [SerializeField] private bool _isShowAllUnits = false;

        private IPurchasesSaver _saver;

        public static bool IsShowAllUnits => Single != null && Single._isShowAllUnits;

        [Inject]
        public void Construct(IPurchasesSaver saver) =>
            _saver = saver;

        private void Awake()
        {
            if (Single == null)
                Single = this;
        }

        [ContextMenu(nameof(ResetPurchasingNoAds))]
        private void ResetPurchasingNoAds()
        {
            if (_saver == null)
            {
                Debug.LogWarning("SDKData must been Received");

                return;
            }

            _saver.SetNoAdsState(false);
        }
    }
}
#endif