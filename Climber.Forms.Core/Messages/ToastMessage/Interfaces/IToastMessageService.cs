namespace Climber.Forms.Core
{
    public interface IToastMessageService
    {
        void ShowToast(string message);
        void ShowDismissToast(string message);
    }
}