using static Climber.Forms.Core.Enums;

namespace Climber.Forms.Core
{
    public interface IDatabaseService
    {
        #region Methods

        T Get<T>(EDatabaseKeys key) where T : class;
        void Add<T>(T data, EDatabaseKeys key) where T : class;
        void Update<T>(T data, EDatabaseKeys key) where T : class;

        #endregion
    }
}
