using System;

namespace Climber.Forms.Core
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}