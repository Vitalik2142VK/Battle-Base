using BattleBase.ScreenshotSystem;
using BattleBase.Utils.Constants;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    [CreateAssetMenu(
        fileName = nameof(PreviewCreateConfig),
        menuName = AssetMenuPaths.ScriptableObjects + nameof(PreviewCreateConfig))]
    public class PreviewCreateConfig : ScriptableObject
    {
        [SerializeField] private Vector2Int _smallTextureSize = new(256, 256);
        [SerializeField] private Vector2Int _bigTextureSize = new(512, 512);
        [SerializeField] private Vector3 _cameraOffset;
        [SerializeField] private Vector3 _modelRotation;
        [SerializeField] private DepthBits _depthBits = DepthBits.TwentyFour;
        [SerializeField] private AntiAliasingLevel _antiAliasingLevel = AntiAliasingLevel.Disabled;

        public Vector2Int SmallTextureSize => _smallTextureSize;

        public Vector2Int BigTextureSize => _bigTextureSize;

        public DepthBits DepthBits => _depthBits;

        public AntiAliasingLevel AntiAliasingLevel => _antiAliasingLevel;

        public Vector3 CameraOffset => _cameraOffset;

        public Vector3 ModelRotation => _modelRotation;
    }
}