using UnityEditor;
using UnityEngine;

namespace FiXiK.CustomLogger.Editor
{
    public class LoggerSettingsWindow : EditorWindow
    {
        private SerializedObject _serializedSettings;
        private LoggerSettings _settings;
        private Vector2 _scrollPos;
        private bool _showTagSettings = false;
        private bool _showTextSettings = false;
        private bool _showColorSettings = false;
        private string[] _languageOptions;
        private int _selectedLanguageIndex;
        private ILanguage[] _allLanguages;

        private void OnEnable()
        {
            LanguageManager.LoadFromPrefs();
            _allLanguages = LanguageManager.GetAllLanguages();
            _selectedLanguageIndex = System.Array.IndexOf(_allLanguages, LanguageManager.Current);

            if (_selectedLanguageIndex < 0)
                _selectedLanguageIndex = 0;

            _languageOptions = new string[_allLanguages.Length];

            for (int i = 0; i < _allLanguages.Length; i++)
                _languageOptions[i] = LanguageManager.GetLanguageName(_allLanguages[i]);

            LanguageManager.LanguageChanged += Repaint;
            LoadSettings();
        }

        private void OnDisable()
        {
            LanguageManager.LanguageChanged -= Repaint;
            LanguageManager.SaveToPrefs();
        }

        private void OnGUI()
        {
            _serializedSettings.Update();

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);

            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"{LanguageManager.Current.Language}:", GUILayout.Width(120));
            int newIndex = EditorGUILayout.Popup(_selectedLanguageIndex, _languageOptions, GUILayout.Width(150));

            if (newIndex != _selectedLanguageIndex)
            {
                _selectedLanguageIndex = newIndex;
                LanguageManager.Current = _allLanguages[newIndex];
                titleContent = new GUIContent(LanguageManager.Current.SettingsWindowTitle);
                Repaint();
            }

            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            if (_serializedSettings == null)
            {
                LoadSettings();

                if (_serializedSettings == null)
                {
                    EditorGUILayout.HelpBox(LanguageManager.Current.FailedToLoadSettings, MessageType.Error);

                    return;
                }
            }

            _showTagSettings = EditorGUILayout.Foldout(_showTagSettings, LanguageManager.Current.TagSettingsHeader, true);

            if (_showTagSettings)
            {
                SerializedProperty showTagProp = _serializedSettings.FindProperty(LoggerConstants.ShowTagField);
                EditorGUILayout.PropertyField(showTagProp, new GUIContent(LanguageManager.Current.ShowTagLabel));

                SerializedProperty defaultTagProp = _serializedSettings.FindProperty(LoggerConstants.DefaultTagField);
                EditorGUILayout.PropertyField(defaultTagProp, new GUIContent(LanguageManager.Current.DefaultTagLabel));

                SerializedProperty tagsProp = _serializedSettings.FindProperty(LoggerConstants.TagsField);
                EditorGUILayout.PropertyField(tagsProp, new GUIContent(LanguageManager.Current.TagsLabel), true);
            }

            EditorGUILayout.Space();

            _showTextSettings = EditorGUILayout.Foldout(_showTextSettings, LanguageManager.Current.MessageSettingsHeader, true);

            if (_showTextSettings)
            {
                SerializedProperty logParamsProp = _serializedSettings.FindProperty(LoggerConstants.LogTextParamsField);
                DrawMessageParams(logParamsProp, LanguageManager.Current.LogLabel);

                SerializedProperty warningParamsProp = _serializedSettings.FindProperty(LoggerConstants.WarningTextParamsField);
                DrawMessageParams(warningParamsProp, LanguageManager.Current.WarningLabel);

                SerializedProperty errorParamsProp = _serializedSettings.FindProperty(LoggerConstants.ErrorTextParamsField);
                DrawMessageParams(errorParamsProp, LanguageManager.Current.ErrorLabel);
            }

            EditorGUILayout.Space();
            _showColorSettings = EditorGUILayout.Foldout(_showColorSettings, LanguageManager.Current.ColorSettingsHeader, true);

            if (_showColorSettings)
            {
                SerializedProperty colorsProp = _serializedSettings.FindProperty("_colors");
                EditorGUILayout.PropertyField(colorsProp, new GUIContent(LanguageManager.Current.ColorsLabel), true);
            }

            EditorGUILayout.Space();

            if (GUILayout.Button(LanguageManager.Current.TestButtonLabel, GUILayout.Height(30)))
                LoggerTest.TestLogs();

            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(LanguageManager.Current.ResetButtonLabel))
                ResetSettings();

            if (GUILayout.Button(LanguageManager.Current.SaveButtonLabel))
                SaveSettings();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            EditorGUILayout.EndScrollView();

            _serializedSettings.ApplyModifiedProperties();

            DrawVersionLabel();
        }

        [MenuItem(LoggerEditorConstants.SettingsMenuPath)]
        public static void ShowWindow()
        {
            LoggerSettingsWindow window = GetWindow<LoggerSettingsWindow>(LanguageManager.Current.SettingsWindowTitle);
            window.minSize = new Vector2(400, 500);
            window.Show();
        }

        private void DrawMessageParams(SerializedProperty prop, string label)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
            SerializedProperty colorProp = prop.FindPropertyRelative(LoggerConstants.TextColorField);
            SerializedProperty styleProp = prop.FindPropertyRelative(LoggerConstants.FontStyleField);
            EditorGUILayout.PropertyField(colorProp, new GUIContent(LanguageManager.Current.ColorLabel));
            EditorGUILayout.PropertyField(styleProp, new GUIContent(LanguageManager.Current.FontStyleLabel));
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(5);
        }

        private void DrawVersionLabel()
        {
            float width = 20;
            float height = 20;
            float indentX = position.width - width;
            float indentY = 0;

            Rect headerRect = new(indentX, indentY, width, height);
            GUILayout.BeginArea(headerRect);
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button(
                EditorGUIUtility.IconContent(LoggerConstants.MenuIconName),
                EditorStyles.toolbarButton,
                GUILayout.Width(width), GUILayout.Height(height)))
            {
                ShowHeaderMenu();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        private void ShowHeaderMenu()
        {
            GenericMenu menu = new();
            ILanguage lang = LanguageManager.Current;
            menu.AddItem(new GUIContent(lang.MenuAboutItem), false, ShowAboutDialog);
            menu.AddItem(new GUIContent(lang.MenuWelcomeItem), false, ShowWelcomeDialog);
            menu.ShowAsContext();
        }

        public static void ShowAboutDialog()
        {
            ILanguage lang = LanguageManager.Current;

            CustomDialog.ShowDialog(
                lang.AboutTitle,
                lang.AboutMessage,
                lang.AboutButtonOk,
                lang.AboutButtonTelegram,
                OpenTelegramLink,
                LoggerEditorConstants.AboutDialogSize,
                LoggerEditorConstants.AboutButtonSize,
                LoggerEditorConstants.AboutSecondaryButtonSize,
                LoggerEditorConstants.AboutSpace);
        }

        public static void ShowWelcomeDialog()
        {
            ILanguage lang = LanguageManager.Current;

            CustomDialog.ShowDialog(
                lang.WelcomeTitle,
                lang.WelcomeMessage,
                lang.WelcomeButtonOk,
                LoggerEditorConstants.WelcomeDialogSize,
                LoggerEditorConstants.WelcomeButtonSize,
                LoggerEditorConstants.WelcomeSpace);
        }

        private static void OpenTelegramLink() =>
            Application.OpenURL(LoggerConstants.TelegramUrl);

        private void LoadSettings()
        {
            _settings = SettingsProvider.Settings;

            if (_settings != null)
                _serializedSettings = new SerializedObject(_settings);
        }

        private void SaveSettings()
        {
            if (_settings == null)
                return;

            _serializedSettings.ApplyModifiedProperties();
            EditorUtility.SetDirty(_settings);
            AssetDatabase.SaveAssets();
            TagTypeGenerator.GenerateTagTypeEnum(_settings.GetAllTagNames());
            LogColorGenerator.GenerateLogColorStruct(_settings.GetAllColors());

            Debug.Log(LanguageManager.Current.SettingsSavedMessage);
        }

        private void ResetSettings()
        {
            if (_settings == null)
                return;

            _settings.ResetToDefault();
            _settings.name = LoggerConstants.ResourcesPath;
            EditorUtility.SetDirty(_settings);
            AssetDatabase.SaveAssets();
            _serializedSettings = new SerializedObject(_settings);
            TagTypeGenerator.GenerateTagTypeEnum(_settings.GetAllTagNames());
            LogColorGenerator.GenerateLogColorStruct(_settings.GetAllColors());

            Debug.Log(LanguageManager.Current.SettingsResetMessage);
        }
    }
}