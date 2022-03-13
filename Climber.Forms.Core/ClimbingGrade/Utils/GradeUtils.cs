using System;
using System.Collections.Generic;

namespace Climber.Forms.Core
{
    public static class GradeUtils
    {
        public static string GetLabel(this EGrade grade)
        {
            return grade switch
            {
                EGrade.Unknown => "Unknown",
                EGrade.Four => "4",
                EGrade.FiveA => "5a",
                EGrade.FiveB => "5b",
                EGrade.FiveC => "5c",
                EGrade.FiveCPlus => "5c+",
                EGrade.SixA => "6a",
                EGrade.SixAPlus => "6a+",
                EGrade.SixB => "6b",
                EGrade.SixBPlus => "6b+",
                EGrade.SixC => "6c",
                EGrade.SixCPlus => "6c+",
                EGrade.SevenA => "7a",
                EGrade.SevenAPlus => "7a+",
                EGrade.SevenB => "7b",
                EGrade.SevenBPlus => "7b+",
                EGrade.SevenC => "7c",
                EGrade.SevenCPlus => "7c+",
                EGrade.EightA => "8a",
                EGrade.EightAPlus => "8a+",
                EGrade.EightB => "8b",
                EGrade.EightBPlus => "8b+",
                EGrade.EightC => "8c",
                EGrade.EightCPlus => "8c+",
                EGrade.NineA => "9a",
                _ => throw new ArgumentException($"Label not found for {grade}"),
            };
        }

        public static IEnumerable<EGrade> GetGrades()
        {
            return new List<EGrade>
            {
                EGrade.FiveB,
                EGrade.FiveC,
                EGrade.FiveCPlus,
                EGrade.SixA,
                EGrade.SixAPlus,
                EGrade.SixB,
                EGrade.SixBPlus,
                EGrade.SixC,
                EGrade.SixCPlus,
                EGrade.SevenA
            };
        }
    }
}
