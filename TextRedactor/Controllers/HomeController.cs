using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextRedactor.Models;
using System.Data.SQLite;

namespace TextRedactor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            SQLiteConnection connection = new SQLiteConnection(@"DataSource=C:/sqlite/TextRedactorDB.db");
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = "select * from Users";
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

            }
            reader.Close();
            connection.Close();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
