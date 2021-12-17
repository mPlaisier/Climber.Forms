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
        readonly IMessageService _messageService;
        readonly ClimbingTaskService _taskService;

        #region Properties

        public override string Title => Labels.Equipment_Title;

        public ObservableCollection<Equipment> Equipment { get; private set; }

        #endregion

        #region Commands

        IAsyncCommand _commandAddEquipment;
        public IAsyncCommand CommandAddEquipment => _commandAddEquipment ??= new AsyncCommand(async () =>
        {
            await CoreMethods.PushPageModel<EquipmentDetailViewModel>();
        });

        #endregion

        #region Constructor

        public EquipmentOverviewViewModel(IEquipmentService equipmentService, IMessageService messageService, ClimbingTaskService taskService)
        {
            _equipmentService = equipmentService;
            _messageService = messageService;
            _taskService = taskService;
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
                    _messageService.ShowInfoMessage(Labels.Equipment_Alert_Created_Body, EMessagePriority.Low);
                else if (result.Action == ECrud.Update)
                    _messageService.ShowInfoMessage(Labels.Equipment_Alert_Updated_Body, EMessagePriority.Low);
                else if (result.Action == ECrud.Delete)
                    _messageService.ShowInfoMessage(Labels.Equipment_Alert_Deleted_Body, EMessagePriority.Medium);

                Init();
            }
        }

        #endregion

        #region Private

        async Task LoadData()
        {
            IEnumerable<Equipment> data = null;

            await _taskService.Execute(async () =>
            {
                data = await _equipmentService.GetEquipment();
            });

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