using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class ClimbingClub
    {
        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsMember { get; set; }

        public Action ActionClicked { get; set; }

        #endregion

        #region Commands

        ICommand _commandClicked;
        public ICommand CommandClicked => _commandClicked ??= new Command(ActionClicked);

        #endregion

        #region Constructor

        public ClimbingClub(DbClimbingClub club)
        {
            Id = club.Id;
            Name = club.Name;
            IsMember = club.IsMember;
        }

        #endregion

        #region Static

        public static explicit operator DbClimbingClub(ClimbingClub ClimbingClub)
        {
            var dbClimbingClub = new DbClimbingClub(ClimbingClub);
            return dbClimbingClub;
        }

        #endregion
    }
}
