using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils.Drawing.Animation;
using DevExpress.XtraEditors;
using is_takip.Entity;

namespace is_takip.Formlar
{
    public partial class FrmGorev : Form
    {
        public FrmGorev()
        {
            InitializeComponent();
        }

        private void btnVazgec_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DbisTakipEntities db = new DbisTakipEntities();
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            TblGorevler tblGorevler = new TblGorevler();
            tblGorevler.Aciklama = txtAciklama.Text;
            tblGorevler.Durum = true;
            tblGorevler.GorevAlan = int.Parse(txtGorevAlan.EditValue.ToString());
            tblGorevler.GorevVeren = int.Parse(txtGorevVeren.EditValue.ToString());
            tblGorevler.Tarih = DateTime.Parse(txtTarih.Text);
            db.TblGorevler.Add(tblGorevler);
            db.SaveChanges();
            XtraMessageBox.Show("Görev Başarıyla Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmGorev_Load(object sender, EventArgs e)
        {
            var degerler = (from x in db.TblPersonel
                                select new
                                {
                                    x.ID,
                                    AdSoyad = x.Ad + " " + x.Soyad
                                }).ToList();
            txtGorevAlan.Properties.ValueMember = "ID";
            txtGorevAlan.Properties.DisplayMember = "AdSoyad";
            txtGorevAlan.Properties.DataSource = degerler;
        }
    }
}
