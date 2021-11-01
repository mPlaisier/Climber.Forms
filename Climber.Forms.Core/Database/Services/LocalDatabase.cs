using System;
using MonkeyCache.LiteDB;
using static Climber.Forms.Core.Enums;

namespace Climber.Forms.Core
{
    public class LocalDatabase : IDatabaseService
    {
        const string KEY_CLIMBING_SESSIONS = nameof(KEY_CLIMBING_SESSIONS);
        const string KEY_SUBSCRIPTIONS = nameof(KEY_SUBSCRIPTIONS);

        #region Constructor

        public LocalDatabase()
        {
            Barrel.ApplicationId = "Climber.Forms";
        }

        #endregion

        #region Public

        public T Get<T>(EDatabaseKeys key) where T : class
        {
            var keyValue = GetKey(key);

            if (Barrel.Current.Exists(keyValue))
                return Barrel.Current.Get<T>(keyValue);
            else
                return default;
        }

        public void Add<T>(T data, EDatabaseKeys key) where T : class
        {
            var keyValue = GetKey(key);

            Barrel.Current.Add(keyValue, data, TimeSpan.MaxValue);
        }

        #endregion

        #region Private

        string GetKey(EDatabaseKeys key)
        {
            return key switch
            {
                EDatabaseKeys.ClimbingSessions => KEY_CLIMBING_SESSIONS,
                EDatabaseKeys.Subscriptions => KEY_SUBSCRIPTIONS,
                _ => throw new ArgumentException($"Key value not setup for {key}"),
            };
        }

        #endregion
    }
}
