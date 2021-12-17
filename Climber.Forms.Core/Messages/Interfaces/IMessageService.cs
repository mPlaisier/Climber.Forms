using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IMessageService
    {
        void ShowErrorMessage(string title, string message);

        void ShowInfoMessage(string message, EMessagePriority priority);

        Task<bool> AskConfirmation(string title, string message);
        Task<bool> AskDeleteConfirmation();
    }
}