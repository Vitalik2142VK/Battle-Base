using System;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Actors.DamageSystem;
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
        private readonly IPreviewInstanceFactory _previewInstanceFactory;

        public PreviewCreator(
            IScreenshoter screenshoter, 
            TeamColorModel teamColorModel, 
            IModelCenterCalculator centerCalculator,
            IPreviewInstanceFactory previewInstanceFactory)
        {
            _screenshoter = screenshoter ?? throw new ArgumentNullException(nameof(screenshoter));
            _teamColorModel = teamColorModel ?? throw new ArgumentNullException(nameof(teamColorModel));
            _centerCalculator = centerCalculator ?? throw new ArgumentNullException(nameof(centerCalculator));
            _previewInstanceFactory = previewInstanceFactory ?? throw new ArgumentNullException(nameof(previewInstanceFactory));
        }

        public Sprite Create(
            GameObject prefab,
            float previewScreenScale,
            Vector2Int textureSize,
            Vector3 cameraOffset,
            Vector3 modelRotation,
            DepthBits depth,
            AntiAliasingLevel antiAliasing)
        {
            GameObject actor = _previewInstanceFactory.Create(prefab);
            ICaptureCamera screenshotCamera = _screenshoter.ScreenshotCamera;

            try
            {
                Transform actorTransform = actor.transform;
                actorTransform.localScale *= previewScreenScale;
                actorTransform.eulerAngles = modelRotation;
                Vector3 anchorPosition = actorTransform.position;

                Vector3 targetOffset = CalculateTargetOffset(actor);
                actorTransform.position -= targetOffset;

                screenshotCamera.Show();
                actor.SetLayerRecursively(screenshotCamera.Layer);
                screenshotCamera.SetCameraPosition(anchorPosition + cameraOffset);
                screenshotCamera.CameraLookAt(_centerCalculator.GetCenter(actor));

                if (actor.TryGetComponent(out MaterialColorChanger colorChanger))
                    colorChanger.Change(_teamColorModel.PlayerColor);

                Vector2 centerPivot = new(0.5f, 0.5f);
                Rect rect = new(0, 0, textureSize.x, textureSize.y);

                Texture2D texture = _screenshoter.CaptureObject(
                    textureSize,
                    depth,
                    antiAliasing);

                return Sprite.Create(texture, rect, centerPivot);
            }
            finally
            {
                if (actor != null)
                {
                    actor.SetActive(false);
                    UnityEngine.Object.DestroyImmediate(actor);
                }

                screenshotCamera.Hide();
            }
        }

        private static Vector3 CalculateTargetOffset(GameObject actor)
        {
            Target target = actor.GetComponentInChildren<Target>(true);

            if (target == null)
                return Vector3.zero;

            return target.Position - actor.transform.position;
        }
    }
}