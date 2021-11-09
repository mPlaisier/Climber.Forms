using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class EquipmentService : IEquipmentService
    {
        readonly IDatabaseService _database;

        #region Constructor

        public EquipmentService(IDatabaseService databaseService)
        {
            _database = databaseService;
        }

        #endregion

        #region Public

        public async Task<IEnumerable<Equipment>> GetEquipment()
        {
            var dbEquipment = await _database.GetListAsync<DbEquipment>();

            //Conver Core to Api
            var lstEquipment = dbEquipment.Select(equipment => (Equipment)equipment)
                                          .OrderByDescending(o => o.IsActive)
                                          .ThenByDescending(t => t.DatePurchase);

            return lstEquipment;
        }

        public async Task AddEquipment(Equipment equipment)
        {
            await _database.SaveAsync((DbEquipment)equipment);
        }

        public async Task UpdateEquipment(Equipment equipment)
        {
            await _database.SaveAsync((DbEquipment)equipment);
        }

        public async Task DeleteEquipment(Equipment equipment)
        {
            await _database.DeleteAsync((DbEquipment)equipment);
        }

        #endregion
    }
}
