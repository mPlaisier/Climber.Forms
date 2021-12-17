using System;
using System.Threading.Tasks;
using PropertyChanged;

namespace Climber.Forms.Core
{
    [AddINotifyPropertyChangedInterface]
    public class EquipmentDetailViewModel : BaseViewModel<Equipment>
    {
        readonly IEquipmentService _equipmentService;
        readonly IClimbingTaskService _taskService;

        Equipment _equipment;

        #region Properties

        public override string Title => _equipment == null
                                           ? Labels.Equipment_Detail_Create_Title
                                           : Labels.Equipment_Detail_Update_Title;

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
        public string ConfirmButtonLabel => _equipment == null
                                           ? Labels.Equipment_Detail_Button_Create_Confirm
                                           : Labels.Equipment_Detail_Button_Update_Confirm;

        public bool IsConfirmButtonEnabled => IsValid();

        #endregion

        #region Commands

        IAsyncCommand _commandConfirm;
        public IAsyncCommand CommandConfirm => _commandConfirm ??= new AsyncCommand(SaveEquipment, IsValid);

        IAsyncCommand _commandDeleteEquipment;
        public IAsyncCommand CommandDeleteEquipment => _commandDeleteEquipment ??= new AsyncCommand(DeleteEquipment);

        #endregion

        #region Constructor

        public EquipmentDetailViewModel(IEquipmentService equipmentService, IClimbingTaskService taskService)
        {
            _equipmentService = equipmentService;
            _taskService = taskService;
        }

        #endregion

        #region LifeCycle

        public override void Prepare(Equipment parameter)
        {
            _equipment = parameter;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (_equipment != null)
            {
                SelectedDate = _equipment.DatePurchase;

                Description = _equipment.Description;

                PriceValue = _equipment.Price.ToString();
                IsActive = _equipment.IsActive;
            }

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

            //Create
            if (_equipment == null)
            {
                var equipment = new Equipment(SelectedDate.Value, Description, price, IsActive);

                await _taskService.Execute(async () =>
                {
                    await _equipmentService.AddEquipment(equipment);
                    await CoreMethods.PopPageModel(new EquipmentDetailResult(true, ECrud.Create), false, true);
                });
            }
            else //Update
            {
                _equipment.DatePurchase = SelectedDate.Value;
                _equipment.Description = Description;
                _equipment.Price = price;
                _equipment.IsActive = IsActive;

                await _taskService.Execute(async () =>
                {
                    await _equipmentService.UpdateEquipment(_equipment);
                    await CoreMethods.PopPageModel(new EquipmentDetailResult(true, ECrud.Update), false, true);
                });
            }
        }

        async Task DeleteEquipment()
        {
            if (_equipment != null)
            {
                await _taskService.ExecuteDelete(async () =>
                {
                    await _equipmentService.DeleteEquipment(_equipment);
                    await CoreMethods.PopPageModel(new EquipmentDetailResult(true, ECrud.Delete), false, true);
                });
            }
        }

        #endregion
    }
}