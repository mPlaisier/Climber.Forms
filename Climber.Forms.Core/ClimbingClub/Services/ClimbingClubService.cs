using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class ClimbingClubService : IClimbingClubService
    {
        readonly IDatabaseService _database;

        #region Constructor

        public ClimbingClubService(IDatabaseService databaseService)
        {
            _database = databaseService;
        }

        #endregion

        #region Public

        public async Task<IEnumerable<ClimbingClub>> GetClubs()
        {
            //TODO until create is done
            var dbClimbingClubs = new List<DbClimbingClub>
            {
                new DbClimbingClub{ Id = 0, Name = "Bleau Climbing team", IsMember = true },
                new DbClimbingClub{ Id = 1, Name = "Rhino", IsMember = false },
                new DbClimbingClub{ Id = 2, Name = "Balance", IsMember = false },
            };

            var lstClimbingClubs = dbClimbingClubs.Select(club => (ClimbingClub)club)
                                                  .OrderByDescending(o => o.IsMember)
                                                  .ThenByDescending(t => t.Name);

            return lstClimbingClubs;
        }

        public async Task AddClub(ClimbingClub club)
        {
            await _database.SaveAsync((DbClimbingClub)club);
        }

        public async Task UpdateClub(ClimbingClub club)
        {
            await _database.SaveAsync((DbClimbingClub)club);
        }

        public async Task DeleteClub(ClimbingClub club)
        {
            await _database.DeleteAsync((DbClimbingClub)club);
        }

        #endregion
    }
}