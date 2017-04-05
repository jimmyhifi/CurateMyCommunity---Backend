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
    public class HostsController : ApiController
    {
        private CMC_DB_Connection db = new CMC_DB_Connection();

        // GET: api/Hosts
        public IQueryable<Host> GetHosts()
        {
            return db.Hosts;
        }

        // GET: api/Hosts/5
        [ResponseType(typeof(Host))]
        public IHttpActionResult GetHosts(int id)
        {
            Host hosts = db.Hosts.Find(id);
            if (hosts == null)
            {
                return NotFound();
            }

            return Ok(hosts);
        }

        // PUT: api/Hosts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHosts(int id, Host hosts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hosts.id_hosts)
            {
                return BadRequest();
            }

            db.Entry(hosts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HostsExists(id))
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

        // POST: api/Hosts
        [ResponseType(typeof(Host))]
        public IHttpActionResult PostHosts(Host hosts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hosts.Add(hosts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hosts.id_hosts }, hosts);
        }

        // DELETE: api/Hosts/5
        //[ResponseType(typeof(Hosts))]
        //public IHttpActionResult DeleteHosts(int id)
        //{
        //    Hosts hosts = db.Hosts.Find(id);
        //    if (hosts == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Hosts.Remove(hosts);
        //    db.SaveChanges();

        //    return Ok(hosts);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool HostsExists(int id)
        {
            return db.Hosts.Count(e => e.id_hosts == id) > 0;
        }
    }
}