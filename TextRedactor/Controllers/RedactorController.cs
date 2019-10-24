using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TextRedactor.Buiseness;
using TextRedactor.Buiseness.JSONModels;
using TextRedactor.Data.Models;
using TextRedactor.Data.Repositories;
using TextRedactor.Models;

namespace TextRedactor.Controllers
{
    [Authorize]
    public class RedactorController : Controller
    {
        private readonly QueryRepository queryRepository;
        private readonly DetectedWordsRepository detectedWordsRepository;
        private readonly TopRequestsRepository topRequestsRepository;
        private readonly UserRepository userRepository;
        private readonly LoginTimeLogRepository loginTimeLogRepository;

        public RedactorController(BaseRepository<Query> _queryRepository, 
            BaseRepository<DetectedWord> _detectedWordsRepository, 
            BaseRepository<TopRequests> _topRequestsRepository, 
            BaseRepository<User> _userRepository,
            BaseRepository<LoginTimeLog> _loginTimeLogRepository)
        {
            queryRepository = (QueryRepository)_queryRepository;
            detectedWordsRepository = (DetectedWordsRepository)_detectedWordsRepository;
            topRequestsRepository = (TopRequestsRepository)_topRequestsRepository;
            userRepository = (UserRepository)_userRepository;
            loginTimeLogRepository = (LoginTimeLogRepository)_loginTimeLogRepository;
        }

        [HttpGet]
        public IActionResult Redaction()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DetectLanguages(IEnumerable<string> words)
        {
            if (words.Count() != 0)
            {
                var user = userRepository.Get(name: User.Identity.Name);
                var detector = new Detector();
                var detections = detector.GetDetectedWords(await detector.DetectLanguages(words));
                var query = queryRepository.Insert(user.Id);
                foreach (var el in detections)
                {
                    var languageInfo = el.languages.Where(x => x.confidence != 0).FirstOrDefault() ?? new LanguagesInfo();
                    detectedWordsRepository.Insert(user.Id, query.Id, el.text, languageInfo.language, languageInfo.confidence);
                }
                return Json(detections);
            }
            else
            {
                return Json(new { status = "failed" });
            }
        }

        [HttpPost]
        public IActionResult UpdateTopRequestsTable()
        {
            var listTopRequests = topRequestsRepository.GetAll("TopRequests").OrderByDescending(x => x.QueriesCount).Take(10);
            int i = 1;
            var listTopRequestsInfo = listTopRequests.Select(userRow =>
            {
                var user = userRepository.Get(id: userRow.UserId);
                return new TopRequestsInfo()
                {
                    userName = user.Name,
                    averageTimeBetweenQueriesInMinutes = userRow.AverageTimeBetweenQueries,
                    queriesCount = userRow.QueriesCount,
                    lastLoginTime = loginTimeLogRepository.GetByUserId(user.Id).Last().LastTimeLogin.ToString(),
                    number = i++
                };
            });
            return Json(listTopRequestsInfo);
        }
    }
}