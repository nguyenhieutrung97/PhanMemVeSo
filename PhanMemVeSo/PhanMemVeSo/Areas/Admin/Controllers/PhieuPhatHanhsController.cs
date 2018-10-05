using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Model.EFModels;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class PhieuPhatHanhsController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();

        // GET: Admin/PhieuPhatHanhs
        public ActionResult Index()
        {
            var phieuPhatHanhs = db.PhieuPhatHanhs.Include(p => p.DaiLy).Include(p => p.LoaiVeSo);
            return View(phieuPhatHanhs.ToList());
        }

        // GET: Admin/PhieuPhatHanhs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            if (phieuPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhatHanh);
        }

        // GET: Admin/PhieuPhatHanhs/Create
        public ActionResult Create()
        {
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy");
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh");
            return View();
        }

        // POST: Admin/PhieuPhatHanhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DaiLyId,LoaiVeSoId,NgayPhat,SLPhat,SLBanDuoc")] PhieuPhatHanh phieuPhatHanh)
        {
            if (ModelState.IsValid)
            {
                db.PhieuPhatHanhs.Add(phieuPhatHanh);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuPhatHanh.DaiLyId);
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh", phieuPhatHanh.LoaiVeSoId);
            return View(phieuPhatHanh);
        }

        // GET: Admin/PhieuPhatHanhs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            if (phieuPhatHanh == null)
            {
                return HttpNotFound();
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuPhatHanh.DaiLyId);
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh", phieuPhatHanh.LoaiVeSoId);
            return View(phieuPhatHanh);
        }

        // POST: Admin/PhieuPhatHanhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DaiLyId,LoaiVeSoId,NgayPhat,SLPhat,SLBanDuoc")] PhieuPhatHanh phieuPhatHanh)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phieuPhatHanh).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DaiLyId = new SelectList(db.DaiLies, "DaiLyId", "TenDaiLy", phieuPhatHanh.DaiLyId);
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh", phieuPhatHanh.LoaiVeSoId);
            return View(phieuPhatHanh);
        }

        // GET: Admin/PhieuPhatHanhs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            if (phieuPhatHanh == null)
            {
                return HttpNotFound();
            }
            return View(phieuPhatHanh);
        }

        // POST: Admin/PhieuPhatHanhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhieuPhatHanh phieuPhatHanh = db.PhieuPhatHanhs.Find(id);
            db.PhieuPhatHanhs.Remove(phieuPhatHanh);
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
