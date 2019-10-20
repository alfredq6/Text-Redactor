using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextRedactor.Buiseness
{
    public class Detector
    {
        public RestClient RestClient { get; private set; }

        public Detector()
        {
            InitializeRestClient();
        }

        public List<DetectionInfo> GetDetectedLanguages(DetectionInfo detection)
        {
            var list = new List<DetectionInfo>();
            list.Add(new DetectionInfo() { language = "English", confidence = ConfidenceOfLanguage(detection, "en") });
            list.Add(new DetectionInfo() { language = "Spanish", confidence = ConfidenceOfLanguage(detection, "es") });
            list.Add(new DetectionInfo() { language = "Portuguese", confidence = ConfidenceOfLanguage(detection, "pt") });
            list.Add(new DetectionInfo() { language = "Bulgarian", confidence = ConfidenceOfLanguage(detection, "bg") });
            list.Add(new DetectionInfo() { language = "Russian", confidence = ConfidenceOfLanguage(detection, "ru") });
            return list;
        }

        public float ConfidenceOfLanguage(DetectionInfo detection, string language)
        {

            switch (detection.language)
            {
                case "en":
                    {
                        return language == "en" ? detection.confidence * 10 : 0;
                    }
                case "es":
                    {
                        return language == "es" ? detection.confidence * 10 : 0;
                    }
                case "pt":
                    {
                        return language == "pt" ? detection.confidence * 10 : 0;
                    }
                case "ru":
                    {
                        return language == "ru" ? detection.confidence * 10 : 0;
                    }
                case "bg":
                    {
                        return language == "bg" ? detection.confidence * 10 : 0;
                    }
                default:
                    {
                        return 0;
                    }
            }
        }
        
        private void InitializeRestClient()
        {
            RestClient = new RestClient("http://ws.detectlanguage.com");
            RestClient.Authenticator = new HttpBasicAuthenticator("c37188593096b01ad94173d7061e5bbb", "");
        }

        public IRestResponse SendRequestToDetect(string text)
        {
            var request = new RestRequest("/0.2/detect", Method.POST);
            request.AddParameter("q", text);
            return RestClient.Execute(request);
        }
    }
}
