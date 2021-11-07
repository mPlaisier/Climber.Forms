using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class ClimbingSessionService : IClimbingSessionService
    {
        readonly IDatabaseService _database;

        #region Constructor

        public ClimbingSessionService(IDatabaseService database)
        {
            _database = database;
        }

        #endregion

        #region Public

        public Task<List<ClimbingSessionItem>> GetClimbingSessions()
        {
            return _database.GetListAsync<ClimbingSessionItem>();
        }

        #endregion
    }
}
