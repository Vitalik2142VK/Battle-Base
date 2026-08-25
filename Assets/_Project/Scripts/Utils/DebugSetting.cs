using UnityEngine;

namespace BattleBase.Utils
{
    //todo remove on release
    public class DebugSetting : MonoBehaviour
    {
        private static DebugSetting Single;

        [SerializeField] private bool _isShowCountActor = false;
        [SerializeField] private bool _isAiDisbale = false;
        [SerializeField] private bool _isBrainDebugEnable = false;

        public static bool IsBrainDebugEnable => Single._isBrainDebugEnable;

        public static bool IsAiDisbale => Single._isAiDisbale;

        public static bool IsShowCountActor => Single._isShowCountActor;

        private void Awake()
        {
            if (Single == null)
                Single = this;
        }
    }
}