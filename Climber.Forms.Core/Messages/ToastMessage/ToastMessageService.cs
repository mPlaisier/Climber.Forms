using System.Drawing;
using Acr.UserDialogs;

namespace Climber.Forms.Core
{
    public class ToastMessageService : IToastMessageService
    {
        readonly IUserDialogs _userDialogs;

        #region Constructor

        public ToastMessageService(IUserDialogs userDialogs)
        {
            _userDialogs = userDialogs;
        }

        #endregion

        #region Public

        public void ShowToast(string message)
        {
            _userDialogs.Toast(message);
        }

        public void ShowDismissToast(string message)
        {
            _userDialogs.Toast(new ToastConfig(message)
            {
                Duration = new System.TimeSpan(0, 0, 10),
                Action = new ToastAction()
                {
                    TextColor = Color.White,
                    Text = LblMessages.Toast_Dismiss,
                }
            });
        }

        #endregion
    }
}