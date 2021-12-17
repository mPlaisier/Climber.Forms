using System;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IClimbingTaskService
    {
        Task Execute(Func<Task> func);
        Task ExecuteDelete(Func<Task> func);
    }
}