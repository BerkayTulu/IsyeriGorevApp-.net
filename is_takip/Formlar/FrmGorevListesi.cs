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
    public partial class FrmGorevListesi : Form
    {
        public FrmGorevListesi()
        {
            InitializeComponent();
        }
        DbisTakipEntities db = new DbisTakipEntities();
        private void FrmGorevListesi_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblGorevler
                                       select new
                                       {
                                           x.Aciklama
                                       }).ToList();
            lblAktifGorev.Text = db.TblGorevler.Count(x => x.Durum == true).ToString();
            lblPasifGorev.Text = db.TblGorevler.Count(x => x.Durum == false).ToString();
            lblToplamDepartman.Text = db.TblDepartmanlar.Count().ToString();

            chartControl1.Series["Durum"].Points.AddPoint("Aktif Görev", int.Parse(lblAktifGorev.Text));
            chartControl1.Series["Durum"].Points.AddPoint("Pasif Görev", int.Parse(lblPasifGorev.Text));

        }
    }
}
