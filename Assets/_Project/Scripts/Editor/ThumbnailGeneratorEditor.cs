using BattleBase.Utils.Screenshot;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ThumbnailGenerator))]
public class ThumbnailGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        ThumbnailGenerator generator =
            (ThumbnailGenerator)target;

        if (GUILayout.Button("Generate All"))
        {
            generator.GenerateAll();
        }
    }
}