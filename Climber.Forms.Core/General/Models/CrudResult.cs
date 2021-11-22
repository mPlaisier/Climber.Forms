﻿namespace Climber.Forms.Core
{
    public class CrudResult
    {
        #region Properties

        public bool IsSuccess { get; }

        public ECrud Action { get; }

        #endregion

        #region Constructor

        public CrudResult(ECrud action, bool isSuccess = true)
        {
            IsSuccess = isSuccess;
            Action = action;
        }

        #endregion
    }
}
