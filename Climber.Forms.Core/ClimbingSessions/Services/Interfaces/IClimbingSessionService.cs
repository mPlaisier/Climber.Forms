using System.Collections.Generic;

namespace Climber.Forms.Core
{
    public interface IClimbingSessionService
    {
        #region Methods

        List<ClimbingSessionItem> GetClimbingSessions();

        #endregion
    }
}
