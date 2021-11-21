using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class ClimbingClubOverviewViewModel : BaseViewModel
    {
        readonly IClimbingClubService _clubService;

        #region Properties

        public override string Title => Labels.Club_Overview_Title;

        public ObservableCollection<ClimbingClub> Clubs { get; private set; }

        #endregion

        #region Constructor

        public ClimbingClubOverviewViewModel(IClimbingClubService clubService)
        {
            _clubService = clubService;
        }

        #endregion

        #region LifeCycle

        public override void Init()
        {
            LoadData().ConfigureAwait(false);
        }

        #endregion

        #region Private

        async Task LoadData()
        {
            IEnumerable<ClimbingClub> data = null;
            try
            {
                data = await _clubService.GetClubs();
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
            }

            if (data != null)
            {
                Clubs = new ObservableCollection<ClimbingClub>(data);
            }
        }

        #endregion
    }
}