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
using CurateMyCommunity_Api.Models.Data;

namespace CurateMyCommunity_Api.Controllers
{
    public class ExhibitsController : ApiController
    {
        private CMC_DB_Connection db = new CMC_DB_Connection();

        // GET: api/Exhibits
        public IQueryable<Exhibit> GetExhibits()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Exhibits;
        }

        // GET: api/Exhibits/5
        [ResponseType(typeof(Exhibit))]
        public IHttpActionResult GetExhibit(int id)
        {
            Exhibit exhibit = db.Exhibits.Find(id);
            if (exhibit == null)
            {
                return NotFound();
            }

            return Ok(exhibit);
        }

        // PUT: api/Exhibits/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutExhibit(int id, Exhibit exhibit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != exhibit.id_exhibits)
            {
                return BadRequest();
            }

            db.Entry(exhibit).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExhibitExists(id))
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

        // POST: api/Exhibits
        [ResponseType(typeof(Exhibit))]
        public IHttpActionResult PostExhibit(Exhibit exhibit)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Exhibits.Add(exhibit);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = exhibit.id_exhibits }, exhibit);
        }

        // DELETE: api/Exhibits/5
        //[ResponseType(typeof(Exhibit))]
        //public IHttpActionResult DeleteExhibit(int id)
        //{
        //    Exhibit exhibit = db.Exhibits.Find(id);
        //    if (exhibit == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Exhibits.Remove(exhibit);
        //    db.SaveChanges();

        //    return Ok(exhibit);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool ExhibitExists(int id)
        {
            return db.Exhibits.Count(e => e.id_exhibits == id) > 0;
        }
    }
}