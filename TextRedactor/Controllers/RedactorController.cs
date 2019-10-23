using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextRedactor.Buiseness;
using TextRedactor.Buiseness.JSONModels;
using TextRedactor.Data.Models;
using TextRedactor.Data.Repositories;

namespace TextRedactor.Controllers
{
    public class RedactorController : Controller
    {
        private readonly QueryRepository queryRepository;
        private readonly DetectedWordsRepository detectedWordsRepository;
        private readonly TopRequestsRepository topRequestsRepository;
        public RedactorController(BaseRepository<Query> _queryRepository, BaseRepository<DetectedWord> _detectedWordsRepository, BaseRepository<TopRequests> _topRequestsRepository)
        {
            queryRepository = (QueryRepository)_queryRepository;
            detectedWordsRepository = (DetectedWordsRepository)_detectedWordsRepository;
            topRequestsRepository = (TopRequestsRepository)_topRequestsRepository;
        }

        public static User CurrentUser { get; private set; }

        [HttpGet]
        public IActionResult TextRedact(User user)
        {
            if (user.Id != 0)
            {
                CurrentUser = user;
                return View();
            }
            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        public async Task<IActionResult> DetectLanguages(IEnumerable<string> words)
        {
            if (CurrentUser != null)
            {
                var detector = new Detector();
                var detections = detector.GetDetectedWords(await detector.DetectLanguages(words));
                var query = queryRepository.Insert(CurrentUser.Id);
                foreach (var el in detections)
                {
                    var languageInfo = el.languages.Where(x => x.confidence != 0).FirstOrDefault() ?? new LanguagesInfo();
                    detectedWordsRepository.Insert(CurrentUser.Id, query.Id, el.text, languageInfo.language, languageInfo.confidence);
                }
                return Json(detections);
            }
            return RedirectToAction("SignIn", "Account");
        }

        [HttpPost]
        public IActionResult UpdateTopRequestsTable()
        {
            if (CurrentUser != null)
            {
                return Json(topRequestsRepository.GetAll("TopRequests"));
            }
            return RedirectToAction("SignIn", "Account");
        }
    }
}