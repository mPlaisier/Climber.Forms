using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IClimbingSessionService
    {
        #region Methods

        Task<IEnumerable<ClimbingSession>> GetClimbingSessions();
        Task<int> GetCountForSubscription(Subscription subscription);
        Task SaveSession(ClimbingSession session);
        Task DeleteSession(ClimbingSession session);

        #endregion
    }
}
