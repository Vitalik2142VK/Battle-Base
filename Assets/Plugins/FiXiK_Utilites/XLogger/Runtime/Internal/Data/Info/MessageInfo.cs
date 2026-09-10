using UnityEngine;

namespace FiXiK.CustomLogger
{
    public struct MessageInfo
    {
        public object Message;
        public Color32 MessageColor;
        public FontStyle FontStyle;
        public TagType Tag;
        public XLogType MessageType;
        public Object Context;
        public bool Condition;
        public bool IsColorOverridden;
        public bool IsStyleOverridden;
    }
}