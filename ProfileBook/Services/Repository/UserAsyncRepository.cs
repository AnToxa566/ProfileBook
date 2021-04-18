using ProfileBook.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProfileBook.Services.Repository
{
    public class UserAsyncRepository : IUserRepository
    {
        private Lazy<SQLiteAsyncConnection> _database;

        public UserAsyncRepository()
        {
            _database = new Lazy<SQLiteAsyncConnection>(() =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "user.db");
                var database = new SQLiteAsyncConnection(path);

                database.CreateTableAsync<User>();

                return database;
            });
        }

        #region ---Public Methods---

        public Task<List<T>> GetAllAsync<T>() where T : IEntityBase, new()
        {
            return _database.Value.Table<T>().ToListAsync();
        }

        public Task<int> InsertAsync<T>(T entity) where T : IEntityBase, new()
        {
            return _database.Value.InsertAsync(entity);
        }

        #endregion

    }
}
