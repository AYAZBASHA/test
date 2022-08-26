using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MRI.API.EmployeeManagement.Controllers
{
    public class WebRequestController : Controller
    {
        // GET: WebRequestController
        public ActionResult Index()
        {
            GetAPIData();
            return View();
        }

        // GET: WebRequestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WebRequestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebRequestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: WebRequestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WebRequestController/Edit/5
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

        // GET: WebRequestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebRequestController/Delete/5
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

        public async Task<string> GetAPIData()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            string apiURL = "https://appstudio.azure-api.net/pet/store/inventory";
            HttpResponseMessage response = client.GetAsync(apiURL).Result;
            if (response.IsSuccessStatusCode)
            {
                
            }
            return "";// Json("{my data}");
        }
    }
}
