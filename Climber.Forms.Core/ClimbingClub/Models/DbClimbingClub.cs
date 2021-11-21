using SQLite;

namespace Climber.Forms.Core
{
    public class DbClimbingClub : IWithId
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMember { get; set; }

        #endregion

        #region Constructor

        public DbClimbingClub()
        {
        }

        public DbClimbingClub(ClimbingClub club)
        {
            Id = club.Id;
            Name = club.Name;
            IsMember = club.IsMember;
        }

        #endregion

        #region Static

        public static explicit operator ClimbingClub(DbClimbingClub dbClimbingClub)
        {
            var climbingClub = new ClimbingClub(dbClimbingClub);
            return climbingClub;
        }

        #endregion
    }
}