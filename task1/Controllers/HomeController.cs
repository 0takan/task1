using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapper;
using Microsoft.Extensions.Configuration;
using task1.Models;
using Microsoft.Data.SqlClient;
namespace task1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;


            _config = config;
        }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public IActionResult Index()
        {
            var item = GetAllUsers();


            return View(item);

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
        private List<User> GetAllUsers()
        {
            using (IDbConnection db = Connection)
            {
                List<User> result = db.Query<User>("SELECT * FROM Users").ToList();

                return result;
            }
        }
        public class User
        {
            public int Id { get; set; }

            public string FirstName { get; set; }
           
            public string SecondName { get; set; }
           
            public double Balance { get; set; }

            public DateTime Created { get; set; }
        }
    }

}
