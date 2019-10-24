using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Models;

namespace TextRedactor.Data.Repositories
{
    public class LoginTimeLogRepository : BaseRepository<LoginTimeLog>
    {
        public LoginTimeLogRepository() : base() { }

        public IEnumerable<LoginTimeLog> GetByUserId(long userId)
        {
            List<LoginTimeLog> loginTimeLogs = new List<LoginTimeLog>();
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select * from LoginTimeLog where UserId = {userId}";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    loginTimeLogs.Add(new LoginTimeLog()
                    {
                        Id = reader.GetFieldValue<long>(0),
                        UserId = userId,
                        LastTimeLogin = reader.GetFieldValue<DateTime>(2)
                    });
                }
            });
            return loginTimeLogs;
        }
    }
}
