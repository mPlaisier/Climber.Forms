using System;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public class Subscription
    {
        #region Properties

        public int Id { get; }

        public DateTime DatePurchase { get; set; }

        public string LblDate => DatePurchase.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

        public ESubscriptionType Type { get; set; }

        public string LblType => Type.GetLabel();

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public Action ActionClicked { get; set; }

        public bool IsProtected { get; internal set; }

        #endregion

        #region Commands

        ICommand _commandClicked;
        public ICommand CommandClicked => _commandClicked ??= new Command(ActionClicked);

        #endregion

        #region Constructor

        internal Subscription(DbSubscription subscription)
        {
            Id = subscription.Id;

            DatePurchase = subscription.DatePurchase;
            Type = subscription.Type;
            Price = subscription.Price;

            IsActive = subscription.IsActive;
            IsProtected = subscription.IsProtected;
        }

        public Subscription(DateTime datePurchase, ESubscriptionType type, decimal price, bool isActive)
        {
            DatePurchase = datePurchase;
            Type = type;
            Price = price;
            IsActive = isActive;
        }

        #endregion

        #region Equqls

        public override bool Equals(object obj)
        {
            if (!(obj is Subscription subscription))
                return false;

            return Id == subscription.Id;
        }

        public override int GetHashCode()
        {
            var hash = 17;
            return hash * 23 * Id.GetHashCode();
        }

        #endregion

        #region Static

        public static explicit operator DbSubscription(Subscription subscription)
        {
            var dbSubscription = new DbSubscription(subscription);
            return dbSubscription;
        }

        public static Subscription CreateProgramSubscription()
        {
            return new Subscription(DateTime.Now, ESubscriptionType.SingleEntree, 0, true)
            {
                IsProtected = true
            };
        }

        #endregion
    }
}