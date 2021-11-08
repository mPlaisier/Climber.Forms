namespace Climber.Forms.Core
{
    public class EquipmentDetailResult
    {
        #region Properties

        public bool IsSuccess { get; }

        public ECrud Action { get; }

        #endregion

        #region Constructor

        public EquipmentDetailResult(bool isSuccess, ECrud action)
        {
            IsSuccess = isSuccess;
            Action = action;
        }

        #endregion
    }
}