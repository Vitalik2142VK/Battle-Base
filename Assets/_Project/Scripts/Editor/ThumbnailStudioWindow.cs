using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public partial class ThumbnailStudioWindow : EditorWindow
{
    [SerializeField] private TextureFormat _textureFormat = TextureFormat.RGBA32;
    [SerializeField] private RenderTexture _renderTexture;

    private readonly List<Entry> _entries = new();

    private Camera _camera;

    private int _width = 512;
    private int _height = 512;

    private string _outputFolder = "Assets/GeneratedThumbnails";

    [MenuItem("Tools/Thumbnail Studio")]
    public static void Open()
    {
        GetWindow<ThumbnailStudioWindow>("Thumbnail Studio");
    }

    private void OnGUI()
    {
        GUILayout.Label("Camera", EditorStyles.boldLabel);
        _camera = (Camera)EditorGUILayout.ObjectField(_camera, typeof(Camera), true);

        GUILayout.Label("RenderTexture", EditorStyles.boldLabel);
        _renderTexture = (RenderTexture)EditorGUILayout.ObjectField(_renderTexture, typeof(RenderTexture), true);

        GUILayout.Space(5);

        _textureFormat = (TextureFormat)EditorGUILayout.EnumPopup("Format", _textureFormat);
        _width = EditorGUILayout.IntField("Width", _width);
        _height = EditorGUILayout.IntField("Height", _height);
        _outputFolder = EditorGUILayout.TextField("Output Folder", _outputFolder);

        if (GUILayout.Button("Create RenderTexture"))
        {
            CreateRT();
        }

        GUILayout.Space(10);
        GUILayout.Label("Entries", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Entry"))
        {
            _entries.Add(new Entry());
        }

        for (int i = 0; i < _entries.Count; i++)
        {
            var e = _entries[i];

            EditorGUILayout.BeginVertical("box");

            e.FileName = EditorGUILayout.TextField("File Name", e.FileName);
            e.Prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", e.Prefab, typeof(GameObject), false);
            e.CameraPoint = (Transform)EditorGUILayout.ObjectField("Camera Point", e.CameraPoint, typeof(Transform), true);

            if (GUILayout.Button("Remove"))
            {
                _entries.RemoveAt(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        GUILayout.Space(10);

        if (GUILayout.Button("GENERATE ALL", GUILayout.Height(40)))
        {
            GenerateAll();
        }
    }

    private void CreateRT()
    {
        _renderTexture = new RenderTexture(_width, _height, 24, RenderTextureFormat.ARGB32);
        _renderTexture.Create();

        Debug.Log("RenderTexture created");
    }

    private void GenerateAll()
    {
        if (_camera == null)
        {
            Debug.LogError("No camera assigned");

            return;
        }

        if (_renderTexture == null)
            CreateRT();

        if (!Directory.Exists(_outputFolder))
            Directory.CreateDirectory(_outputFolder);

        _camera.targetTexture = _renderTexture;

        foreach (var e in _entries)
        {
            if (e.Prefab == null || e.CameraPoint == null)
                continue;

            Generate(e);
        }

        _camera.targetTexture = null;

        AssetDatabase.Refresh();

        Debug.Log("DONE");
    }

    private void Generate(Entry e)
    {
        // spawn preview object
        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(e.Prefab);

        instance.transform.position = Vector3.zero;
        instance.transform.rotation = Quaternion.identity;

        // move camera
        _camera.transform.SetPositionAndRotation(
            e.CameraPoint.position,
            e.CameraPoint.rotation
        );

        _camera.Render();

        RenderTexture.active = _renderTexture;

        Texture2D tex = new(_width, _height, _textureFormat, false);

        tex.ReadPixels(new Rect(0, 0, _width, _height), 0, 0);
        tex.Apply();

        byte[] png = tex.EncodeToPNG();

        string path = Path.Combine(_outputFolder, e.FileName + ".png");

        File.WriteAllBytes(path, png);

        RenderTexture.active = null;

        Object.DestroyImmediate(instance);
        Object.DestroyImmediate(tex);

        Debug.Log($"Saved {path}");
    }
}