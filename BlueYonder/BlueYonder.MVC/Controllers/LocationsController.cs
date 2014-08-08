using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using BlueYonder.Model;

namespace BlueYonder.MVC.Controllers
{
    public class LocationsController : ApiController
    {
        private BlueYonderEntities db = new BlueYonderEntities();

        // GET api/Locations
        public IEnumerable<Locations> GetLocations()
        {
            return db.Locations.AsEnumerable();
        }

        // GET api/Locations/5
        public Locations GetLocations(int id)
        {
            Locations locations = db.Locations.Find(id);
            if (locations == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return locations;
        }

        // PUT api/Locations/5
        public HttpResponseMessage PutLocations(int id, Locations locations)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != locations.LocationId)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            db.Entry(locations).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // POST api/Locations
        public HttpResponseMessage PostLocations(Locations locations)
        {
            if (ModelState.IsValid)
            {
                db.Locations.Add(locations);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, locations);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = locations.LocationId }));
                return response;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        // DELETE api/Locations/5
        public HttpResponseMessage DeleteLocations(int id)
        {
            Locations locations = db.Locations.Find(id);
            if (locations == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Locations.Remove(locations);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }

            return Request.CreateResponse(HttpStatusCode.OK, locations);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}