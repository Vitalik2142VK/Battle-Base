using BattleBase.Utils;
using UnityEngine;

namespace BattleBase.Gameplay.Map
{
    [CreateAssetMenu(
        fileName = nameof(CameraConfig),
        menuName = Constants.ConfigsAssetMenuName + "/" + nameof(CameraConfig))]
    public class CameraConfig : ScriptableObject, ICameraConfig
    {
        [SerializeField][Min(0f)] private float _restoreSpeed = 3;
        [SerializeField][Min(0f)] private float _zoomSpeed = 1;
        [SerializeField][Min(0f)] private float _minimumZoom = 0.3f;
        [SerializeField][Min(0f)] private float _maximumZoom = 1.2f;

        public float RestoreSpeed => _restoreSpeed;

        public float ZoomSpeed => _zoomSpeed;

        public float MinimumZoom => _minimumZoom;

        public float MaximumZoom => _maximumZoom;
    }
}