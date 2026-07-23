using System;
using UnityEngine;

namespace BattleBase.ScreenshotSystem
{
    public class RenderTextureFactory : IDisposable, IRenderTextureFactory
    {
        private RenderTexture _renderTexture;

        public void Dispose()
        {
            if (_renderTexture != null)
            {
                _renderTexture.Release();
                UnityEngine.Object.Destroy(_renderTexture);
            }
        }

        public RenderTexture GetOrCreate(Vector2Int textureSize, DepthBits depth, AntiAliasingLevel antiAliasing)
        {
            bool needsRecreate = _renderTexture == null
                || _renderTexture.width != textureSize.x
                || _renderTexture.height != textureSize.y
                || _renderTexture.depth != (int)depth
                || _renderTexture.antiAliasing != (int)antiAliasing;

            if (needsRecreate)
            {
                if (_renderTexture != null)
                    _renderTexture.Release();

                _renderTexture = new(textureSize.x, textureSize.y, (int)depth, RenderTextureFormat.ARGB32)
                {
                    antiAliasing = (int)antiAliasing,
                };

                _renderTexture.Create();
            }

            return _renderTexture;
        }
    }
}