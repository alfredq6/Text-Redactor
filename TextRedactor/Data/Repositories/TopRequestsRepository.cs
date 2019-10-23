using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Models;

namespace TextRedactor.Data.Repositories
{
    public class TopRequestsRepository : BaseRepository<TopRequests>
    {
        public TopRequestsRepository() : base() { }

        public IEnumerable<TopRequests> GetByUserId(long userId)
        {
            List<TopRequests> queries = new List<TopRequests>();
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select * from TopRequests where UserId = {userId}";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    queries.Add(new TopRequests()
                    {
                        Id = reader.GetFieldValue<long>(0),
                        UserId = userId,
                        QueriesCount = reader.GetFieldValue<long>(2),
                        LastTimeLoginId = reader.GetFieldValue<DateTime>(3),
                        AverageTimeBetweenQueries = reader.GetFieldValue<long>(4)
                    });
                }
            });
            return queries;
        }
    }
}
