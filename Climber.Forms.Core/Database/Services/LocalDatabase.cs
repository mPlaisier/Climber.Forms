using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SQLite;

namespace Climber.Forms.Core
{
    public class LocalDatabase : IDatabaseService
    {
        readonly SQLiteAsyncConnection _database;

        #region Constructor

        public LocalDatabase()
        {
            _database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags, false);
        }

        #endregion

        #region Public

        public async Task<List<T>> GetListAsync<T>() where T : class, new()
        {
            await CheckTable<T>();

            var data = await _database.Table<T>().ToListAsync();
            return data;
        }

        public async Task<T> GetAsync<T>(string id) where T : class, IWithId, new()
        {
            await CheckTable<T>();

            // Get a specific note.
            var data = await _database.Table<T>()
                            .Where(i => i.Id.Equals(id))
                            .FirstOrDefaultAsync();

            return data;
        }

        public async Task<bool> SaveAsyn<T>(T data) where T : class, IWithId, new()
        {
            await CheckTable<T>();

            //Create
            if (data.Id == 0)
            {
                var updated = await _database.InsertAsync(data);
                return updated == 1;
            }
            else //Update
            {
                var updated = await _database.UpdateAsync(data);
                return updated == 1;
            }
        }

        public async Task<bool> DeleteAsync<T>(T data) where T : class, new()
        {
            await CheckTable<T>();

            var updated = await _database.DeleteAsync(data);
            return updated == 1;
        }

        #endregion

        #region Private

        async Task CheckTable<T>()
        {
            if (!_database.TableMappings.Any(x => x.MappedType == typeof(T)))
            {
                await _database.EnableWriteAheadLoggingAsync().ConfigureAwait(false);
                await _database.CreateTablesAsync(CreateFlags.None, typeof(T)).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
