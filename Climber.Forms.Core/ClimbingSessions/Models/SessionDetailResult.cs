namespace Climber.Forms.Core
{
    public class SessionDetailResult
    {
        #region Properties

        public bool IsSuccess { get; }

        public ECrud Action { get; }

        #endregion

        #region Constructor

        public SessionDetailResult(bool isSuccess, ECrud action)
        {
            IsSuccess = isSuccess;
            Action = action;
        }

        #endregion
    }
}