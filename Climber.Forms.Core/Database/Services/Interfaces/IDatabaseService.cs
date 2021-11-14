using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Climber.Forms.Core
{
    public interface IDatabaseService
    {
        #region Methods

        Task<List<T>> GetListAsync<T>() where T : class, new();
        Task<List<T>> GetListAsync<T>(Expression<Func<T, bool>> expression) where T : class, new();

        Task<T> GetAsync<T>(int id) where T : class, IWithId, new();

        Task<bool> SaveAsync<T>(T data) where T : class, IWithId, new();

        Task<bool> DeleteAsync<T>(T data) where T : class, new();

        #endregion
    }
}