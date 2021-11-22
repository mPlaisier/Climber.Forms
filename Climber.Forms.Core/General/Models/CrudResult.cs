namespace Climber.Forms.Core
{
    public class CrudResult
    {
        #region Properties

        public bool IsSuccess { get; }

        public ECrud Action { get; }

        #endregion

        #region Constructor

        public CrudResult(ECrud action) : this(action, true)
        {
        }

        public CrudResult(ECrud action, bool isSuccess)
        {
            IsSuccess = isSuccess;
            Action = action;
        }

        #endregion
    }
}