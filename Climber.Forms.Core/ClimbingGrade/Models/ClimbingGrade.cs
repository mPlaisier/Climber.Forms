using System.Collections.Generic;
using System.Linq;
using CBP.Extensions;

namespace Climber.Forms.Core
{
    public class ClimbingGrade
    {
        #region Properties

        public EGrade Grade { get; }

        public string Label => Grade.GetLabel();

        #endregion

        #region Constructor

        public ClimbingGrade(EGrade grade)
        {
            Grade = grade;
        }

        #endregion

        #region Static

        public static List<ClimbingGrade> GetValidClimbingGrades()
        {
            var grades = GradeUtils.GetGrades();

            return grades.Select(grade => new ClimbingGrade(grade)).ToList();
        }

        public static List<ClimbingGrade> GetAllClimbingGrades()
        {
            var grades = EnumUtil.GetValues<EGrade>();

            return grades.Select(grade => new ClimbingGrade(grade)).ToList();
        }

        #endregion
    }
}