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
using IosServiceAPp.Models;

namespace IosServiceAPp.Controllers
{
    public class MyController : ApiController
    {
        private iosServiceDBEntities db = new iosServiceDBEntities();

        // GET: api/My
        public IQueryable<tblHouse> GettblHouses()
        {
            return db.tblHouses;
        }

        // GET: api/My/5
        [ResponseType(typeof(tblHouse))]
        public IHttpActionResult GettblHouse(int id)
        {
            tblHouse tblHouse = db.tblHouses.Find(id);
            if (tblHouse == null)
            {
                return NotFound();
            }

            return Ok(tblHouse);
        }

        // PUT: api/My/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PuttblHouse(int id, tblHouse tblHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tblHouse.houseid)
            {
                return BadRequest();
            }

            db.Entry(tblHouse).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tblHouseExists(id))
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

        // POST: api/My
        [ResponseType(typeof(tblHouse))]
        public IHttpActionResult PosttblHouse(tblHouse tblHouse)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tblHouses.Add(tblHouse);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tblHouse.houseid }, tblHouse);
        }

        // DELETE: api/My/5
        [ResponseType(typeof(tblHouse))]
        public IHttpActionResult DeletetblHouse(int id)
        {
            tblHouse tblHouse = db.tblHouses.Find(id);
            if (tblHouse == null)
            {
                return NotFound();
            }

            db.tblHouses.Remove(tblHouse);
            db.SaveChanges();

            return Ok(tblHouse);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tblHouseExists(int id)
        {
            return db.tblHouses.Count(e => e.houseid == id) > 0;
        }
    }
}