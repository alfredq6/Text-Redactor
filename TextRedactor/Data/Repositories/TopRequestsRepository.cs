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
                        LastTimeLoginId = reader.GetFieldValue<long>(3),
                        AverageTimeBetweenQueries = reader.GetFieldValue<long>(4)
                    });
                }
            });
            return queries;
        }

        public IEnumerable<TopRequests> GetAll(string modelName)
        {
            List<TopRequests> requestsList = new List<TopRequests>();
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select * from TopRequests";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    requestsList.Add(new TopRequests()
                    {
                        Id = reader.GetFieldValue<long>(0),
                        UserId = reader.GetFieldValue<long>(1),
                        QueriesCount = reader.GetValue(2) is DBNull ? 0 : (long)reader.GetValue(2),
                        LastTimeLoginId = reader.GetFieldValue<long>(3),
                        AverageTimeBetweenQueries = reader.GetValue(4) is DBNull ? 0 : (long)reader.GetValue(4)
                    });
                }
                reader.Close();
            });
            return requestsList;
        }
    }
}
