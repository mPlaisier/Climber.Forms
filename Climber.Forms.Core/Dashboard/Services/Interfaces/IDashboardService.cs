using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IDashboardService
    {
        Task<ObservableCollection<ICell>> GetDashboardItems();
    }
}