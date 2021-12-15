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

        public string City { get; set; }

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
            City = club.City;
        }

        #endregion

        #region Static

        public static explicit operator ClimbingClub(DbClimbingClub dbClimbingClub)
        {
            if (dbClimbingClub != null)
                return new ClimbingClub(dbClimbingClub);
            return default;
        }

        #endregion
    }
}