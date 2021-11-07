namespace Climber.Forms.Core
{
    public class SubscriptionDetailResult
    {
        #region Properties

        public bool IsSuccess { get; }

        public bool IsUpdate { get; }

        #endregion

        #region Constructor

        public SubscriptionDetailResult(bool isSuccess, bool isUpdate)
        {
            IsSuccess = isSuccess;
            IsUpdate = isUpdate;
        }

        #endregion
    }
}