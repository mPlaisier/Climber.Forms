using System;
using System.Collections.Generic;

namespace Climber.Forms.Core
{
    public class ClimbingSessionItem
    {
        #region Properties

        public string Id { get; }

        public DateTime Data { get; }

        public string LblDate => Data.ToLongDateString().FirstCharToUpper();

        public string SubscriptionId { get; set; }

        public List<string> MaterialId { get; set; }

        public decimal Cost { get; set; }

        public EClimbingType Type { get; set; }

        #endregion

        #region Constructor

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
