using UnityEngine;

namespace FiXiK.CustomLogger
{
    public static class LoggerBuilder
    {
        public static MessageInfo Message(object message)
        {
            MessageParams defaultParams = SettingsProvider.Settings.GetParams(XLogType.Log);

            return new()
            {
                Message = message,
                MessageColor = defaultParams.Color,
                FontStyle = defaultParams.FontStyle,
                Tag = TagType.LOGGER,
                MessageType = XLogType.Log,
                Context = null,
                Condition = true,
                IsColorOverridden = false,
                IsStyleOverridden = false
            };
        }

        public static MessageInfo WithColor(this MessageInfo info, Color32 color)
        {
            info.MessageColor = color;
            info.IsColorOverridden = true;

            return info;
        }

        public static MessageInfo WithFontStyle(this MessageInfo info, FontStyle fontStyle)
        {
            info.FontStyle = fontStyle;
            info.IsStyleOverridden = true;

            return info;
        }

        public static MessageInfo WithTag(this MessageInfo info, TagType tag)
        {
            info.Tag = tag;

            return info;
        }

        public static MessageInfo WithLogType(this MessageInfo info, XLogType messageType)
        {
            info.MessageType = messageType;

            MessageParams newParams = SettingsProvider.Settings.GetParams(messageType);

            if (info.IsColorOverridden == false)
                info.MessageColor = newParams.Color;

            if (info.IsStyleOverridden == false)
                info.FontStyle = newParams.FontStyle;

            return info;
        }

        public static MessageInfo WithContext(this MessageInfo info, Object context)
        {
            info.Context = context;

            return info;
        }

        public static MessageInfo WithCondition(this MessageInfo info, bool condition)
        {
            info.Condition = condition;

            return info;
        }
    }
}