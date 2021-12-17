using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class MessageService : IMessageService
    {
        readonly IToastMessageService _toastService;
        readonly IDialogService _dialogService;

        #region Constructor

        public MessageService(IToastMessageService toastMessageService, IDialogService dialogService)
        {
            _toastService = toastMessageService;
            _dialogService = dialogService;
        }

        #endregion

        #region Public

        public void ShowInfoMessage(string message, EMessagePriority priority)
        {
            if (priority == EMessagePriority.Low)
            {
                _toastService.ShowToast(message);
            }
            else if (priority == EMessagePriority.Medium)
            {
                _toastService.ShowDismissToast(message);
            }
            else if (priority == EMessagePriority.High)
            {
                _dialogService.ShowDialog("Info", message);
            }
        }

        public void ShowErrorMessage(string title, string message)
        {
            _dialogService.ShowDialog(title, message);
        }

        public async Task<bool> AskConfirmation(string title, string message)
        {
            return await _dialogService.ConfirmDialog(title, message);
        }

        public async Task<bool> AskDeleteConfirmation()
        {
            return await AskConfirmation(Labels.LblDelete, Labels.LblConfirm).ConfigureAwait(false);
        }

        #endregion
    }
}