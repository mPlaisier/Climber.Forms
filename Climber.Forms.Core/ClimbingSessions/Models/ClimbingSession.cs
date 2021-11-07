using System;
using System.Collections.Generic;
using SQLite;

namespace Climber.Forms.Core
{
    public class ClimbingSessionItem
    {
        #region Properties

        [PrimaryKey]
        public string Id { get; }

        public DateTime Data { get; }

        [Ignore]
        public string LblDate => Data.ToLongDateString().FirstCharToUpper();

        public string SubscriptionId { get; set; }

        [Ignore]
        public List<string> MaterialId { get; set; }

        public decimal Cost { get; set; }

        public EClimbingType Type { get; set; }

        #endregion

        #region Constructor

        public ClimbingSessionItem()
        {

        }

        public ClimbingSessionItem(DateTime date, EClimbingType type)
        {
            Data = date;
            Type = type;
        }

        public ClimbingSessionItem(string id, DateTime date, string subscriptionId, List<string> materialId, decimal cost)
        {
            Id = id;
            Data = date;
            SubscriptionId = subscriptionId;
            MaterialId = materialId;
            Cost = cost;
        }

        #endregion
    }
}
