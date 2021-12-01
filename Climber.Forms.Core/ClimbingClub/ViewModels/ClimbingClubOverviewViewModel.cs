using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Climber.Forms.Core
{
    public class ClimbingClubOverviewViewModel : BaseViewModel
    {
        readonly IClimbingClubService _clubService;

        #region Properties

        public override string Title => Labels.Club_Overview_Title;

        public ObservableCollection<ClimbingClub> Clubs { get; private set; }

        #endregion

        #region Commands

        ICommand _commandAddEquipment;
        public ICommand CommandAddClub => _commandAddEquipment ??= new Command(async () =>
        {
            await CoreMethods.PushPageModel<ClimbingClubDetailViewModel>();
        });

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

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

            if (returnedData is CrudResult result && result.IsSuccess)
            {
                if (result.Action == ECrud.Create)
                    CoreMethods.DisplayAlert(Labels.Club_Overview_Alert_Created_Title, Labels.Club_Overview_Alert_Created_Body, Labels.Ok);
                else if (result.Action == ECrud.Update)
                    CoreMethods.DisplayAlert(Labels.Club_Overview_Alert_Updated_Title, Labels.Club_Overview_Alert_Updated_Body, Labels.Ok);
                else if (result.Action == ECrud.Delete)
                    CoreMethods.DisplayAlert(Labels.Club_Overview_Alert_Deleted_Title, Labels.Club_Overview_Alert_Deleted_Body, Labels.Ok);

                Init();
            }
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
                var clubs = new ObservableCollection<ClimbingClub>(data);

                clubs.ForEach((club) =>
                {
                    club.ActionClicked = () =>
                    {
                        CoreMethods.PushPageModel<ClimbingClubDetailViewModel>(club);
                    };
                });

                Clubs = clubs;
            }
        }

        #endregion
    }
}