using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using is_takip.Entity;

namespace is_takip.Formlar
{
    public partial class FrmPersonelistatistik : Form
    {
        public FrmPersonelistatistik()
        {
            InitializeComponent();
        }
        DbisTakipEntities db = new DbisTakipEntities();
        private void FrmPersonelistatistik_Load(object sender, EventArgs e)
        {
            lblToplamDepartman.Text = db.TblDepartmanlar.Count().ToString();
            lblToplamPersonel.Text = db.TblPersonel.Count().ToString();
            lblFirmaSayisi.Text = db.TblFirmalar.Count().ToString();
            lblAktifis.Text = db.TblGorevler.Count(x => x.Durum == true).ToString();
            lblPasifis.Text = db.TblGorevler.Count(x => x.Durum == false).ToString();
            lblSonGorev.Text = db.TblGorevler.OrderByDescending(x => x.ID).Select(y => y.Aciklama).FirstOrDefault();
            lblSehirSayisi.Text = db.TblFirmalar.Select(x => x.il).Distinct().Count().ToString();
            lblSektorSayisi.Text = db.TblFirmalar.Select(x => x.Sektör).Distinct().Count().ToString();
            DateTime bugun = DateTime.Today;
            lblGunlukisSayisi.Text = db.TblGorevler.Count(x => x.Tarih == bugun).ToString();
            var ayinPersoneli = db.TblGorevler.GroupBy(x =>x.GorevAlan).OrderByDescending(y => y.Count()).Select(z => z.Key).FirstOrDefault();
            lblAyinPersoneli.Text = db.TblPersonel.Where(x =>x.ID==ayinPersoneli).Select(y => y.Ad + " " + y.Soyad).FirstOrDefault().ToString();
            lblAyinDepartmani.Text = db.TblDepartmanlar.Where(x => x.ID == db.TblPersonel.Where(y => y.ID == ayinPersoneli).Select(z => z.Departman).FirstOrDefault()).Select(k => k.Ad).FirstOrDefault().ToString();
            var maxTaskAssigner = db.TblGorevler
                                .GroupBy(g => g.GorevVeren)
                                .Select(group => new { GorevVeren = group.Key, TaskCount = group.Count() })
                                .OrderByDescending(x => x.TaskCount)
                                .FirstOrDefault()?.GorevVeren;
            var departmentName = db.TblPersonel
                                .Where(p => p.ID == maxTaskAssigner)
                                .Select(p => p.TblDepartmanlar.Ad)
                                .FirstOrDefault();
            lblMaxDepartman.Text = departmentName;
        }
    }
}
