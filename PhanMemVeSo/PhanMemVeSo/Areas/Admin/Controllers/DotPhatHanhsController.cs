using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EFModels;
using Model.ViewModels;
using Model.Dao;

namespace PhanMemVeSo.Areas.Admin.Controllers
{
    public class DotPhatHanhsController : Controller
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();
        // GET: Admin/DotPhatHanhs
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult findDateandTypeVeSo()
        {
            ViewBag.LoaiVeSoId = new SelectList(db.LoaiVeSoes, "LoaiVeSoId", "TenTinh");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult findDateandTypeVeSo(int LoaiVeSoId,System.DateTime ngayPhatHanhId)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Create",new { loaiVeSoId =LoaiVeSoId, ngayPhat =ngayPhatHanhId});
            }

            return View();
        }

        // GET: Admin/PhieuPhatHanhs/Create
        public ActionResult Create(int loaiVeSoId ,System.DateTime ngayPhat)
        {
            ViewBag.LoaiVeSo = loaiVeSoId;
            ViewBag.NgayPhatHanh = ngayPhat;

            DotPhatHanh dotPhatHanh = new DotPhatHanh();
            dotPhatHanh.NgayPhat = ngayPhat;
            dotPhatHanh.LoaiVeSoId = loaiVeSoId;
            List<PhieuPhatHanhVM> listPhieuPhatHanh = new List<PhieuPhatHanhVM>();
            var listDaiLyId = db.DaiLies.Select(m => m.DaiLyId).ToList();
            foreach(var item in listDaiLyId)
            {
                PhieuPhatHanhVM phieuPhatHanhVM = new PhieuPhatHanhVM();
                phieuPhatHanhVM.DaiLyId = item;
                DaiLyDao daiLyDao = new DaiLyDao();
                phieuPhatHanhVM.SLPhat=daiLyDao.TinhToanSLPhatTheoDaiLy(item, ngayPhat   );
                listPhieuPhatHanh.Add(phieuPhatHanhVM);
            }
            dotPhatHanh.PhieuPhatHanhs = listPhieuPhatHanh;
            
            return View(dotPhatHanh);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( DotPhatHanh dotPhatHanh)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in dotPhatHanh.PhieuPhatHanhs)
                {
                    PhieuPhatHanh phieuPhatHanh = new PhieuPhatHanh();
                    phieuPhatHanh.LoaiVeSoId = dotPhatHanh.LoaiVeSoId;
                    phieuPhatHanh.NgayPhat = dotPhatHanh.NgayPhat;
                    phieuPhatHanh.DaiLyId = item.DaiLyId;
                    phieuPhatHanh.SLPhat = item.SLPhat;
                    db.PhieuPhatHanhs.Add(phieuPhatHanh);
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(dotPhatHanh);
        }
    }
}