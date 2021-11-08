using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
                var equipment = new ObservableCollection<Equipment>(data);

                equipment.ForEach((subscription) =>
                {
                    subscription.ActionClicked = () =>
                    {
                        //TODO Navigation to detail (issue #23)
                    };
                });

                Equipment = equipment;
            }
        }

        #endregion
    }
}