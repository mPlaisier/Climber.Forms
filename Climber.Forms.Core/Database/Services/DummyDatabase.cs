using System;
using System.Collections.Generic;
using static Climber.Forms.Core.Enums;

namespace Climber.Forms.Core
{
    public class DummyDatabase : IDatabaseService
    {
        List<Subscription> _subscriptions;

        #region Public

        public T Get<T>(EDatabaseKeys key) where T : class
        {
            if (key == EDatabaseKeys.ClimbingSessions)
                return (T)Convert.ChangeType(GetClimbingSessions(), typeof(T));
            else if (key == EDatabaseKeys.Subscriptions)
                return (T)Convert.ChangeType(GetSubscriptions(), typeof(T));

            throw new NotImplementedException($"Data not setup for key: {key}");
        }

        public void Add<T>(T data, EDatabaseKeys key) where T : class
        {
            if (key == EDatabaseKeys.Subscriptions)
                AddSubscription((List<Subscription>)Convert.ChangeType(data, typeof(List<Subscription>)));
        }

        #endregion

        #region Private

        List<ClimbingSessionItem> GetClimbingSessions()
        {
            return new List<ClimbingSessionItem>()
            {
                new ClimbingSessionItem(new DateTime(2021, 8, 10), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 8, 15), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 8, 16), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 8, 19), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 8, 22), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 8, 26), EClimbingType.Length),
                new ClimbingSessionItem(new DateTime(2021, 9, 3), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 4), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 8), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 9), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 13), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 14), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 17), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 21), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 28), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 9, 30), EClimbingType.Length),
                new ClimbingSessionItem(new DateTime(2021, 10, 3), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 10, 3), EClimbingType.Length),
                new ClimbingSessionItem(new DateTime(2021, 10, 7), EClimbingType.Length),
                new ClimbingSessionItem(new DateTime(2021, 10, 10), EClimbingType.Length),
                new ClimbingSessionItem(new DateTime(2021, 10, 10), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 10, 11), EClimbingType.Boulder),
                new ClimbingSessionItem(new DateTime(2021, 10, 14), EClimbingType.Length),
                new ClimbingSessionItem(new DateTime(2021, 10, 17), EClimbingType.Boulder)
            };
        }

        List<Subscription> GetSubscriptions()
        {
            if (_subscriptions == null || _subscriptions.Count == 0)
            {
                _subscriptions = new List<Subscription>()
                {
                    new Subscription(new DateTime(2021, 6, 27), ESubscriptionType.TenTurnCard, 90, false),
                    new Subscription(new DateTime(2021, 8, 22), ESubscriptionType.ThreeMonthSubscription, 130)
                };
            }
            return _subscriptions;
        }

        void AddSubscription(List<Subscription> subscription)
        {
            if (_subscriptions == null || _subscriptions.Count == 0)
                return;

            _subscriptions = subscription;
        }

        #endregion
    }
}
