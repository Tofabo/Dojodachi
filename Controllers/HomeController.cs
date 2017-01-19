using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace dojodachi.Controllers
{
    public class HomeController : Controller
    {
        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            HttpContext.Session.SetInt32("Fullness", 20);
            HttpContext.Session.SetInt32("Happiness", 20);
            HttpContext.Session.SetInt32("Meals", 3);
            HttpContext.Session.SetInt32("Energy", 50);
            HttpContext.Session.SetString("Message","This is Gudetama, take good care of him, ok?");
            HttpContext.Session.SetString("Picture","/images/gudetama.png");
            HttpContext.Session.SetInt32("Win", 0);
            ViewBag.Win = HttpContext.Session.GetInt32("Win");
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Message = HttpContext.Session.GetString("Message");
            ViewBag.Pic =  HttpContext.Session.GetString("Picture");
            return View("Index");
        }

        [HttpPost]
        [Route("action")]
        public IActionResult Action(string action)
        {   Random rnd = new Random();
            if(action == "feed" || action == "play"){
                int rand = rnd.Next(1, 5);
                if( rand == 1 && action == "feed"){
                    HttpContext.Session.SetInt32("Meals", HttpContext.Session.GetInt32("Meals").Value-1);
                    HttpContext.Session.SetString("Message",$"Gudetama doesn't like it..... Meals: -1");
                    HttpContext.Session.SetString("Picture","/images/gude_no.png");
                }
                if( rand == 1 && action == "play"){
                    HttpContext.Session.SetInt32("Energy", HttpContext.Session.GetInt32("Energy").Value-5);
                    HttpContext.Session.SetString("Message",$"Gudetama doesn't like it..... Energy: -5");
                    HttpContext.Session.SetString("Picture","/images/gude_no.png");
                }
                if(rand > 1 && action == "feed"){
                int rand1 = rnd.Next(5,11);
                HttpContext.Session.SetInt32("Meals", HttpContext.Session.GetInt32("Meals").Value-1);
                HttpContext.Session.SetInt32("Fullness", HttpContext.Session.GetInt32("Fullness").Value + rand1);
                HttpContext.Session.SetString("Message",$"This food is ok... Fullness: +{rand1} Meals: -1");
                HttpContext.Session.SetString("Picture","/images/gude_eat.png");
                }
                if(rand > 1 && action == "play"){
                int rand1 = rnd.Next(5,11);
                HttpContext.Session.SetInt32("Energy", HttpContext.Session.GetInt32("Energy").Value-5);
                HttpContext.Session.SetInt32("Happiness", HttpContext.Session.GetInt32("Happiness").Value + rand1);
                HttpContext.Session.SetString("Message",$"Staaaaph! Happiness: +{rand1} Energy: -5");
                HttpContext.Session.SetString("Picture","/images/gudetama_play2.jpg");
            }
            }
            
            if(action == "work"){
                int rand = rnd.Next(1,4);
                HttpContext.Session.SetInt32("Energy", HttpContext.Session.GetInt32("Energy").Value-5);
                HttpContext.Session.SetInt32("Meals", HttpContext.Session.GetInt32("Meals").Value + rand);
                HttpContext.Session.SetString("Message",$"I hate work. Meals: +{rand} Energy: -5");
                HttpContext.Session.SetString("Picture","/images/work_gude.jpg");
            }
            if(action == "sleep"){
                HttpContext.Session.SetInt32("Energy", HttpContext.Session.GetInt32("Energy").Value+15);
                HttpContext.Session.SetInt32("Fullness", HttpContext.Session.GetInt32("Fullness").Value - 5);
                HttpContext.Session.SetInt32("Happiness", HttpContext.Session.GetInt32("Happiness").Value - 5);
                HttpContext.Session.SetString("Message","My favorite. Energy: +15 Happiness: -5 Fullness: -5");
                HttpContext.Session.SetString("Picture","/images/gudetama_sleep.png");
            }
            if(HttpContext.Session.GetInt32("Happiness").Value >= 100 && HttpContext.Session.GetInt32("Fullness").Value >=100){
                HttpContext.Session.SetString("Message","Gudetama is happy! Wow! You win!");
                HttpContext.Session.SetString("Picture","/images/gudetama.png");
                 HttpContext.Session.SetInt32("Win", 1);
            }
            if(HttpContext.Session.GetInt32("Happiness").Value <=0 || HttpContext.Session.GetInt32("Fullness").Value <=0){
                HttpContext.Session.SetString("Message","How could you?!\n Gudetama rolls away...");
                HttpContext.Session.SetString("Picture","/images/sad_gude_1.jpg");
                 HttpContext.Session.SetInt32("Win", 1);
            }
            ViewBag.Win = HttpContext.Session.GetInt32("Win");
            ViewBag.Fullness = HttpContext.Session.GetInt32("Fullness");
            ViewBag.Happiness = HttpContext.Session.GetInt32("Happiness");
            ViewBag.Meals = HttpContext.Session.GetInt32("Meals");
            ViewBag.Energy = HttpContext.Session.GetInt32("Energy");
            ViewBag.Message = HttpContext.Session.GetString("Message");
            ViewBag.Pic =  HttpContext.Session.GetString("Picture");
            return View("Index");
        }

        //if meals are zero, you can't feed. also work

        [HttpPost]
        [Route("reset")]
        public IActionResult Reset()
        {   
            HttpContext.Session.Clear();
            return RedirectToAction("Index"); 
        }
    }
}
