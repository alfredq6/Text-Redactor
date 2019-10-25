using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Interfaces;

namespace TextRedactor.Data.Repositories
{
    public abstract class BaseRepository<TModel> where TModel : IModel
    {
        protected readonly SQLiteConnection connection;
        public BaseRepository()
        {
            connection = new SQLiteConnection($@"DataSource={Path.GetFullPath("TextRedactorDB.db")}");
        }

        protected void ExecuteCommandInConnection(Action<SQLiteCommand> action)
        {
            connection.Open();
            using (var command = new SQLiteCommand(connection))
            {
                action?.Invoke(command);
            }
            connection.Close();
        }
    }
}
