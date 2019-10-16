﻿using System;
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

        protected void OpenCloseConnection(Action action)
        {
            connection.Open();
            action?.Invoke();
            connection.Close();
        }

        public IEnumerable<TModel> GetAll(string name)
        {
            List<TModel> table = null;
            OpenCloseConnection(() => {
                using (var command = new SQLiteCommand(connection))
                {
                    command.CommandText = $"select * from {name}";
                    SQLiteDataReader reader = command.ExecuteReader();
                    table = reader.Cast<TModel>().ToList();
                    reader.Close();
                }
            });
            return table;
        }
    }
}