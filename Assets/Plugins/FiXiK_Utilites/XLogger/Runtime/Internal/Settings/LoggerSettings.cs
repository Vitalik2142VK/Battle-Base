using System.Collections.Generic;
using UnityEngine;

namespace FiXiK.CustomLogger
{
    public class LoggerSettings : ScriptableObject
    {
        [SerializeField] private MessageParams _logTextParams;
        [SerializeField] private MessageParams _warningTextParams;
        [SerializeField] private MessageParams _errorTextParams;

        [SerializeField] private bool _showTag;
        [SerializeField] private TagParams _defaultTag;
        [SerializeField] private TagParams[] _tags = GetDefaultTags();

        [SerializeField] private ColorParams[] _colors = GetDefaultColors();

        public bool ShowTag => _showTag;

        public MessageParams GetParams(XLogType type)
        {
            return type switch
            {
                XLogType.Log => _logTextParams,
                XLogType.LogWarning => _warningTextParams,
                XLogType.LogError => _errorTextParams,
                _ => _logTextParams,
            };
        }

        public TagParams DefaultTag => _defaultTag;

        public bool TryGetTag(TagType tagType, out TagParams tagParams)
        {
            foreach (TagParams tag in _tags)
            {
                if (tag.Name == tagType.ToString())
                {
                    tagParams = tag;

                    return true;
                }
            }

            tagParams = null;

            return false;
        }

        public ColorParams[] GetAllColors() =>
            _colors ?? GetDefaultColors();

        public string[] GetAllTagNames()
        {
            HashSet<string> uniqueNames = new();

            if (_defaultTag != null && string.IsNullOrWhiteSpace(_defaultTag.Name) == false)
                uniqueNames.Add(_defaultTag.Name);

            if (_tags != null)
            {
                foreach (TagParams tag in _tags)
                {
                    if (tag != null && string.IsNullOrWhiteSpace(tag.Name) == false)
                        uniqueNames.Add(tag.Name);
                }
            }

            if (uniqueNames.Count == 0)
                uniqueNames.Add(LoggerConstants.DefaultTagName);

            string[] result = new string[uniqueNames.Count];
            uniqueNames.CopyTo(result);

            return result;
        }

        public void ResetToDefault()
        {
            _showTag = true;
            _defaultTag = new TagParams(LoggerConstants.DefaultTagName, new Color32(0x00, 0xBC, 0xD4, 0xFF));
            _tags = GetDefaultTags();
            _logTextParams = new MessageParams(new Color32(0xF2, 0xF2, 0xF2, 0xFF), FontStyle.Normal);
            _warningTextParams = new MessageParams(new Color32(0xFF, 0xF5, 0x82, 0xFF), FontStyle.Normal);
            _errorTextParams = new MessageParams(new Color32(0xFF, 0x82, 0x82, 0xFF), FontStyle.Normal);
            _colors = GetDefaultColors();
        }

        private static TagParams[] GetDefaultTags()
        {
            return new TagParams[]
            {
                new("GAMEPLAY", new Color32(0x8C, 0xFF, 0x91, 0xFF)),
                new("UI", new Color32(0x21, 0x96, 0xF3, 0xFF)),
                new("AUDIO", new Color32(0x9C, 0x27, 0xB0, 0xFF)),
                new("AI", new Color32(0x00, 0x96, 0x88, 0xFF)),
                new("PHYSICS", new Color32(0xF4, 0x43, 0x36, 0xFF)),
                new("INPUT", new Color32(0xFF, 0xEB, 0x3B, 0xFF)),
                new("ANIMATION", new Color32(0xE9, 0x1E, 0x63, 0xFF)),
                new("EDITOR", new Color32(0x8B, 0xC3, 0x4A, 0xFF)),
                new("ANALYTICS", new Color32(0xE6, 0x4A, 0x19, 0xFF)),
            };
        }

        private static ColorParams[] GetDefaultColors()
        {
            return new ColorParams[]
            {
                new("Red", new Color32(0xFF, 0x82, 0x82, 0xFF)),
                new("Green", new Color32(0x94, 0xFF, 0x94, 0xFF)),
                new("Blue", new Color32(0x9D, 0x9D, 0xFF, 0xFF)),
                new("Yellow", new Color32(0xFF, 0xFF, 0x8A, 0xFF)),
                new("Cyan", new Color32(0x00, 0xFF, 0xFF, 0xFF)),
                new("Magenta", new Color32(0xFF, 0x8C, 0xFF, 0xFF)),
                new("White", new Color32(0xFF, 0xFF, 0xFF, 0xFF)),
                new("Black", new Color32(0x00, 0x00, 0x00, 0xFF)),
                new("Gray", new Color32(0xCB, 0xCB, 0xCB, 0xFF)),
                new("Orange", new Color32(0xFF, 0xD6, 0x8C, 0xFF)),
                new("Purple", new Color32(0x80, 0x00, 0x80, 0xFF)),
                new("Pink", new Color32(0xFF, 0xC0, 0xCB, 0xFF)),
                new("Brown", new Color32(0xA5, 0x2A, 0x2A, 0xFF)),
                new("Teal", new Color32(0x00, 0x80, 0x80, 0xFF)),
                new("Lime", new Color32(0x00, 0xFF, 0x00, 0xFF)),
            };
        }
    }
}