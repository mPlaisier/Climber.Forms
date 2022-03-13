using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FreshMvvm;

namespace Climber.Forms.Core
{
    public interface IDashboardService
    {
        Task<ObservableCollection<ICell>> GetDashboardItems(IPageModelCoreMethods pageModelCoreMethods);
    }
}