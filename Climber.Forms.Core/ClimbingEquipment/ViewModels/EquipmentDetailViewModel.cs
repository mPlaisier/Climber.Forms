using System;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class EquipmentDetailViewModel : BaseViewModel<Equipment>
    {
        readonly IEquipmentService _equipmentService;

        #region Properties

        public override string Title => Labels.Equipment_Detail_Title;

        //Date
        public string DatePlaceholder => Labels.Equipment_Detail_Date_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public DateTime? SelectedDate { get; set; } = DateTime.Now;

        //Description
        public string DescriptionPlaceholder => Labels.Equipment_Detail_Description_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public string Description { get; set; }

        //Price
        public string PricePlaceholder => Labels.Equipment_Detail_Price_Placeholder;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public string PriceValue { get; set; }

        //Active equipment
        public string IsActiveLabel => Labels.Equipment_Detail_Active_Switch_Label;

        [AlsoNotifyFor(nameof(IsConfirmButtonEnabled))]
        public bool IsActive { get; set; } = true;

        //Confirm button
        public string ConfirmButtonLabel => Labels.Equipment_Detail_Button_Create_Confirm;

        public bool IsConfirmButtonEnabled => IsValid();

        #endregion

        #region Commands

        Command _commandConfirm;
        public Command CommandConfirm => _commandConfirm ??= new Command(async () => await SaveEquipment().ConfigureAwait(false));

        #endregion

        #region Constructor

        public EquipmentDetailViewModel(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
        }

        #endregion

        #region LifeCycle

        public override void Init(Equipment parameter)
        {
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            RaisePropertyChanged(nameof(IsConfirmButtonEnabled));
        }

        #endregion

        #region Private

        bool IsValid()
        {
            if (SelectedDate == null)
                return false;

            if (Description == null || Description.Trim().Equals(string.Empty))
                return false;

            if (PriceValue == null || PriceValue == string.Empty || !decimal.TryParse(PriceValue, out _))
                return false;

            return true;
        }

        async Task SaveEquipment()
        {
            decimal.TryParse(PriceValue, out var price);

            var equipment = new Equipment(SelectedDate.Value, Description, price, IsActive);

            try
            {
                await _equipmentService.AddEquipment(equipment);
                await CoreMethods.PopPageModel(new EquipmentDetailResult(true, ECrud.Create), false, true);
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
            }
        }

        #endregion
    }
}