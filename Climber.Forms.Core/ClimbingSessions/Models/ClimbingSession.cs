using System;
using System.Collections.Generic;

namespace Climber.Forms.Core
{
    public class ClimbingSession
    {
        #region Properties

        public int Id { get; }

        public DateTime Date { get; }

        public string LblDate => Date.ToLongDateString().FirstCharToUpper();

        public Subscription Subscription { get; }

        public decimal Cost { get; set; }

        public EClimbingType Type { get; set; }

        public List<Equipment> LstClimbingEquipmentItems { get; }

        #endregion

        #region Constructor

        public ClimbingSession()
        {
        }

        public ClimbingSession(DbClimbingSession dbClimbingSession, List<Equipment> lstClimbingEquipmentItems, Subscription subscription)
        {
            Id = dbClimbingSession.Id;
            Date = dbClimbingSession.Date;
            Cost = dbClimbingSession.Cost;
            Type = dbClimbingSession.Type;

            LstClimbingEquipmentItems = lstClimbingEquipmentItems;

            Subscription = subscription;
        }

        public ClimbingSession(DateTime selectedDate, Subscription subscription, ClimbingType climbingType)
        {
            Date = selectedDate;
            Subscription = subscription;
            Type = climbingType.Type;
        }

        #endregion
    }
}