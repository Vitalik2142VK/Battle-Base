using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CreateAssetMenu(
        fileName = nameof(CameraConfig),
        menuName = Constants.ConfigsAssetMenuPath + nameof(CameraConfig))]
    public class CameraConfig : ScriptableObject, ICameraConfig
    {
        [SerializeField][Min(0f)] private float _restoreSpeed = 3;
        [SerializeField][Min(0f)] private float _zoomSpeed = 1;
        [SerializeField][Min(0f)] private float _minimumOrtoSize = 0.3f;
        [SerializeField][Min(0f)] private float _maximumOrtoSize = 1.2f;

        public float RestoreSpeed => _restoreSpeed;

        public float ZoomSpeed => _zoomSpeed;

        public float MinimumOrtoSize => _minimumOrtoSize;

        public float MaximumOrtoSize => _maximumOrtoSize;
    }
}