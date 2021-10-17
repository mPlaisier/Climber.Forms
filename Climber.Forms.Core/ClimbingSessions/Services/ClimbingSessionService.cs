using System.Collections.Generic;
using static Climber.Forms.Core.Enums;

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

        public List<ClimbingSessionItem> GetClimbingSessions()
        {
            return _database.Get<List<ClimbingSessionItem>>(EDatabaseKeys.ClimbingSessions);
        }

        #endregion
    }
}
