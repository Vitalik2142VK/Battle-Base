using UnityEngine;

public partial class ThumbnailStudioWindow
{
    [System.Serializable]
    private class Entry
    {
        public string FileName;
        public GameObject Prefab;
        public Transform CameraPoint;
    }
}