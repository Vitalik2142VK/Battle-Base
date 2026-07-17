using System;
using UnityEngine;

namespace BattleBase.PreviewCreatingSystem
{
    public class ModelCenterCalculator : IModelCenterCalculator
    {
        public Vector3 GetCenter(GameObject model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));

            ScreenshotCenter screenshotCenterTarget = model.GetComponentInChildren<ScreenshotCenter>();

            if (screenshotCenterTarget != null)
                return screenshotCenterTarget.transform.position;

            Renderer[] renderers = model.GetComponentsInChildren<Renderer>();

            if (renderers.Length == 0)
                return model.transform.position;

            Bounds bounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; i++)
                bounds.Encapsulate(renderers[i].bounds);

            return bounds.center;
        }
    }
}