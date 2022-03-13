using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IDashboardSubscriptionScreenManagerService
    {
        Task<ObservableCollection<BaseSubscriptionDetail>> CreateSubscriptionCells(IEnumerable<Subscription> data);
    }
}