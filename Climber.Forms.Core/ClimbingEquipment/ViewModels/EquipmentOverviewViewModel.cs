using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Climber.Forms.Core
{
    public class EquipmentOverviewViewModel : BaseViewModel
    {
        readonly IEquipmentService _equipmentService;

        #region Properties

        public override string Title => Labels.Equipment_Title;

        public ObservableCollection<Equipment> Equipment { get; private set; }

        #endregion

        #region Commands

        ICommand _commandAddEquipment;
        public ICommand CommandAddEquipment => _commandAddEquipment ??= new Command(async () =>
        {
            await CoreMethods.PushPageModel<EquipmentDetailViewModel>();
        });

        #endregion

        #region Constructor

        public EquipmentOverviewViewModel(IEquipmentService equipmentService)
        {
            _equipmentService = equipmentService;
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

            if (returnedData is EquipmentDetailResult result && result.IsSuccess)
            {
                if (result.Action == ECrud.Create)
                    CoreMethods.DisplayAlert(Labels.Equipment_Alert_Created_Title, Labels.Equipment_Alert_Created_Body, Labels.Ok);
                else if (result.Action == ECrud.Update)
                    CoreMethods.DisplayAlert(Labels.Equipment_Alert_Updated_Title, Labels.Equipment_Alert_Updated_Body, Labels.Ok);
                else if (result.Action == ECrud.Delete)
                    CoreMethods.DisplayAlert(Labels.Equipment_Alert_Deleted_Title, Labels.Equipment_Alert_Deleted_Body, Labels.Ok);

                Init();
            }
        }

        #endregion

        #region Private

        async Task LoadData()
        {
            IEnumerable<Equipment> data = null;
            try
            {
                data = await _equipmentService.GetEquipment();
            }
            catch (Exception ex)
            {
                await CoreMethods.DisplayAlert(Labels.LblError, ex.Message, Labels.Ok);
            }

            if (data != null)
            {
                var lstEquipment = new ObservableCollection<Equipment>(data);

                lstEquipment.ForEach((equipment) =>
                {
                    equipment.ActionClicked = () =>
                    {
                        CoreMethods.PushPageModel<EquipmentDetailViewModel>(equipment);
                    };
                });

                Equipment = lstEquipment;
            }
        }

        #endregion
    }
}