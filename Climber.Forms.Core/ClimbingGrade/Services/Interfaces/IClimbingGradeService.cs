using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IClimbingGradeService
    {
        Task<IEnumerable<ClimbingGradeCell>> GetHighestGrades();
    }
}