using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
            connection = new SQLiteConnection(@"DataSource=C:/sqlite/TextRedactorDB.db");
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

        public IEnumerable<TModel> GetAll(string modelName)
        {
            List<TModel> table = new List<TModel>();
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select * from {modelName}";
                SQLiteDataReader reader = command.ExecuteReader();
                table = reader.Cast<TModel>().ToList();
                reader.Close();
            });
            return table;
        }
    }
}
