using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IEquipmentService
    {
        Task AddEquipment(Equipment equipment);
        Task DeleteEquipment(Equipment equipment);
        Task<IEnumerable<Equipment>> GetEquipment();
        Task UpdateEquipment(Equipment equipment);
    }
}