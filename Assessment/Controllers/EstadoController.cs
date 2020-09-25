using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;

namespace Assessment.Controllers
{
    public class EstadoController : Controller
    {
        // GET: EstadoController
        public async Task<IActionResult> Index()
        {
            List<Estado> estadoList = new List<Estado>();

            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("http://localhost:52858/api/estados"))
                {
                    string apiResponse = await
                   response.Content.ReadAsStringAsync();


                    estadoList = JsonConvert.DeserializeObject<List<Estado>>(apiResponse);
                }
            }

            return View(estadoList);
        }

        // GET: EstadoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Estado state)
        {
            Estado estado = new Estado();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(state), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("http://localhost:52858/api/estados/", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    estado = JsonConvert.DeserializeObject<Estado>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: EstadoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadoController/Edit/5
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

        // GET: EstadoController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = new HttpClient();
            HttpResponseMessage response = await cliente.DeleteAsync("http://localhost:52858/api/estados" + id);
            var code = response.StatusCode.ToString();
            if (code == "204")
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");


        }

        // POST: EstadoController/Delete/5
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
