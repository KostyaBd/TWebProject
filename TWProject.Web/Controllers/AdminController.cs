using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TWProject.Attributes;
using TWProject.BusinessLogic.DB;
using TWProject.BusinessLogic.Interfaces;
using TWProject.Domain.Entities.Car;

namespace TWProject.Web.Controllers
{
    [AdminMod]
    public class AdminController : Controller
    {
	    private CarRentalContext db = new CarRentalContext();
       
        public ActionResult Index()
        {
	        return View(db.Cars.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDBTable carDBTable = db.Cars.Find(id);
            if (carDBTable == null)
            {
                return HttpNotFound();
            }
            return View(carDBTable);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CarId,Mark,Model,ProductionYear,BodyType,SeatsNum,Color,Odometer,EnginePower,EngineCapacity,PricePerDay,GearboxType,FuelType,ImagePath,IsAvailable")] CarDBTable carDBTable)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(carDBTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(carDBTable);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDBTable carDBTable = db.Cars.Find(id);
            if (carDBTable == null)
            {
                return HttpNotFound();
            }
            return View(carDBTable);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CarId,Mark,Model,ProductionYear,BodyType,SeatsNum,Color,Odometer,EnginePower,EngineCapacity,PricePerDay,GearboxType,FuelType,ImagePath,IsAvailable")] CarDBTable carDBTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carDBTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(carDBTable);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarDBTable carDBTable = db.Cars.Find(id);
            if (carDBTable == null)
            {
                return HttpNotFound();
            }
            return View(carDBTable);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarDBTable carDBTable = db.Cars.Find(id);
            db.Cars.Remove(carDBTable);
            db.SaveChanges();
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
