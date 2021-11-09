namespace Climber.Forms.Core
{
    public class SubscriptionDetailResult
    {
        #region Properties

        public bool IsSuccess { get; }

        public ECrud Action { get; }

        #endregion

        #region Constructor

        public SubscriptionDetailResult(bool isSuccess, ECrud action)
        {
            IsSuccess = isSuccess;
            Action = action;
        }

        #endregion
    }
}