using UnityEngine;

namespace FiXiK.CustomLogger.Editor
{
    public static class LoggerEditorConstants
    {
        public const string SettingsMenuPath = "Tools/XLogger/Settings";
        public const string EditorPrefsKey = "LoggerLanguage";

        public static readonly Vector2 AboutDialogSize = new(400, 170);
        public static readonly Vector2 AboutButtonSize = new(50, 25);
        public static readonly Vector2 AboutSecondaryButtonSize = new(110, 25);
        public const int AboutSpace = 10;

        public static readonly Vector2 WelcomeDialogSize = new(400, 170);
        public static readonly Vector2 WelcomeButtonSize = new(120, 25);
        public const int WelcomeSpace = 10;
    }
}