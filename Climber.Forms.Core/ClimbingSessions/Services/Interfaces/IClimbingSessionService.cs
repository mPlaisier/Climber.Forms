using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IClimbingSessionService
    {
        #region Methods

        Task<IEnumerable<ClimbingSession>> GetClimbingSessions();
        Task AddSession(ClimbingSession session);

        #endregion
    }
}
