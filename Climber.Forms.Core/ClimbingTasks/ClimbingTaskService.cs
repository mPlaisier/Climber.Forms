using System;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public class ClimbingTaskService : IClimbingTaskService
    {
        readonly IMessageService _messageService;

        #region Constructor

        public ClimbingTaskService(IMessageService messageService)
        {
            _messageService = messageService;
        }

        #endregion

        #region Public

        public async Task Execute(Func<Task> func)
        {
            try
            {
                await func.Invoke();
            }
            catch (Exception ex)
            {
                _messageService.ShowErrorMessage(Labels.LblError, ex.Message);
            }
        }

        public async Task ExecuteDelete(Func<Task> func)
        {
            var delete = await _messageService.AskDeleteConfirmation();
            if (delete)
            {
                await Execute(func);
            }
        }

        #endregion
    }
}
