using is_takip.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace is_takip.Formlar
{
    public partial class FrmAnaForm : Form
    {
        public FrmAnaForm()
        {
            InitializeComponent();
        }
        DbisTakipEntities db = new DbisTakipEntities();

        private void FrmAnaForm_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = (from x in db.TblGorevler
                                       select new
                                       {
                                           x.Aciklama,
                                           GorevAlan = x.TblPersonel.Ad + " " + x.TblPersonel.Soyad,
                                           x.Tarih,
                                           x.Durum
                                       }).Where(y=>y.Durum == true).ToList();
            gridView1.Columns["Durum"].Visible = false;

            //Bugün yapılan görevler
            DateTime bugun = DateTime.Parse(DateTime.Now.ToShortDateString());
            gridControl2.DataSource = (from x in db.TblGorevDetaylar
                                       select new
                                       {
                                           x.TblGorevler.Aciklama,
                                           detay=x.Aciklama,
                                           x.Tarih
                                       }).Where(y => y.Tarih == bugun).ToList();
            gridView2.Columns["Tarih"].Visible = false;

            // Aktif Çağrı Listesi
            gridControl3.DataSource = (from x in db.TblCagrilar
                                       select new
                                       {
                                           x.TblFirmalar.Ad,
                                           x.Konu,
                                           x.Tarih,
                                           x.Durum
                                       }).Where(y => y.Durum == true).ToList();
            gridView3.Columns["Durum"].Visible = false;

            // Fihrist Komutları
            gridControl5.DataSource = (from x in db.TblFirmalar
                                       select new
                                       {
                                           x.Ad,
                                           x.Telefon,
                                           x.Mail
                                       }).ToList();

            // Çağrı Grafiği

            int aktif_cagrilar = db.TblCagrilar.Count(x => x.Durum == true);
            int pasif_cagrilar = db.TblCagrilar.Count(x => x.Durum == false);

            chartControl1.Series["Series 1"].Points.AddPoint("Aktif Çağrılar", aktif_cagrilar);
            chartControl1.Series["Series 1"].Points.AddPoint("Pasif Çağrılar", pasif_cagrilar);

        }
    }
}
