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
        public UserRepository() : base() { }

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
            if (isExist)
                InsertLoginTime(Get(name).Id);
            return isExist;
        }

        public User Get(string name = null, long id = 0)
        {
            User user = null;
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = name != null ? $"select * from Users where Name = '{name}'" : $"select * from Users where Id = '{id}'";
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User()
                    {
                        Id = reader.GetFieldValue<long>(0),
                        Name = reader.GetFieldValue<string>(1),
                        LastTimeLoginId = reader.GetValue(3) is DBNull ? 0 : (long)reader.GetValue(3)
                    };
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
            var user = Get(name);
            InsertLoginTime(user.Id);
            return user;
        }

        private void InsertLoginTime(long userId)
        {
            ExecuteCommandInConnection((command) => {
                command.CommandText = $"insert into LoginTimeLog (UserId, LastTimeLogin) values ({userId}, '{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}')";
                command.ExecuteNonQuery();
            });
        }
    }
}
