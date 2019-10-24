using Google.Apis.Auth.OAuth2;
using Google.Cloud.Translation.V2;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextRedactor.Buiseness.JSONModels;

namespace TextRedactor.Buiseness
{
    public class Detector
    {
        public List<LanguagesInfo> GetDetectedLanguages(Detection detection)
        {
            var list = new List<LanguagesInfo>();
            list.Add(new LanguagesInfo() { language = "English", confidence = ConfidenceOfLanguage(detection, "en") });
            list.Add(new LanguagesInfo() { language = "Spanish", confidence = ConfidenceOfLanguage(detection, "es") });
            list.Add(new LanguagesInfo() { language = "Portuguese", confidence = ConfidenceOfLanguage(detection, "pt") });
            list.Add(new LanguagesInfo() { language = "Bulgarian", confidence = ConfidenceOfLanguage(detection, "bg") });
            list.Add(new LanguagesInfo() { language = "Russian", confidence = ConfidenceOfLanguage(detection, "ru") });
            return list;
        }

        public List<DetectionInfo> GetDetectedWords(IList<Detection> detections)
        {
            var list = new List<DetectionInfo>();
            foreach (var detection in detections)
            {
                list.Add(new DetectionInfo() { text = detection.Text, languages = GetDetectedLanguages(detection) });
            }
            return list;
        }

        public async Task<IList<Detection>> DetectLanguages(IEnumerable<string> words)
        {
            TranslationClient client = await TranslationClient.CreateAsync();
            var detections = await client.DetectLanguagesAsync(words);
            return detections;
        }

        public float ConfidenceOfLanguage(Detection detection, string language)
        {
            switch (detection.Language)
            {
                case "en":
                    {
                        return language == "en" ? detection.Confidence * 100 : 0;
                    }
                case "es":
                    {
                        return language == "es" ? detection.Confidence * 100 : 0;
                    }
                case "pt":
                    {
                        return language == "pt" ? detection.Confidence * 100 : 0;
                    }
                case "ru":
                    {
                        return language == "ru" ? detection.Confidence * 100 : 0;
                    }
                case "bg":
                    {
                        return language == "bg" ? detection.Confidence * 100 : 0;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
    }
}
