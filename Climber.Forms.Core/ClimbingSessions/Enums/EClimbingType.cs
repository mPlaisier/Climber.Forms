using System;
using System.ComponentModel;

namespace Climber.Forms.Core
{
    public enum EClimbingType
    {
        [Description("B")]
        Boulder,
        [Description("L")]
        Length
    }

    public static class EClimbingTypeExtension
    {
        public static string GetLabel(this EClimbingType type)
        {
            return type switch
            {
                EClimbingType.Boulder => "Boulder",
                EClimbingType.Length => "Lengths",
                _ => throw new ArgumentException($"No label found for type {type}")
            };
        }
    }
}