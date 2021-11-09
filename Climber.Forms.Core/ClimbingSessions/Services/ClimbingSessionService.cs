using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Climber.Forms.Core
{
    public class ClimbingSessionService : IClimbingSessionService
    {
        readonly IDatabaseService _database;

        #region Constructor

        public ClimbingSessionService(IDatabaseService database)
        {
            _database = database;
        }

        #endregion

        #region Public

        public async Task<IEnumerable<ClimbingSession>> GetClimbingSessions()
        {
            //Fetch required data
            var dbClimbingSessions = await _database.GetListAsync<DbClimbingSession>();
            var dbClimbingEquipment = await _database.GetListAsync<DbEquipment>();

            //Conver Core to Api
            var lstEquipment = await CreateClimbingSessionsFromResult(dbClimbingSessions, dbClimbingEquipment);

            return lstEquipment.OrderByDescending(o => o.Date);
        }

        public async Task AddSession(ClimbingSession session)
        {
            var equipmentJson = GetEquipmentJson(session);
            var dbClimbingSession = new DbClimbingSession(session, equipmentJson);

            await _database.SaveAsync(dbClimbingSession);
        }

        #endregion

        #region Private

        async Task<List<ClimbingSession>> CreateClimbingSessionsFromResult(List<DbClimbingSession> dbClimbingSessions, List<DbEquipment> dbClimbingEquipment)
        {
            var lstEquipment = new List<ClimbingSession>();
            foreach (var dbSession in dbClimbingSessions)
            {
                var lstEquipmentString = JsonConvert.DeserializeObject<List<int>>(dbSession.EquipmentJson);
                var lstClimbingEquipmentItems = GetClimbingSessionEquipment(dbClimbingEquipment, lstEquipmentString);

                var DbSubscription = await _database.GetAsync<DbSubscription>(dbSession.SubscriptionId);

                var session = new ClimbingSession(dbSession, lstClimbingEquipmentItems, (Subscription)DbSubscription);

                lstEquipment.Add(session);
            }

            return lstEquipment;
        }

        List<Equipment> GetClimbingSessionEquipment(List<DbEquipment> dbClimbingEquipment, List<int> lstEquipmentString)
        {
            if (lstEquipmentString == null || lstEquipmentString.Count == 0)
                return new List<Equipment>();

            var sessionDbClimbingList = dbClimbingEquipment.Where(x => lstEquipmentString.Contains(x.Id));
            if (sessionDbClimbingList == null || sessionDbClimbingList.Any())
                return new List<Equipment>();

            return sessionDbClimbingList.Select(x => new Equipment(x)).ToList();
        }

        string GetEquipmentJson(ClimbingSession session)
        {
            var equipmentListString = new List<int>();
            if (session.LstClimbingEquipmentItems != null && session.LstClimbingEquipmentItems.Count == 0)
                equipmentListString = session.LstClimbingEquipmentItems.Select(x => x.Id).ToList();

            return JsonConvert.SerializeObject(equipmentListString);
        }

        #endregion
    }
}