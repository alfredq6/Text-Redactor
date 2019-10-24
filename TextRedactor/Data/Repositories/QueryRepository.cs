using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Models;

namespace TextRedactor.Data.Repositories
{
    public class QueryRepository : BaseRepository<Query>
    {
        public QueryRepository() : base() { }
        
        public IEnumerable<Query> GetByUserId(long userId)
        {
            List<Query> queries = new List<Query>();
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select Id, UserId, datetime(Time) from Queries where UserId = {userId}";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    queries.Add(new Query() { Id = reader.GetFieldValue<long>(0),
                        UserId = userId,
                        Time = Convert.ToDateTime(reader.GetFieldValue<string>(2)) });
                }
            });
            return queries;
        }

        public Query Insert(long userId)
        {
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"insert into Queries (UserId, Time) values ({userId}, '{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}')";
                command.ExecuteNonQuery();
            });
            return GetByUserId(userId).Last();
        }
    }
}
