using System;
using BattleBase.Gameplay.Actors.Colored;
using BattleBase.Gameplay.Map;
using BattleBase.ScreenshotSystem;
using UnityEngine;

namespace BattleBase.ShopSystem
{
    public class PreviewCreator
    {
        private readonly Screenshoter _screenshoter;
        private readonly TeamColorModel _teamColorModel;

        public PreviewCreator(Screenshoter screenshoter, TeamColorModel teamColorModel)
        {
            _screenshoter = screenshoter != null ? screenshoter : throw new ArgumentNullException(nameof(screenshoter));
            _teamColorModel = teamColorModel ?? throw new ArgumentNullException(nameof(teamColorModel));
        }

        public Sprite Create(
            GameObject actorCleanPrefab,
            float previewScreenScale,
            int squareTextureSize = 256)
        {
            GameObject actor = UnityEngine.Object.Instantiate(actorCleanPrefab);
            actor.transform.localScale = actor.transform.localScale * previewScreenScale;

            if (actor.TryGetComponent(out MaterialColorChanger colorChanger))
                colorChanger.Change(_teamColorModel.PlayerColor);

            Vector2 centerPivot = new(0.5f, 0.5f);
            Rect rect = new(0, 0, squareTextureSize, squareTextureSize);
            Texture2D texture = _screenshoter.CaptureObject(actor, squareTextureSize, squareTextureSize);

            Sprite preview = Sprite.Create(
                texture,
                rect,
                centerPivot);

            actor.SetActive(false);
            UnityEngine.Object.Destroy(actor);

            return preview;
        }
    }
}