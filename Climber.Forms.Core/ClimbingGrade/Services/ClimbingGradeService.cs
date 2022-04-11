using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CBP.Extensions;

namespace Climber.Forms.Core
{
    public class ClimbingGradeService : IClimbingGradeService
    {
        readonly IDatabaseService _database;

        #region Constructor

        public ClimbingGradeService(IDatabaseService database)
        {
            _database = database;
        }

        #endregion

        #region Public

        public async Task<IEnumerable<ClimbingGradeCell>> GetHighestGrades()
        {
            var grades = new List<ClimbingGradeCell>();

            var highestBoulderGrade = await GetHighestGradeForType(EClimbingType.Boulder).ConfigureAwait(false);
            if (highestBoulderGrade.IsNotNull())
                grades.Add(highestBoulderGrade);

            var highestLengthGrade = await GetHighestGradeForType(EClimbingType.Length).ConfigureAwait(false);
            if (highestLengthGrade.IsNotNull())
                grades.Add(highestLengthGrade);

            return grades;
        }

        #endregion

        #region Private

        async Task<ClimbingGradeCell> GetHighestGradeForType(EClimbingType type)
        {
            var lst = await _database.GetListAsync<DbClimbingSession>(x => x.Type == type);

            if (lst.IsNotNullAndHasItems())
            {
                var grade = lst.Max(x => x.HighestGrade);
                return new ClimbingGradeCell(grade, type);
            }
            return null;
        }

        #endregion
    }
}