using System;
using System.Threading.Tasks;
using PropertyChanged;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class ClimbingClubDetailViewModel : BaseViewModel<ClimbingClub>
    {
        readonly IClimbingClubService _clubService;
        readonly IClimbingTaskService _taskService;

        ClimbingClub _club;

        #region Properties

        public override string Title => _club == null
                                           ? Labels.Club_Detail_Create_Title
                                           : Labels.Club_Detail_Update_Title;

        //Name
        public string NamePlaceholder => Labels.Club_detail_Name_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public string Name { get; set; }

        //Membership
        public string IsMemberLabel => Labels.Club_Detail_Member_Label;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public bool IsMember { get; set; }

        //City
        public string CityPlaceholder => Labels.Club_detail_City_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public string City { get; set; }

        //Confirm button
        public string ConfirmButtonLabel => _club == null
                                           ? Labels.Club_Detail_Button_Create_Confirm
                                           : Labels.Club_Detail_Button_Update_Confirm;

        public bool IsConfirmButtonEnabled => IsValid();

        #endregion

        #region Commands

        IAsyncCommand _commandConfirm;
        public IAsyncCommand CommandConfirm => _commandConfirm ??= new AsyncCommand(SaveClub, IsValid);

        IAsyncCommand _commandDeleteClub;
        public IAsyncCommand CommandDeleteClub => _commandDeleteClub ??= new AsyncCommand(DeleteClub);

        #endregion

        #region Constructor

        public ClimbingClubDetailViewModel(IClimbingClubService clubService, IClimbingTaskService taskService)
        {
            _clubService = clubService;
            _taskService = taskService;
        }

        #endregion

        #region LifeCycle

        public override void Prepare(ClimbingClub parameter)
        {
            _club = parameter;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (_club != null)
            {
                Name = _club.Name;
                IsMember = _club.IsMember;
                City = _club.City;
            }

            RaisePropertyChanged(nameof(IsConfirmButtonEnabled));
        }

        #endregion

        #region Private

        async Task SaveClub()
        {
            Name = Name.Trim();
            City = City.Trim();

            //Create
            if (_club == null)
            {
                var club = new ClimbingClub(Name, IsMember, City);

                await _taskService.Execute(async () =>
                {
                    await _clubService.AddClub(club);
                    await CoreMethods.PopPageModel(new CrudResult(ECrud.Create), false, true);
                });
            }
            else //Update
            {
                _club.Name = Name;
                _club.IsMember = IsMember;
                _club.City = City;

                await _taskService.Execute(async () =>
                {
                    await _clubService.UpdateClub(_club);
                    await CoreMethods.PopPageModel(new CrudResult(ECrud.Update), false, true);
                });
            }
        }

        async Task DeleteClub()
        {
            if (_club != null)
            {
                await _taskService.ExecuteDelete(async () =>
                {
                    await _clubService.DeleteClub(_club);
                    await CoreMethods.PopPageModel(new CrudResult(ECrud.Delete), false, true);
                });
            }
        }

        bool IsValid()
        {
            if (Name == null || City == null)
                return false;

            var name = Name.Trim();
            var city = City.Trim();

            return !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(city);
        }

        #endregion
    }
}