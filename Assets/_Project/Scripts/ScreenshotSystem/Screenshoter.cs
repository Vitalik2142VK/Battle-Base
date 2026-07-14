using System;
using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public class Screenshoter : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _target;

        private RenderTexture _renderTexture;

        public Texture2D CaptureObject(GameObject model, int width = 256, int height = 256)
        {
            gameObject.SetActive(true);

            if (model == null)
                throw new ArgumentNullException(nameof(model));

            model.transform.SetPositionAndRotation(_target.position, _target.rotation);

            Vector3 modelCenter = GetModelCenter(model);
            _camera.transform.LookAt(modelCenter);

            _camera.targetTexture = GetOrCreateRenderTexture(width, height);
            _camera.Render();

            RenderTexture.active = _camera.targetTexture;
            Texture2D screenshot = new(width, height, TextureFormat.RGBA32, false);
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply();

            RenderTexture.active = null;
            _camera.targetTexture = null;

            gameObject.SetActive(false);

            return screenshot;
        }

        private RenderTexture GetOrCreateRenderTexture(int width, int height)
        {
            if (_renderTexture == null || _renderTexture.width != width || _renderTexture.height != height)
            {
                if (_renderTexture != null)
                    _renderTexture.Release();

                int depth = 24;

                _renderTexture = new(width, height, depth, RenderTextureFormat.ARGB32)
                {
                    antiAliasing = 1
                };

                _renderTexture.Create();
            }

            return _renderTexture;
        }

        private Vector3 GetModelCenter(GameObject model)
        {
            ScreenshotCenterTarget screenshotCenterTarget = model.GetComponentInChildren<ScreenshotCenterTarget>();

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

        private void OnDestroy()
        {
            if (_renderTexture != null)
            {
                _renderTexture.Release();
                Destroy(_renderTexture);
            }
        }
    }
}