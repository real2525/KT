using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_MVC.Models;

namespace Web_MVC.Controllers
{
    public class phoneController : Controller
    {
        private Model1 db = new Model1();
        public string addressIp = "http://localhost:14335"; 
        // GET: Phones
        public async Task<ActionResult> Index()
        {
            IEnumerable<phone> phone = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(addressIp);
                var result = await client.GetAsync($"api/phone");
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    phone = JsonConvert.DeserializeObject<List<phone>>(data);
                }
                else
                {
                    phone = Enumerable.Empty<phone>();
                    ModelState.AddModelError(string.Empty, "Server error");
                }

            }
            return View(phone);
        }

        // GET: Phones/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phone phone = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(addressIp);
                var result = await client.GetAsync("api/phone/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    phone = JsonConvert.DeserializeObject<phone>(data);
                }
            }
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // GET: Phones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Phones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,model,price,general_note")] phone phone)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(phone);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(addressIp);
                    var result = await client.PostAsync("api/phone", content);
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "chán");
                    }
                }
            }

            return View(phone);
        }

        // GET: Phones/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {

                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phone phone = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(addressIp);
                var result = await client.GetAsync("api/phone/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    phone = JsonConvert.DeserializeObject<phone>(data);
                }
            }
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,model,price,general_note")] phone phone)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    string data = JsonConvert.SerializeObject(phone);
                    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                    client.BaseAddress = new Uri(addressIp);
                    var result = await client.PutAsync("api/phone/" + phone.id, content);
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Chán");
                    }
                }
            }
            return View(phone);
        }

        // GET: Phones/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            phone phone = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(addressIp);
                var result = await client.GetAsync("api/phone/" + id);
                if (result.IsSuccessStatusCode)
                {
                    string data = result.Content.ReadAsStringAsync().Result;
                    phone = JsonConvert.DeserializeObject<phone>(data);
                }
            }
            if (phone == null)
            {
                return HttpNotFound();
            }
            return View(phone);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(addressIp);
                await client.DeleteAsync("api/phone/" + id);

            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
