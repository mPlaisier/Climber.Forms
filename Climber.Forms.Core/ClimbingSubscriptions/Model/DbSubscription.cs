using System;
using SQLite;

namespace Climber.Forms.Core
{
    public class DbSubscription : IWithId
    {
        #region Properties

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public DateTime DatePurchase { get; set; }

        public int? ClubId { get; set; }

        public ESubscriptionType Type { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public bool IsProtected { get; set; }

        #endregion

        #region Constructor

        public DbSubscription()
        {
        }

        internal DbSubscription(Subscription subscription)
        {
            Id = subscription.Id;

            DatePurchase = subscription.DatePurchase;

            ClubId = subscription.Club?.Id;

            Type = subscription.Type;
            Price = subscription.Price;

            IsActive = subscription.IsActive;
            IsProtected = subscription.IsProtected;
        }

        #endregion
    }
}