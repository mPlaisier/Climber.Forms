using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace Climber.Forms.Core
{
    public abstract class BaseSubscriptionDetail : ICell
    {
        #region Properties

        public Subscription Subscription { get; }

        public DateTime DatePurchase => Subscription.DatePurchase;

        public ClimbingClub Club => Subscription.Club;

        public abstract ESubscriptionType Type { get; }

        public string LblType => Type.GetLabel();

        public Action ActionClicked { get; set; }

        #endregion

        #region Commands

        ICommand _commandClicked;
        public ICommand CommandClicked => _commandClicked ??= new Command(ActionClicked);

        #endregion

        #region Constructor

        protected BaseSubscriptionDetail(Subscription subscription)
        {
            Subscription = subscription;
        }

        #endregion
    }
}
