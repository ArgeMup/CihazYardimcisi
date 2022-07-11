// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ArgeMup.HazirKod;
using System.IO;
using System.Reflection;
using System.IO.Ports;

namespace Cihaz_Yardımcısı
{
    public partial class AnaEkran : Form
    {
        YeniYazılımKontrolü_ YeniYazılımKontrolü = new YeniYazılımKontrolü_();
        public AnaEkran()
        {
            InitializeComponent();
        }
        private void AnaEkran_Load(object sender, EventArgs e)
        {
            Text = "ArGeMuP " + Kendi.Adı() + " V" + Kendi.Sürümü_Dosya();

            O.pak = Kendi.Klasörü() + "\\CihazYardımcısıDosyalari\\"; //programanaklasör

            Directory.CreateDirectory(O.pak + "Banka");
            O.Ayarlar = new Ayarlar_
                (out _, "", O.pak + "Banka\\" + Kendi.Adı() + ".Ayarlar");

            O.UygulamaKontrolu = new PencereVeTepsiIkonuKontrolu_(this, O.Ayarlar);
            O.UygulamaKontrolu.TepsiİkonunuBaşlat(false);

            splitContainer1.SplitterDistance = Convert.ToInt32(O.Ayarlar.Oku("splitContainer1.SplitterDistance", splitContainer1.SplitterDistance.ToString()));
        }
        private void AnaEkran_Shown(object sender, EventArgs e)
        {
            ResimListesi.Images.Clear();
            ResimListesi.Images.Add(Properties.Resources.Cihaz_SeriPort_Yeşil);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_SeriPort_Kırmızı);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_SeriPort_Mavi);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_Ağ_Yeşil);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_Ağ_Kırmızı);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_Ağ_Mavi);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_Uygulama_Yeşil);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_Uygulama_Kırmızı);
            ResimListesi.Images.Add(Properties.Resources.Cihaz_Uygulama_Mavi);
            ResimListesi.Images.Add(Properties.Resources.Durgun);
            ResimListesi.Images.Add(Properties.Resources.Meşgul);
            ResimListesi.Images.Add(Properties.Resources.Seçili);
            ResimListesi.Images.Add(Properties.Resources.Bulunanlar);
            ResimListesi.Images.Add(Properties.Resources.İzinKontrolu);
            ResimListesi.Images.Add(Properties.Resources.İzinAraMal);
            //Ağaç.SelectedImageIndex = (int)Görseller.Resim.Seçili;
            //Ağaç.TreeViewNodeSorter = new Ağaç_NodeSorter();
            
            Görseller.Başlat(Ağaç);
            Cihazlar.Başlat();

            O.DurumBildirimi = new DurumBildirimi_(this);

            O.UygulamaOncedenCalistirildiMi = new UygulamaOncedenCalistirildiMi_();
            if (O.UygulamaOncedenCalistirildiMi.KontrolEt())
            {
                int adet = O.UygulamaOncedenCalistirildiMi.DiğerUygulamayıÖneGetir();
                O.DurumBildirimi.BaloncukluUyarı("Çalışan " + adet + " adet daha uygulama mevcut.");
            }

            //Uygulamaların Listelenmesi
            string SonUygulam = O.Ayarlar.Oku("KullanıcınınSeçtiğiSonUygulama", "Gönder_Al");
            foreach(Type Tip in from t in Assembly.GetExecutingAssembly().GetTypes() where t.Namespace == "Cihaz_Yardımcısı.Uygulama" && t.BaseType.Name == "UserControl" && t.Name != "Örnek_Şablon" select t)
            {
                ToolStripMenuItem yeni_tsmi = new ToolStripMenuItem();
                yeni_tsmi.Name = Tip.Name;
                yeni_tsmi.Text = Tip.Name.Replace('_', ' ');
                yeni_tsmi.Click += AnaEkran_Uygulama_Seçildi;

                Menü_Ağaç_uygulama.DropDownItems.Add(yeni_tsmi);

                if (SonUygulam == yeni_tsmi.Name) AnaEkran_Uygulama_Seçildi(yeni_tsmi, null);
            }

            YeniYazılımKontrolü.Başlat(new Uri("https://github.com/ArgeMup/CihazYardimcisi/blob/main/Cihaz%20Yard%C4%B1mc%C4%B1s%C4%B1/bin/Release/Cihaz%20Yard%C4%B1mc%C4%B1s%C4%B1.exe?raw=true"));
        }
        private void AnaEkran_FormClosed(object sender, FormClosedEventArgs e)
        {
            O.Çalışıyor = false;
            Bağlantılar_Tarama.Durdur();
            Cihazlar.Durdur();

            O.Ayarlar.Yaz("splitContainer1.SplitterDistance", splitContainer1.SplitterDistance.ToString());

            if (O.KullanıcınınSeçtiğiUygulama != null)
            {
                if (Bağlantılar_Uygulama.Çağırılacak_Islem_Kapatıldı != null) Bağlantılar_Uygulama.Çağırılacak_Islem_Kapatıldı("");

                ((UserControl)O.KullanıcınınSeçtiğiUygulama).Dock = DockStyle.None;
                splitContainer1.Panel2.Controls.Remove((UserControl)O.KullanıcınınSeçtiğiUygulama);
                ((UserControl)O.KullanıcınınSeçtiğiUygulama).Dispose();
                O.KullanıcınınSeçtiğiUygulama = null;
            }

            O.UygulamaKontrolu.Dispose();
            YeniYazılımKontrolü.Durdur();
        }
        private void AnaEkran_Uygulama_Seçildi(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = sender as ToolStripMenuItem;

            if (tsmi.Name == O.KullanıcınınSeçtiğiUygulama_Adı) return;

            foreach (Type Tip in from t in Assembly.GetExecutingAssembly().GetTypes() where t.IsClass && t.Namespace == "Cihaz_Yardımcısı.Uygulama" select t)
            {
                if (Tip.Name == tsmi.Name)
                {
                    try
                    {
                        if (O.KullanıcınınSeçtiğiUygulama != null)
                        {
                            if (Bağlantılar_Uygulama.Çağırılacak_Islem_Kapatıldı != null) Bağlantılar_Uygulama.Çağırılacak_Islem_Kapatıldı("");
                            Bağlantılar_Uygulama.Çağırılacak_Islem_CihazDeğişti = null;
                            Bağlantılar_Uygulama.Çağırılacak_Islem_BilgiGeldi = null;
                            Bağlantılar_Uygulama.Çağırılacak_Islem_Kapatıldı = null;

                            ((UserControl)O.KullanıcınınSeçtiğiUygulama).Dock = DockStyle.None;
                            splitContainer1.Panel2.Controls.Remove((UserControl)O.KullanıcınınSeçtiğiUygulama);
                            ((UserControl)O.KullanıcınınSeçtiğiUygulama).Dispose();
                            O.KullanıcınınSeçtiğiUygulama = null;
                        }

                        Bağlantılar_Uygulama.DosyaKayıtKlasörü = O.pak + tsmi.Name + @"\";
                        Bağlantılar_Uygulama.GörselDüzen = Font;

                        O.KullanıcınınSeçtiğiUygulama = Activator.CreateInstance(Tip, new object[] { /*Font*/ });
                        O.KullanıcınınSeçtiğiUygulama_Adı = Tip.Name;
                        splitContainer1.Panel2.Controls.Add((UserControl)O.KullanıcınınSeçtiğiUygulama);
                        ((UserControl)O.KullanıcınınSeçtiğiUygulama).Dock = DockStyle.Fill;

                        foreach (ToolStripMenuItem dal in Menü_Ağaç_uygulama.DropDownItems) dal.Checked = false;
                        tsmi.Checked = true;

                        O.Ayarlar.Yaz("KullanıcınınSeçtiğiSonUygulama", tsmi.Name);
                    }
                    catch (Exception ex) { O.DurumBildirimi.BaloncukluUyarı("Örnek_Şablon un tüm nitelikleri karşılanmıyor" + Environment.NewLine + Environment.NewLine + ex.Message); }

                    return;
                }
            }
        }

        private void Menü_Ağaç_yenile_Click(object sender, EventArgs e)
        {
            if (Bağlantılar_Tarama.Durumu() == Bağlantılar_Tarama.Durum.Taranıyor)
            {
                O.DurumBildirimi.BaloncukluUyarı("Tarama iptal ediliyor.");
                Bağlantılar_Tarama.Durdur();
            }
            else Bağlantılar_Tarama.Başlat();
        }
        private void Menü_Ağaç_Opening(object sender, CancelEventArgs e)
        {
            if (Bağlantılar_Tarama.Durumu() == Bağlantılar_Tarama.Durum.Taranıyor) Menü_Ağaç_yenile.Text = "Durdur";
            else Menü_Ağaç_yenile.Text = "Yenile";

            if (Ağaç.SelectedNode == null || Ağaç.SelectedNode.Tag == null) goto HatalıÇıkış;

            Menü_Ağaç_yenile.Visible = true;
            Menü_Ağaç_ekle.Visible = true;

            if (Ağaç.SelectedNode.Tag is _Ayar_Bağlantı_)
            {
                Menü_Ağaç_kaldır.Visible = false;
                Menü_Ağaç_izinDurumu.Visible = false;

                _Ayar_Bağlantı_ bağlantı = Ağaç.SelectedNode.Tag as _Ayar_Bağlantı_;
                Menü_Ağaç_TextBox.Text = bağlantı.SonEklemeYazısı;

                switch (bağlantı.Tipi)
                {
                    case Cihaz_Tip.SeriPort:
                        if (Menü_Ağaç_TextBox.Text == "") Menü_Ağaç_TextBox.Text = "CTCx CTCx 921600 8 Yok 1 1000 1500 500";
                        Menü_Ağaç_TextBox.ToolTipText = "A B C D E F G H I" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "A : Bağlantının adı" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "B : Başlık (Boş olabilir)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "C : Bit hızı bps (>300)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "D : Bit sayısı (7 : 8)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "E : Doğrulama ( Yok : Tek : Çift : Boşluk : İşaret )" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "F : Dur biti ( 0 : 1 : 1,5 : 2 )" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "G : Gönderim zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "H : Tarama öncesi uyandırma zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "I : Tarama cevap gelme zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "* Boşluk yerine / karakteri de kullanılabilir";
                        break;

                    case Cihaz_Tip.AğTcpSunucu:
                    case Cihaz_Tip.AğUdpDinleyiciGönderici:
                        if (Menü_Ağaç_TextBox.Text == "") Menü_Ağaç_TextBox.Text = "CTCx CTCx 9999 1000 1500 500";
                        Menü_Ağaç_TextBox.ToolTipText = "A B C D E F" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "A : Bağlantının adı" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "B : Başlık (Boş olabilir)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "C : Erişim noktası" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "D : Gönderim zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "E : Tarama öncesi uyandırma zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "F : Tarama cevap gelme zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "* Boşluk yerine / karakteri de kullanılabilir";
                        break;

                    case Cihaz_Tip.AğTcpİstemci:
                    case Cihaz_Tip.AğUdpGönderici:
                        if (Menü_Ağaç_TextBox.Text == "") Menü_Ağaç_TextBox.Text = "CTCx CTCx 127.0.0.1 9999 5000 1000 1500 500";
                        Menü_Ağaç_TextBox.ToolTipText = "A B C D E F G H" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "A : Bağlantının adı" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "B : Başlık (Boş olabilir)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "C : Sunucunun adresi" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "D : Sunucunun erişim noktası" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "E : İlk bağlantı zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "F : Gönderim zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "G : Tarama öncesi uyandırma zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "H : Tarama cevap gelme zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "* Boşluk yerine / karakteri de kullanılabilir";
                        break;

                    case Cihaz_Tip.KomutSatırı:
                        if (Menü_Ağaç_TextBox.Text == "") Menü_Ağaç_TextBox.Text = "CTCx / CTCx / C:\\Uygulama.exe / -a 100 -b 500 / 1000 / 1500 / 500";
                        Menü_Ağaç_TextBox.ToolTipText = "A B C D E F G" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "A : Bağlantının adı" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "B : Başlık (Boş olabilir)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "C : Uygulamanın dosya sistemi üzerindeki konumu" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "D : Uygulamanın başlangıç parametreleri (Boş olabilir)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "E : Gönderim zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "F : Tarama öncesi uyandırma zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "G : Tarama cevap gelme zaman aşımı msn" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "* Boşluk yerine / karakteri de kullanılabilir";
                        break;
                }

                Ağaç.Tag = bağlantı;
                return;
            }
            else if (Ağaç.SelectedNode.Tag is _Ayar_Cihaz_)
            {
                _Ayar_Cihaz_ cihaz = Ağaç.SelectedNode.Tag as _Ayar_Cihaz_;

                if (cihaz.KullanıcıEkledi) Menü_Ağaç_kaldır.Visible = true;
                else Menü_Ağaç_kaldır.Visible = false;
                Menü_Ağaç_izinDurumu.Visible = true;
                Menü_Ağaç_TextBox.Text = cihaz.SonEklemeYazısı;

                switch (cihaz.Tipi)
                {
                    case Cihaz_Tip.SeriPort:
                        string KullanılabilirDonanımlar = "";
                        foreach (var don in SerialPort.GetPortNames()) { KullanılabilirDonanımlar += don + ", "; }
                        KullanılabilirDonanımlar = KullanılabilirDonanımlar.TrimEnd(' ', ',');
                        if (string.IsNullOrEmpty(Menü_Ağaç_TextBox.Text)) Menü_Ağaç_TextBox.Text = "COMx 1 255";

                        Menü_Ağaç_TextBox.ToolTipText = "A B C -> (CTC?? [B] ile CTC?? [B+C] arasındaki kartlar)" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "A : Seri Port (" + KullanılabilirDonanımlar + ")" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "B : Başlangıç adresi" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "C : Takip edilecek kanal sayısı" + Environment.NewLine;
                        Menü_Ağaç_TextBox.ToolTipText += "* Boşluk yerine / karakteri de kullanılabilir";
                        break;

                    case Cihaz_Tip.AğTcpSunucu:
                    case Cihaz_Tip.AğTcpİstemci:
                    case Cihaz_Tip.AğUdpDinleyiciGönderici:
                    case Cihaz_Tip.AğUdpGönderici:
                    case Cihaz_Tip.KomutSatırı:
                        Menü_Ağaç_ekle.Visible = false;
                        break;
                }
                
                Ağaç.Tag = cihaz;
                return;
            }

        HatalıÇıkış:
            Ağaç.Tag = null;
            e.Cancel = true;
            //Menü_Ağaç_yenile.Visible = true;
            //Menü_Ağaç_ekle.Visible = false;
            //Menü_Ağaç_kaldır.Visible = false;
            //Menü_Ağaç_izinDurumu.Visible = false;
        }     
        private void Menü_Ağaç_tamam_Click(object sender, EventArgs e)
        {
            if (Ağaç.Tag == null) return;

            if (!Cihazlar.Kullanıcıİsteği_Ekle(Ağaç.Tag, Menü_Ağaç_TextBox.Text))
            {
                O.DurumBildirimi.BaloncukluUyarı("Girdileri kontrol ediniz." + Environment.NewLine + Menü_Ağaç_TextBox.Text);
            }
            else Ağaç.SelectedNode.Expand();
        }
        private void Menü_Ağaç_kaldır_Click(object sender, EventArgs e)
        {
            if (Ağaç.Tag != null && Ağaç.Tag.GetType() == typeof(_Ayar_Cihaz_))
            {
                _Ayar_Cihaz_ ayar = (Ağaç.Tag as _Ayar_Cihaz_);
                if (ayar.KullanıcıEkledi)
                {
                    DialogResult Dr = MessageBox.Show("Kaldırmak istediğinize emin misiniz?", Kendi.Adı(), MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (Dr == DialogResult.No) return;

                    Cihazlar.Kullanıcıİsteği_Sil(ayar);
                    Görseller.YenidenÇizdir();
                }
            }
        }
        private void Menü_Ağaç_izinDurumu_Click(object sender, EventArgs e)
        {
            if (Ağaç.Tag != null && Ağaç.Tag.GetType() == typeof(_Ayar_Cihaz_))
            {
                _Ayar_Cihaz_ ayar = (Ağaç.Tag as _Ayar_Cihaz_);
                ayar.Etkin = !ayar.Etkin;
                Cihazlar.Kullanıcıİsteği_Kaydet();
                Görseller.Dal_Değiştir(ayar.Dalı, null, null, Görseller.Resim_Bul(ayar.Tipi, ayar.Etkin ? "m" : "k"));
            }
        }
        private void Menü_Ağaç_genişlet_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode != null) Ağaç.SelectedNode.ExpandAll();
        }
        private void Menü_Ağaç_daralt_Click(object sender, EventArgs e)
        {
            if (Ağaç.SelectedNode != null) Ağaç.SelectedNode.Collapse(false);
        }
        #region Ağaç_NodeSorter
        private class Ağaç_NodeSorter : System.Collections.IComparer
        {   // Your sorting logic here... return -1 if tx < ty, 1 if tx > ty, 0 otherwise
            public int Compare(object x_, object y_)
            {
                string x, y;

                if (((TreeNode)x_).Parent == null) return -1;
                if (((TreeNode)y_).Parent == null) return 1;

                if (((TreeNode)x_).Name.StartsWith("IZ") || ((TreeNode)y_).Name.StartsWith("IZ"))
                {
                    if (((TreeNode)x_).Name == "IZ SP") return -1;
                    if (((TreeNode)y_).Name == "IZ SP") return 1;

                    if (((TreeNode)x_).Name == "IZ AG") return -1;
                    if (((TreeNode)y_).Name == "IZ AG") return 1;

                    if (((TreeNode)x_).Name == "IZ UY") return -1;
                    if (((TreeNode)y_).Name == "IZ UY") return 1;

                    x = ((TreeNode)x_).Name;
                    string[] dizi_g = x.Split(' ');
                    x = dizi_g[0] + " " + dizi_g[1] + " " + (Convert.ToInt32(dizi_g[2])).ToString("000");
                    
                    y = ((TreeNode)y_).Name;
                    dizi_g = y.Split(' ');
                    y = dizi_g[0] + " " + dizi_g[1] + " " + (Convert.ToInt32(dizi_g[2])).ToString("000");
                }
                else
                {
                    x = ((TreeNode)x_).Text;
                    y = ((TreeNode)y_).Text;
                }

                int[] dizi = new int[] { x.Length, y.Length};

                for (int i = 0; i < dizi.Min(); i++)
                {
                    if (x[i] == y[i]) continue;
                    else if (x[i] < y[i]) return -1;
                    else return 1;
                }

                if (x.Length == y.Length) return 0;
                if (x.Length < y.Length) return -1;
                return 1;
            }
        }

        #endregion
        private void Ağaç_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Ağaç.SelectedNode = e.Node;

            if (e.Node == null || e.Node.Tag == null) return;

            if (e.Node.Tag.GetType() == typeof(_Ayar_Cihaz_))
            {
                _Ayar_Cihaz_ ayar_cihaz = e.Node.Tag as _Ayar_Cihaz_;

                switch (ayar_cihaz.Tipi)
                {
                    case Cihaz_Tip.SeriPort:
                        return;

                    case Cihaz_Tip.AğTcpSunucu:
                    case Cihaz_Tip.AğTcpİstemci:
                    case Cihaz_Tip.AğUdpDinleyiciGönderici:
                    case Cihaz_Tip.AğUdpGönderici:
                    case Cihaz_Tip.KomutSatırı:
                        if (!ayar_cihaz.Etkin)
                        {
                            O.DurumBildirimi.BaloncukluUyarı("Durgun, sağ tuş menüsünden etkinleştirilebilir");
                            return;
                        }

                        if (ayar_cihaz.Cihazı == null)
                        {
                            ayar_cihaz.Cihazı = new _Cihaz();
                            ayar_cihaz.Cihazı.Ayarı = ayar_cihaz;
                            ayar_cihaz.Cihazı.Başlık = string.IsNullOrEmpty(ayar_cihaz.Başlık) ? "" : ayar_cihaz.Başlık + " ";
                            Cihazlar.EtkinCihazlar.Add(ayar_cihaz.Cihazı);
                            ayar_cihaz.Cihazı.Dalı = ayar_cihaz.Dalı;
                        }

                        Cihazlar.Eleman_Başlat(ayar_cihaz.Cihazı);
                        if (ayar_cihaz.Tipi == Cihaz_Tip.AğUdpGönderici) Bağlantılar_Uygulama.AraHedefCihaz = ayar_cihaz.Ağİstemci.IpVeyaAdresi + ":" + ayar_cihaz.Ağİstemci.ErişimNoktası;
                        else Bağlantılar_Uygulama.AraHedefCihaz = "";
                        break;
                } 
            }
            else if (e.Node.Tag.GetType() == typeof(_Cihaz))
            {
                _Cihaz cihaz = e.Node.Tag as _Cihaz;

                switch (cihaz.Ayarı.Tipi)
                {
                    case Cihaz_Tip.SeriPort:
                    case Cihaz_Tip.AğTcpİstemci:
                    case Cihaz_Tip.AğUdpGönderici:
                        if (Bağlantılar_Tarama.Durumu() == Bağlantılar_Tarama.Durum.EnAzBirCihazBulundu) Cihazlar.Eleman_Başlat(cihaz);
                        break;

                    case Cihaz_Tip.AğTcpSunucu:
                    case Cihaz_Tip.AğUdpDinleyiciGönderici:
                        Bağlantılar_Uygulama.AraHedefCihaz = e.Node.Text;
                        break;
                }
            }
        }
    }
}
