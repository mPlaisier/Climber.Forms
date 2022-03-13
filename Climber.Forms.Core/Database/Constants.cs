using System;
using System.IO;

namespace Climber.Forms.Core
{
    public static class Constants
    {
        #region Properties

#if DEBUG
        public static string DatabaseFilename => "ClimberFormSQLiteDevelopment.db3";
#else
        public static string DatabaseFilename => "ClimberFormSQLite.db3";
#endif
        public static SQLite.SQLiteOpenFlags Flags =>
            // open the database in read/write mode
            SQLite.SQLiteOpenFlags.ReadWrite |
            // create the database if it doesn't exist
            SQLite.SQLiteOpenFlags.Create |
            // enable multi-threaded database access
            SQLite.SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(basePath, DatabaseFilename);
            }
        }

        #endregion
    }
}