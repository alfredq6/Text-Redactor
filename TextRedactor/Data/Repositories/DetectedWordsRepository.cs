using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Data.Models;

namespace TextRedactor.Data.Repositories
{
    public class DetectedWordsRepository : BaseRepository<DetectedWord>
    {
        public DetectedWordsRepository() : base() { }

        public DetectedWord Insert(long userId, long queryId, string word, string language, float confidence)
        {
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"insert into DetectedWords (UserId, QueryId, Word, Language, Confidence) values ({userId}, {queryId}, '{word}', '{language}', {confidence})";
                command.ExecuteNonQuery();
            });
            return GetByUserId(userId).Last();
        }

        public IEnumerable<DetectedWord> GetByUserId(long id)
        {
            List<DetectedWord> detectedWords = new List<DetectedWord>();
            ExecuteCommandInConnection((command) =>
            {
                command.CommandText = $"select * from DetectedWords where UserId = {id}";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    detectedWords.Add(new DetectedWord() { Id = reader.GetFieldValue<long>(0),
                        UserId = id,
                        QueryId = reader.GetFieldValue<long>(2),
                        Word = reader.GetFieldValue<string>(3),
                        Language = reader.GetFieldValue<string>(4),
                        Confidence = reader.GetFieldValue<double>(5)
                    });
                }
            });
            return detectedWords;
        }
    }
}
