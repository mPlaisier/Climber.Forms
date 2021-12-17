using System.Threading.Tasks;
using Acr.UserDialogs;

namespace Climber.Forms.Core
{
    public class DialogService : IDialogService
    {
        readonly IUserDialogs _userDialogs;

        #region Constructor

        public DialogService(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        #endregion

        #region Public

        public void ShowDialog(string title, string message)
        {
            _userDialogs.Alert(message, title);
        }

        public async Task<bool> ConfirmDialog(string title, string message)
        {
            return await _userDialogs.ConfirmAsync(message, title, Labels.LblYes, Labels.LblCancel);
        }

        #endregion
    }
}