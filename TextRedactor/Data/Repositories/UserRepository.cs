using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Models;

namespace TextRedactor.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository() : base()
        {
            
        }

        public bool CheckUserSignIn(string name, string password)
        {
            bool isExist = false;
            OpenCloseConnection(() =>
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = $"select * from Users where Name = '{name}' and Password = '{password}'";
                var reader = command.ExecuteReader();
                isExist = reader.HasRows;
            });
            return isExist;
        }

        public bool Create(string name, string password)
        {
            OpenCloseConnection(() =>
            {
                var command = new SQLiteCommand(connection);
                command.CommandText = $"insert into Users(Name, Password) values ('{name}', '{password}')";
                command.ExecuteNonQuery();
            });
            return false;
        }
    }
}
