using System;

namespace BattleBase
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public sealed class PreviewExcludedAttribute : Attribute
    {
        public PreviewExcludedAttribute(PreviewExclusionMode mode = PreviewExclusionMode.Disable)
        {
            Mode = mode;
        }

        public PreviewExclusionMode Mode { get; }
    }
}