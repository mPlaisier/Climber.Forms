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

        public string City { get; set; }

        public Action ActionClicked { get; set; }

        #endregion

        #region Commands

        ICommand _commandClicked;
        public ICommand CommandClicked => _commandClicked ??= new Command(ActionClicked);

        #endregion

        #region Constructor

        public ClimbingClub(string name, bool isMember, string city)
        {
            Name = name;
            IsMember = isMember;
            City = city;
        }

        public ClimbingClub(DbClimbingClub club)
        {
            Id = club.Id;
            Name = club.Name;
            IsMember = club.IsMember;
            City = club.City;
        }

        #endregion

        #region Static

        public static explicit operator DbClimbingClub(ClimbingClub ClimbingClub)
        {
            var dbClimbingClub = new DbClimbingClub(ClimbingClub);
            return dbClimbingClub;
        }

        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            if (!(obj is ClimbingClub club))
                return false;

            return Id == club.Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            return hash * 23 * Id.GetHashCode();
        }

        #endregion
    }
}
