using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using TextRedactor.Buiseness;

namespace TextRedactor.Controllers
{
    public class RedactorController : Controller
    {
        [HttpGet]
        public IActionResult TextRedact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DetectLanguage(string text)
        {
            var detector = new Detector();
            IRestResponse response = detector.SendRequestToDetect(text);
            var result = JsonConvert.DeserializeObject<DetectionResult>(response.Content);
            DetectionInfo detection = result.data.detections[0];
            return Json(detector.GetDetectedLanguages(detection));
        }
    }
}