using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SharpFbric.Applacation;
using SharpFbric.Models;

namespace SharpFbric.Controllers
{
    public class HomeController : Controller
    {
        IMySshClient mySsh;
        public HomeController(IMySshClient mySshClient)
        {
            mySsh = mySshClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Ssh()
        {
            mySsh.Connect(); 
            var comandResultList = mySsh.TestCommand();
            mySsh.Disconnect();

            return View(comandResultList);
        }
        public IActionResult Sftp()
        {
            mySsh.Connect();
            mySsh.TestSendFile();
            
            mySsh.Disconnect();

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
