using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Model;
using Newtonsoft.Json;

namespace Assessment.Controllers
{
    public class AmigoController : Controller
    {
        // GET: AmigoController
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Amigo> amigoList = new List<Amigo>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:44359/api/amigos"))
                {
                    string apiResponse = await
                   response.Content.ReadAsStringAsync();

                    
                    amigoList = JsonConvert.DeserializeObject<List<Amigo>>(apiResponse);
                }
            }

            return View(amigoList);
        }

        // GET: AmigoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var amigo = new Amigo();
            var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:44359/api/amigos/" + id);
            if (response.IsSuccessStatusCode)
            {
                var resultado = response.Content.ReadAsStringAsync().Result;
                amigo = JsonConvert.DeserializeObject<Amigo>(resultado);
            }
            return View(amigo);
        }

        // GET: AmigoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AmigoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Amigo friend)
        {
            Amigo amigo = new Amigo();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(friend), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:44359/api/amigos/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    amigo = JsonConvert.DeserializeObject<Amigo>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: AmigoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AmigoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AmigoController/Delete/5
        public async Task<HttpStatusCode> Delete(int id)
        {
            var cliente = new HttpClient();
            HttpResponseMessage response = await cliente.DeleteAsync("http://localhost:44359/api/amigos/"+ id);
            var code = response.StatusCode.ToString();
           if (code == "204")
            {
                RedirectToAction("Index");
                return response.StatusCode;
               
            }
            return response.StatusCode;

            
        }

        // POST: AmigoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
