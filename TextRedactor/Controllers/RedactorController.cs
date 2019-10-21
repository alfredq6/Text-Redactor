using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using RestSharp.Authenticators;
using Newtonsoft.Json;
using TextRedactor.Buiseness;
using TextRedactor.Data.Models;

namespace TextRedactor.Controllers
{
    public class RedactorController : Controller
    {
        public static User CurrentUser { get; private set; }

        [HttpGet]
        public IActionResult TextRedact(User user)
        {
            if (user.Id != 0)
            {
                CurrentUser = user;
                return View();
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }
        }

        [HttpPost]
        public IActionResult DetectLanguage(string text)
        {
            if (CurrentUser != null)
            {
                var detector = new Detector();
                IRestResponse response = detector.SendRequestToDetect(text);
                var result = JsonConvert.DeserializeObject<DetectionResult>(response.Content);
                DetectionInfo detection = result.data.detections[0];
                return Json(detector.GetDetectedLanguages(detection));
            }
            else
            {
                return RedirectToAction("SignIn", "Account");
            }
        }
    }
}