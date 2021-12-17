using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IDialogService
    {
        void ShowDialog(string title, string message);
        Task<bool> ConfirmDialog(string title, string message);
    }
}