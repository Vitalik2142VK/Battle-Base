using System;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Map;
using BattleBase.ScreenshotSystem;
using BattleBase.Utils.Extensions;
using UnityEngine;

namespace BattleBase.PreviewCreatingSystem
{
    public class PreviewCreator : IPreviewCreator
    {
        private readonly IScreenshoter _screenshoter;
        private readonly TeamColorModel _teamColorModel;
        private readonly IModelCenterCalculator _centerCalculator;

        public PreviewCreator(IScreenshoter screenshoter, TeamColorModel teamColorModel, IModelCenterCalculator centerCalculator)
        {
            _screenshoter = screenshoter ?? throw new ArgumentNullException(nameof(screenshoter));
            _teamColorModel = teamColorModel ?? throw new ArgumentNullException(nameof(teamColorModel));
            _centerCalculator = centerCalculator ?? throw new ArgumentNullException(nameof(centerCalculator));
        }

        public Sprite Create(
            GameObject actorCleanPrefab,
            float previewScreenScale,
            Vector2Int textureSize,
            Vector3 cameraOffset,
            Vector3 modelRotation,
            DepthBits depth,
            AntiAliasingLevel antiAliasing)
        {
            GameObject actor = UnityEngine.Object.Instantiate(actorCleanPrefab);
            Transform actorTransform = actor.transform;
            actorTransform.localScale *= previewScreenScale;
            actorTransform.eulerAngles = modelRotation;

            ICaptureCamera screenshotCamera = _screenshoter.ScreenshotCamera;
            screenshotCamera.Show();
            actor.SetLayerRecursively(screenshotCamera.Layer);
            screenshotCamera.SetCameraPosition(actor.transform.position + cameraOffset);
            screenshotCamera.CameraLookAt(_centerCalculator.GetCenter(actor));

            if (actor.TryGetComponent(out MaterialColorChanger colorChanger))
                colorChanger.Change(_teamColorModel.PlayerColor);

            Vector2 centerPivot = new(0.5f, 0.5f);
            Rect rect = new(0, 0, textureSize.x, textureSize.y);

            Texture2D texture = _screenshoter.CaptureObject(
                textureSize,
                depth,
                antiAliasing);

            Sprite preview = Sprite.Create(
                texture,
                rect,
                centerPivot);

            actor.SetActive(false);
            UnityEngine.Object.Destroy(actor);
            screenshotCamera.Hide();

            return preview;
        }
    }
}