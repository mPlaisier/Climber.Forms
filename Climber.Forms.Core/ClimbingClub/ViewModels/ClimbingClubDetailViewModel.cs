using System;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class ClimbingClubDetailViewModel : BaseViewModel<Equipment>
    {
        readonly IClimbingClubService _clubService;

        #region Properties

        public override string Title => Labels.Club_Detail_Create_Title;

        //Name
        public string NamePlaceholder => Labels.Club_detail_Name_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public string Name { get; set; }

        //Membership
        public string IsMemberLabel => Labels.Club_Detail_Member_Label;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public bool IsMember { get; set; }

        //Confirm button
        public string ConfirmButtonLabel => Labels.Club_Detail_Button_Create_Confirm;

        public bool IsConfirmButtonEnabled => Name != null && !Name.Equals(string.Empty);

        #endregion

        #region Commands

        ICommand _commandConfirm;
        public ICommand CommandConfirm => _commandConfirm ??= new Command(async () => await SaveClub().ConfigureAwait(false));

        #endregion

        #region Constructor

        public ClimbingClubDetailViewModel(IClimbingClubService clubService)
        {
            _clubService = clubService;
        }

        #endregion

        #region LifeCycle

        public override void Prepare(Equipment parameter)
        {
            //Done in #58
        }

        #endregion

        #region Private

        async Task SaveClub()
        {
            var club = new ClimbingClub(Name, IsMember);

            try
            {
                await _clubService.AddClub(club);
                await CoreMethods.PopPageModel(new CrudResult(ECrud.Create), false, true);
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
            }
        }

        #endregion
    }
}