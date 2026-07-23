using BattleBase.ScreenshotSystem;
using UnityEngine;

namespace BattleBase.PreviewCreatingSystem
{
    public interface IPreviewCreator
    {
        public Sprite Create(
            GameObject actorCleanPrefab,
            float previewScreenScale,
            Vector2Int textureSize,
            Vector3 cameraOffset,
            Vector3 modelRotation,
            DepthBits depth,
            AntiAliasingLevel antiAliasing);
    }
}