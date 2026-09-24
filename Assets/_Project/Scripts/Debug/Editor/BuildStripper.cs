#if UNITY_EDITOR
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;
using BattleBase.Utils;

public class BuildStripper : IProcessSceneWithReport
{
    public int callbackOrder => 0;

    public void OnProcessScene(Scene scene, BuildReport report)
    {
        if (report == null)
            return;

        foreach (var root in scene.GetRootGameObjects())
        {
            var comps = root.GetComponentsInChildren<DebugSetting>(true);

            foreach (var c in comps)
                Object.DestroyImmediate(c, true);
        }
    }
}
#endif