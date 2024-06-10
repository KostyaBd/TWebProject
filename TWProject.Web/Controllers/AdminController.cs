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
using TWProject.Domain.Entities.Booking;
using TWProject.Domain.Entities.Car;
using TWProject.Domain.Entities.User;

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

        public ActionResult UserIndex()
        {
            return View(db.User.ToList());
        }
        public ActionResult BookingsIndex()
        {
            return View(db.Bookings.ToList());
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

        public ActionResult UserEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UDBTable uDBTable = db.User.Find(id);
            if (uDBTable == null)
            {
                return HttpNotFound();
            }
            return View(uDBTable);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit([Bind(Include = "UserId,Name,Level,Email,RegistrationDate")] UDBTable uDBTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(uDBTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserIndex");
            }
            return View(uDBTable);
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

        public ActionResult UserDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UDBTable uDBTable = db.User.Find(id);
            if (uDBTable == null)
            {
                return HttpNotFound();
            }
            return View(uDBTable);
        }

        [HttpPost, ActionName("UserDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult UserDeleteConfirmed(int id)
        {
            UDBTable uDBTable = db.User.Find(id);
            db.User.Remove(uDBTable);
            db.SaveChanges();
            return RedirectToAction("UserIndex");
        }

        public ActionResult BookingsDelete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingDBTable bookingDBTable = db.Bookings.Find(id);
            if (bookingDBTable == null)
            {
                return HttpNotFound();
            }
            return View(bookingDBTable);
        }

        [HttpPost, ActionName("BookingsDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult BookingsDeleteConfirmed(int id)
        {
            BookingDBTable bookingDBTable = db.Bookings.Find(id);
            db.Bookings.Remove(bookingDBTable);
            db.SaveChanges();
            return RedirectToAction("BookingsIndex");
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
