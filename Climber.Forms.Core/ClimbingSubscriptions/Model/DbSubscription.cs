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

        public ESubscriptionType Type { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        #endregion

        #region Constructor

        public DbSubscription()
        {
        }

        public DbSubscription(int id, DateTime datePurchase, ESubscriptionType type, decimal price, bool isActive)
        {
            Id = id;

            DatePurchase = datePurchase;
            Type = type;
            Price = price;
            IsActive = isActive;
        }

        internal DbSubscription(Subscription subscription)
        {
            Id = subscription.Id;

            DatePurchase = subscription.DatePurchase;
            Type = subscription.Type;
            Price = subscription.Price;
            IsActive = subscription.IsActive;
        }

        #endregion

        #region Static

        public static explicit operator Subscription(DbSubscription dbSubscription)
        {
            var subscription = new Subscription(dbSubscription);
            return subscription;
        }

        #endregion
    }
}