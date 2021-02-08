using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservasiApp.Models;

namespace ReservasiApp.Controllers
{
    public class ReservationController : Controller
    {
        public static bool status;

        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetReservation()
        {
            using (MDKAReservasiEntities db = new MDKAReservasiEntities())
            {
                var employees = db.tblT_Reservasi.OrderBy(x => x.TanggalReservasi).ToList();
                return Json(new { data = employees }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        // GET: Home/Details/5
        public ActionResult Save(int id)
        {
            using (MDKAReservasiEntities db = new MDKAReservasiEntities())
            {
                var v = db.tblT_Reservasi.Where(x => x.Reservasi_PK == id).FirstOrDefault();
                return View(v);
            }
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Save(tblT_Reservasi emp)
        {
            try
            {
                // TODO: Add insert logic here

                using (MDKAReservasiEntities db = new MDKAReservasiEntities())
                {
                    if (emp.Reservasi_PK > 0)
                    {
                        var v = db.tblT_Reservasi.Where(x => x.Reservasi_PK == emp.Reservasi_PK).FirstOrDefault();

                        if (v != null)
                        {
                            v.Ruangan_FK = emp.Ruangan_FK;
                            v.SubjectReservasi = emp.SubjectReservasi;
                            v.TanggalReservasi = emp.TanggalReservasi;
                            v.JamMulai = emp.JamMulai;
                            v.JamSelesai = emp.JamSelesai;
                        }
                    }
                    else
                    {
                        db.tblT_Reservasi.Add(emp);
                    }

                    db.SaveChanges();
                    status = true;
                }

                return new JsonResult { Data = new { status = status } };

            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            using (MDKAReservasiEntities db = new MDKAReservasiEntities())
            {
                var v = db.tblT_Reservasi.FirstOrDefault(x => x.Reservasi_PK == id);
                if (v != null)
                {
                    return View(v);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        // POST: Home/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteEmployee(int id)
        {
            try
            {
                bool status = false;
                using (MDKAReservasiEntities dc = new MDKAReservasiEntities())
                {
                    var v = dc.tblT_Reservasi.Where(a => a.Reservasi_PK == id).FirstOrDefault();
                    if (v != null)
                    {
                        dc.tblT_Reservasi.Remove(v);
                        dc.SaveChanges();
                        status = true;
                    }
                }
                return new JsonResult { Data = new { status = status } };
            }
            catch
            {
                return View();
            }
        }
    }
}