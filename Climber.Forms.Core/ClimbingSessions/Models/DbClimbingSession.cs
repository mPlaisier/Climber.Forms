using System;
using SQLite;

namespace Climber.Forms.Core
{
    public class DbClimbingSession : IWithId
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int SubscriptionId { get; set; }

        public int ClubId { get; set; }

        public string EquipmentJson { get; set; }

        public decimal Cost { get; set; }

        public EClimbingType Type { get; set; }

        public EGrade HighestGrade { get; set; }

        #endregion

        #region Constructor

        public DbClimbingSession()
        {
        }

        public DbClimbingSession(ClimbingSession session, string equipmentJson)
        {
            Id = session.Id;
            Date = session.Date;

            SubscriptionId = session.Subscription.Id;
            ClubId = session.Club.Id;
            EquipmentJson = equipmentJson;

            Cost = session.Cost;
            Type = session.Type;
            HighestGrade = session.HighestGrade;
        }

        #endregion
    }
}