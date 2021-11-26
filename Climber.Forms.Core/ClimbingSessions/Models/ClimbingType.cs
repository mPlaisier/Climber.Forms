using System.Collections.Generic;

namespace Climber.Forms.Core
{
    public class ClimbingType
    {
        #region Properties

        public EClimbingType Type { get; }

        public string Label => Type.GetLabel();

        #endregion

        #region Constructor

        public ClimbingType(EClimbingType type)
        {
            Type = type;
        }

        #endregion

        #region Static

        public static List<ClimbingType> GetClimbingTypes()
        {
            return new List<ClimbingType>
            {
                new ClimbingType(EClimbingType.Boulder),
                new ClimbingType(EClimbingType.Length),
            };
        }

        #endregion
    }
}