#if UNITY_EDITOR
using UnityEngine;

namespace BattleBase.Utils
{
    //todo remove on release
    public class DebugSettingMenuScene : MonoBehaviour
    {
        private static DebugSettingMenuScene Single;

        [SerializeField] private bool _isShowAllUnits = false;

        public static bool IsShowAllUnits => Single != null && Single._isShowAllUnits;

        private void Awake()
        {
            if (Single == null)
                Single = this;
        }
    }
}
#endif