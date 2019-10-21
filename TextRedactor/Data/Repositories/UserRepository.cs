using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Buiseness;
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
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select * from Users where Name = '{name}'";
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        isExist = Crypto.VerifyHashedPassword(reader.GetFieldValue<string>(2), password);
                    }
            });
            return isExist;
        }

        public User GetByName(string name)
        {
            User user = null;
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select * from Users where Name = '{name}'";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User() { Id = reader.GetFieldValue<long>(0), Name = reader.GetFieldValue<string>(1) };
                }
                reader.Close();
            });
            return user;
        }

        public User Create(string name, string password)
        {
            ExecuteCommandInConnection((command) =>
            {
                password = Crypto.HashPassword(password);
                command.CommandText = $"insert into Users(Name, Password) values ('{name}', '{password}')";
                command.ExecuteNonQuery();
            });
            return GetByName(name);
        }
    }
}
