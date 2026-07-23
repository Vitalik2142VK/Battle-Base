using System.Collections.Generic;

#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using UnityEngine;

namespace BattleBase.Utils.Screenshot
{
    public class ThumbnailGenerator : MonoBehaviour
    {
#if UNITY_EDITOR
        [Header("Camera")]
        [SerializeField] private Camera _captureCamera;

        [Header("Render")]
        [SerializeField] private RenderTexture _renderTexture;

        [SerializeField] private TextureFormat _textureFormat = TextureFormat.ARGB32;
        [SerializeField][Min(32)] private int _width = 512;
        [SerializeField][Min(32)] private int _height = 512;

        [Header("Output")]
        [SerializeField] private string _outputFolder = "Assets/GeneratedIcons";
        [SerializeField] private GameObject[] _captures;

        private Queue<GameObject> _queue;

        public void GenerateAll()
        {
            if (_captureCamera == null)
            {
                Debug.LogError("Camera not assigned.");

                return;
            }

            if (_renderTexture == null)
                _renderTexture = new(_width, _height, 24, RenderTextureFormat.ARGB32);

            if (Directory.Exists(_outputFolder) == false)
                Directory.CreateDirectory(_outputFolder);

            foreach (var capture in _captures)
                capture.SetActive(false);

            _queue = new Queue<GameObject>(_captures);

            EditorApplication.update += OnProcessNext;
        }

        private void OnProcessNext()
        {
            _captureCamera.targetTexture = _renderTexture;

            GameObject gameObject = _queue.Dequeue();

            if (gameObject == null)
                return;

            Capture(gameObject);

            _captureCamera.targetTexture = null;

            AssetDatabase.Refresh();

            if (_queue.Count == 0)
            {
                Debug.Log("Generation completed.");

                EditorApplication.update -= OnProcessNext;
            }
        }

        private void Capture(GameObject capture)
        {
            capture.SetActive(true);

            _captureCamera.Render();

            RenderTexture.active = _renderTexture;

            Texture2D texture = new Texture2D(_width, _height, _textureFormat, false);
            texture.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
            texture.Apply();

            byte[] png = texture.EncodeToPNG();
            string path = Path.Combine(_outputFolder, $"{capture.name}.png");

            File.WriteAllBytes(path, png);

            DestroyImmediate(texture);

            RenderTexture.active = null;

            capture.SetActive(false);

            Debug.Log($"Saved: {path}");
        }
#endif
    }
}