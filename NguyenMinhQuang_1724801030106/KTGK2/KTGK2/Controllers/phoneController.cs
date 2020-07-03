using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using KTGK2.Models;
using System.Threading.Tasks;

namespace KTGK2.Controllers
{
    public class phoneController : ApiController
    {
        private Model1 db = new Model1();

        // GET: api/phone
        public IQueryable<phone> Getphones()
        {
            return db.phones;
        }

        // GET: api/phone/5
        [ResponseType(typeof(phone))]
        public async Task< IHttpActionResult> Getphone(int id)
        {
            phone phone = await db.phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            return Ok(phone);
        }

        // PUT: api/phone/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putphone(int id, phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != phone.id)
            {
                return BadRequest();
            }

            db.Entry(phone).State = EntityState.Modified;

            try
            {
               await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!phoneExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/phone
        [ResponseType(typeof(phone))]
        public async Task<IHttpActionResult> Postphone(phone phone)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.phones.Add(phone);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = phone.id }, phone);
        }

        // DELETE: api/phone/5
        [ResponseType(typeof(phone))]
        public async Task<IHttpActionResult> Deletephone(int id)
        {
            phone phone =await db.phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            db.phones.Remove(phone);
            await db.SaveChangesAsync();

            return Ok(phone);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool phoneExists(int id)
        {
            return db.phones.Count(e => e.id == id) > 0;
        }
    }
}