using System;
using System.Collections.Generic;
using System.Windows.Input;
using CBP.Extensions;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class ClimbingSession : ICell
    {
        #region Properties

        public int Id { get; }

        public DateTime Date { get; set; }

        public string LblDate => Date.ToLongDateString().FirstCharToUpper();

        public Subscription Subscription { get; set; }

        public ClimbingClub Club { get; set; }

        public decimal Cost { get; set; }

        public EClimbingType Type { get; set; }

        public EGrade HighestGrade { get; set; }

        public List<Equipment> LstClimbingEquipmentItems { get; }

        public Action ActionClicked { get; set; }

        #endregion

        #region Commands

        ICommand _commandClicked;
        public ICommand CommandClicked => _commandClicked ??= new Command(ActionClicked);

        #endregion

        #region Constructor

        public ClimbingSession()
        {
        }

        public ClimbingSession(DbClimbingSession dbClimbingSession, List<Equipment> lstClimbingEquipmentItems, Subscription subscription, ClimbingClub club)
        {
            Id = dbClimbingSession.Id;

            Date = dbClimbingSession.Date;
            Cost = dbClimbingSession.Cost;
            Type = dbClimbingSession.Type;
            HighestGrade = dbClimbingSession.HighestGrade;

            LstClimbingEquipmentItems = lstClimbingEquipmentItems;

            Subscription = subscription;
            Club = club;
        }

        public ClimbingSession(DateTime selectedDate, Subscription subscription, ClimbingClub club, ClimbingType climbingType, ClimbingGrade climbingGrade)
        {
            Date = selectedDate;

            Subscription = subscription;
            Club = club;

            Type = climbingType.Type;
            HighestGrade = climbingGrade.Grade;
        }

        #endregion
    }
}