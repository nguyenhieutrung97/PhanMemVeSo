using Model.EFModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class DaiLyDao
    {
        private PhanPhoiVeSoEntities db = new PhanPhoiVeSoEntities();
        public decimal TinhToanSLPhatTheoDaiLy(int daiLyId, System.DateTime ngayPhat)
        {
            decimal slDangKy = db.PhieuDangKies.OrderByDescending(m => m.NgayDangKy).Where(m => m.DaiLyId == daiLyId & m.NgayDangKy <= ngayPhat).Select(m=>m.SLDangKy).FirstOrDefault();
            System.DateTime ngayDangKy= db.PhieuDangKies.OrderByDescending(m => m.NgayDangKy).Where(m => m.DaiLyId == daiLyId & m.NgayDangKy <= ngayPhat).Select(m => m.NgayDangKy).FirstOrDefault();
            var listTop3 = db.PhieuPhatHanhs.OrderByDescending(m => m.NgayPhat).Where(m => m.DaiLyId == daiLyId).Take(3);
            int count = listTop3.Count();
            if (count == 0)
            {
                return slDangKy;
            }
            else {
                var list = listTop3.Select(m => (m.SLBanDuoc * 100 / slDangKy));
                decimal? getReturn = list.Sum() / 3;
                return getReturn??default(decimal);
            }
        }
    }
}
