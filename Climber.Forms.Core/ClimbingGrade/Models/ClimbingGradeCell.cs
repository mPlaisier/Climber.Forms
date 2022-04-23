namespace Climber.Forms.Core
{
    public class ClimbingGradeCell : ClimbingGrade, ICell
    {
        #region Properties

        public EClimbingType Type { get; }

        public string LblType => Type.GetLabel();

        #endregion

        #region Constructor

        public ClimbingGradeCell(EGrade grade, EClimbingType type) : base(grade)
        {
            Type = type;
        }

        #endregion
    }
}