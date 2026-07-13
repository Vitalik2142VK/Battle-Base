using UnityEngine;

namespace BattleBase.Utils
{
    //todo remove on release
    public class DebugSetting : MonoBehaviour
    {
        private static DebugSetting Single;

        [SerializeField] private bool _isAiDisbale = false;

        public static bool IsAiDisbale => Single._isAiDisbale;

        private void Awake()
        {
            if (Single == null)
                Single = this;
        }
    }
}