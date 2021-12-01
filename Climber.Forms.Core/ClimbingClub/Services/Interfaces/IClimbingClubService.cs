using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IClimbingClubService
    {
        Task AddClub(ClimbingClub club);
        Task DeleteClub(ClimbingClub club);
        Task<IEnumerable<ClimbingClub>> GetClubs();
        Task UpdateClub(ClimbingClub club);
    }
}