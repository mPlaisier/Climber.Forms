using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class DummyDatabase : IDatabaseService
    {
        List<DbSubscription> _subscriptions;
        List<DbEquipment> _equipment;

        #region Public

        public Task<List<T>> GetListAsync<T>() where T : class, new()
        {
            if (typeof(T) == typeof(DbSubscription))
                return Task.FromResult((List<T>)Convert.ChangeType(GetSubscriptions(), typeof(List<T>)));
            else if (typeof(T) == typeof(ClimbingSessionItem))
                return Task.FromResult((List<T>)Convert.ChangeType(GetClimbingSessions(), typeof(List<T>)));
            else if (typeof(T) == typeof(DbEquipment))
                return Task.FromResult((List<T>)Convert.ChangeType(GetClimbingEquipment(), typeof(List<T>)));

            throw new NotImplementedException($"Data not setup for {typeof(T)}");
        }

        public Task<T> GetAsync<T>(string id) where T : class, IWithId, new()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync<T>(T data) where T : class, IWithId, new()
        {
            if (typeof(T) == typeof(DbSubscription))
            {
                if (data.Id == 0)
                {
                    AddSubscription((DbSubscription)Convert.ChangeType(data, typeof(DbSubscription)));
                    return Task.FromResult(true);
                }
                else //Update
                {
                    UpdateSubscription((DbSubscription)Convert.ChangeType(data, typeof(DbSubscription)));
                    return Task.FromResult(true);
                }
            }

            if (typeof(T) == typeof(DbEquipment))
            {
                //Create
                if (data.Id == 0)
                {
                    AddEquipment((DbEquipment)Convert.ChangeType(data, typeof(DbEquipment)));
                    return Task.FromResult(true);
                }
            }

            throw new NotImplementedException($"Data not setup for {typeof(T)}");
        }

        public Task<bool> DeleteAsync<T>(T data) where T : class, new()
        {
            if (typeof(T) == typeof(DbSubscription))
            {
                DeleteSubscription((DbSubscription)Convert.ChangeType(data, typeof(DbSubscription)));
                return Task.FromResult(true);
            }

            throw new NotImplementedException($"Data not setup for {typeof(T)}");
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

        List<DbSubscription> GetSubscriptions()
        {
            if (_subscriptions == null || _subscriptions.Count == 0)
            {
                _subscriptions = new List<DbSubscription>()
                {
                    new DbSubscription(1, new DateTime(2021, 6, 27), ESubscriptionType.TenTurnCard, 90, false),
                    new DbSubscription(2, new DateTime(2021, 8, 22), ESubscriptionType.ThreeMonthSubscription, 130, true)
                };
            }
            return _subscriptions;
        }

        void AddSubscription(DbSubscription subscription)
        {
            if (_subscriptions == null || _subscriptions.Count == 0)
                return;

            subscription.Id = _subscriptions.Count + 1;

            _subscriptions.Add(subscription);
        }

        void UpdateSubscription(DbSubscription subscription)
        {
            var index = _subscriptions.FindIndex(x => x.Id.Equals(subscription.Id));

            if (index != -1)
                _subscriptions[index] = subscription;
            else
                throw new ArgumentException("Update failed for Subscription. Id not found in database.");
        }

        void DeleteSubscription(DbSubscription subscription)
        {
            var item = _subscriptions.Find(x => x.Id.Equals(subscription.Id));
            _subscriptions.Remove(item);
        }

        List<DbEquipment> GetClimbingEquipment()
        {
            if (_equipment == null)
            {
                _equipment = new List<DbEquipment>()
                {
                    new DbEquipment(1, new DateTime(2021, 6,27),"Climbing shoes", 76),
                    new DbEquipment(1, new DateTime(2021, 9, 3),"Zekeringtoestel", 90),
                };
            }
            return _equipment;
        }

        void AddEquipment(DbEquipment equipment)
        {
            if (_equipment == null)
                return;

            equipment.Id = _equipment.Count + 1;

            _equipment.Add(equipment);
        }

        #endregion
    }
}
