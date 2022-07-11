// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using ArgeMup.HazirKod;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using ArgeMup.HazirKod.Dönüştürme;

namespace Cihaz_Yardımcısı.Uygulama
{
    public partial class Gönder_Al : UserControl
    {
        public const string Sürüm = "V1.2";

        #region Değişkenler
        Ayarlar_ Ayarlar;
        Gönderi_ Gönderi;
        enum MenüNeredeAçıldı_ { Girdi, Çıktı, Çıktı2 };
        MenüNeredeAçıldı_ MenüNeredeAçıldı;
        UInt32 Sayac_Tur = 0, Sayac_CevapVeren = 0, Sayac_ZamanAşımıOluşan = 0;
        UInt32 Sayac_Tur_2 = 0, Sayac_CevapVeren_2 = 0, Sayac_ZamanAşımıOluşan_2 = 0;

        struct Gönderi_
        {
            public Task Görev;
            public bool AcilDur;
            public int GönderimAnı;

            const int Anlık = 5;
            public int AnlıkBekleme()
            {
                return Anlık;
            }
            public void Bekle(int msn)
            {
                for (; msn > Anlık && !AcilDur; msn -= Anlık) { Thread.Sleep(Anlık); }
                if (!AcilDur) Thread.Sleep(msn);
            }
        }

        [Serializable]
        class Şablon_
        {
            public string CihazVeyaUygulamanınAdı = "";
            public string SeçiliOlanınAdı = "";
            public decimal Bekleme_İkiKomutArası = 500;
            public decimal Bekleme_KomutaCevap = 1000;
            public bool SürekliDevamEt = false;
            public bool ÇıktıyıTemizle = false;
            public bool MetniKaydır = false;
            public bool Başlıklı = false;
            public bool Girdili = true;

            [NonSerialized]
            public string Klasörü;
        }
        Şablon_ Şablon = null;
        #endregion

        public Gönder_Al( /*Font GörselDüzen*/ )
        {
            InitializeComponent();

            Bağlantılar_Uygulama.Çağırılacak_Islem_CihazDeğişti = Çağırılacak_Islem_CihazDeğişti;
            Bağlantılar_Uygulama.Çağırılacak_Islem_BilgiGeldi = Çağırılacak_Islem_BilgiGeldi;
            Bağlantılar_Uygulama.Çağırılacak_Islem_Kapatıldı = Çağırılacak_Islem_Kapatıldı;
        }
        private void Gönder_Al_Load(object sender, EventArgs e)
        {
            Ayarlar = new Ayarlar_(out _, "", Bağlantılar_Uygulama.DosyaKayıtKlasörü + this.Name + ".Ayarlar");

            splitContainer1.SplitterDistance = splitContainer1.Height / 5;
            splitContainer2.SplitterDistance = splitContainer2.Width / 2;
            Girdi.Text = Environment.NewLine + "Cihaz veya uygulama seçildiğinde boş alanlar doldurulacaktır";
        }
        void Çağırılacak_Islem_CihazDeğişti(string Gelen)
        {
            if (Şablon != null && !string.IsNullOrEmpty(Şablon.CihazVeyaUygulamanınAdı))
            {
                Şablon.Bekleme_İkiKomutArası = Bekleme_İkiKomutArası.Value;
                Şablon.Bekleme_KomutaCevap = Bekleme_KomutaCevap.Value;
                Şablon.SürekliDevamEt = Menü1_SürekliDevamEt.Checked;
                Şablon.ÇıktıyıTemizle = Menü1_ÇıktıyıTemizle.Checked;
                Şablon.MetniKaydır = Menü1_MetniKaydır.Checked;
                Şablon.Başlıklı = Menü1_Başlıklı.Checked;
                Şablon.Girdili = Menü1_Girdili.Checked;
                try { File.WriteAllText(Şablon.Klasörü + Şablon.SeçiliOlanınAdı, Girdi.Text); } catch (Exception) { }

                Ayarlar.Yaz("Şablon " + Şablon.CihazVeyaUygulamanınAdı, D_HexYazı.BaytDizisinden(D_Nesne.BaytDizisine(Şablon)));
            }

            string okunan = Ayarlar.Oku("Şablon " + Gelen);
            Şablon = null;
            if (!string.IsNullOrEmpty(okunan))
            {
                try
                {
                    Şablon = D_Nesne.BaytDizisinden(D_HexYazı.BaytDizisine(okunan)) as Şablon_;
                }
                catch (Exception) { }
            }
            if (Şablon == null) { Şablon = new Şablon_(); Şablon.CihazVeyaUygulamanınAdı = Gelen; }

            Şablon.Klasörü = Bağlantılar_Uygulama.DosyaKayıtKlasörü + "Şablon\\" + D_DosyaKlasörAdı.Düzelt(Şablon.CihazVeyaUygulamanınAdı) + "\\";
            Directory.CreateDirectory(Şablon.Klasörü);

            ŞablonListesi.Items.Clear();
            foreach (var dosya in Directory.GetFiles(Şablon.Klasörü)) if (!Path.GetFileNameWithoutExtension(dosya).StartsWith("Şablon")) ŞablonListesi.Items.Add(Path.GetFileNameWithoutExtension(dosya));
            for (int i = 1; i < 21; i++) ŞablonListesi.Items.Add("Şablon " + i);

            Bekleme_İkiKomutArası.Value = Şablon.Bekleme_İkiKomutArası;
            Bekleme_KomutaCevap.Value = Şablon.Bekleme_KomutaCevap;
            Menü1_SürekliDevamEt.Checked = Şablon.SürekliDevamEt;
            Menü1_ÇıktıyıTemizle.Checked = Şablon.ÇıktıyıTemizle;
            Menü1_MetniKaydır.Checked = Şablon.MetniKaydır;
            Menü1_Başlıklı.Checked = Şablon.Başlıklı;
            Menü1_Girdili.Checked = Şablon.Girdili;
            Menü1_MetniKaydır_Click(null, null);
            ŞablonAdı.Text = Path.GetFileNameWithoutExtension(Şablon.SeçiliOlanınAdı);
            if (File.Exists(Şablon.Klasörü + Şablon.SeçiliOlanınAdı)) Girdi.Text = File.ReadAllText(Şablon.Klasörü + Şablon.SeçiliOlanınAdı);
            ŞablonListesi.SelectedIndex = ŞablonListesi.Items.IndexOf(ŞablonAdı.Text);

            Enabled = true;
        }
        void Çağırılacak_Islem_BilgiGeldi(string Gelen)
        {
            Gelen = Gelen.TrimEnd('\r', '\n', '\0');

            if (Gelen.StartsWith(">>"))
            {
                if (Gönderi.GönderimAnı > 0)
                {
                    ÇıktıYaz(Gelen + " (" + (Environment.TickCount - Gönderi.GönderimAnı) + "msn)" + Environment.NewLine);
                    Gönderi.GönderimAnı = 0;
                    Sayac_CevapVeren++; Sayac_CevapVeren_2++;
                }
                else ÇıktıYaz(Gelen + Environment.NewLine);
            }
            else Çıktı2Yaz(Gelen + Environment.NewLine);
        }
        void Çağırılacak_Islem_Kapatıldı(string Gelen)
        {
            Çağırılacak_Islem_CihazDeğişti("");

            Ayarlar.Dispose(); Ayarlar = null;
            if (Gönderi.Görev != null) { Gönderi.Görev.Dispose(); Gönderi.Görev = null; }
        }

        private void Menü1_Opening(object sender, CancelEventArgs e)
        {
            if (((ContextMenuStrip)sender).SourceControl.Name == "Girdi") MenüNeredeAçıldı = MenüNeredeAçıldı_.Girdi;
            else if (((ContextMenuStrip)sender).SourceControl.Name == "Çıktı") MenüNeredeAçıldı = MenüNeredeAçıldı_.Çıktı;
            else MenüNeredeAçıldı = MenüNeredeAçıldı_.Çıktı2;
        }
        private void Menü1_MetniKaydır_Click(object sender, EventArgs e)
        {
            bool Evet = false;
            if (Menü1_MetniKaydır.Checked) Evet = true;

            Girdi.WordWrap = Evet;
            Çıktı.WordWrap = Evet;

            if (Çıktı2Filtre.Tag == null) Çıktı2.WordWrap = Evet;
            else
            {
                foreach (RichTextBox biri in (Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>).Values)
                {
                    biri.WordWrap = Evet;
                }
            }
        }
        private void Menü1_Komutlar_Bekle_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Komutlar;Bekle;100 //sonraki satıra geçmeden önce fazladan 100 msn bekler" + Environment.NewLine);
        }
        private void Menü1_Komutlar_ÇıktıPunto_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Komutlar;ÇıktıPunto;12" + Environment.NewLine);
        }
        private void Menü1_Sabitler_İkiKomutArasındaBekleme_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Sabitler;İkiKomutArasıBekleme;100 //Seçenekler 0 ile 999999 msn arasında" + Environment.NewLine);
        }
        private void Menü1_Sabitler_KomutCevapZamanAşımı_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Sabitler;KomutaCevapZamanAşımı;100 //Seçenekler 1 ile 60000 msn arasında" + Environment.NewLine);
        }
        private void Menü1_Sabitler_Başlıklı_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Sabitler;Başlıklı;E //Seçenekler E:Evet H:Hayır" + Environment.NewLine);
        }
        private void Menü1_Sabitler_Girdili_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Sabitler;Girdili;E //Seçenekler E:Evet H:Hayır" + Environment.NewLine);
        }
        private void Menü1_Şablonlar_UygulamaGönderisi_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Şablonlar;UyGö;.;Alıcı;Konu;Mesaj" + Environment.NewLine);
        }
        private void Menü1_Şablonlar_CevabıHexOlarakGöster_Click(object sender, EventArgs e)
        {
            Girdi.AppendText(">>>Şablonlar;CeHeOlGö;YANKI;7 //>>TAMAM\\r\\n için 9" + Environment.NewLine);
        }
        private void Menü1_Kes_Click(object sender, EventArgs e)
        {
            switch (MenüNeredeAçıldı)
            {
                case MenüNeredeAçıldı_.Girdi: 
                    if (Girdi.SelectedText != "") { Clipboard.SetText(Girdi.SelectedText); Girdi.SelectedText = ""; } 
                    break;

                case MenüNeredeAçıldı_.Çıktı:
                    if (Çıktı.SelectedText != "") { Clipboard.SetText(Çıktı.SelectedText); Çıktı.SelectedText = ""; }
                    break;

                case MenüNeredeAçıldı_.Çıktı2:
                    RichTextBox biri;
                    if (Çıktı2Filtre.Tag == null) biri = Çıktı2;
                    else biri = (Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>)[splitContainer2.Panel2.Controls[1].Name];
                   
                    if (biri.SelectedText != "") { Clipboard.SetText(biri.SelectedText); biri.SelectedText = ""; }
                    break;
            }
        }
        private void Menü1_Kopyala_Click(object sender, EventArgs e)
        {
            switch (MenüNeredeAçıldı)
            {
                case MenüNeredeAçıldı_.Girdi:
                    if (Girdi.SelectedText != "") Clipboard.SetText(Girdi.SelectedText);
                    break;

                case MenüNeredeAçıldı_.Çıktı:
                    if (Çıktı.SelectedText != "") Clipboard.SetText(Çıktı.SelectedText);
                    break;

                case MenüNeredeAçıldı_.Çıktı2:
                    RichTextBox biri;
                    if (Çıktı2Filtre.Tag == null) biri = Çıktı2;
                    else biri = (Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>)[splitContainer2.Panel2.Controls[1].Name];

                    if (biri.SelectedText != "") Clipboard.SetText(biri.SelectedText);
                    break;
            }
        }
        private void Menü1_Yapıştır_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetText().Length == 0) return;
            int bitiş;

            switch (MenüNeredeAçıldı)
            {
                case MenüNeredeAçıldı_.Girdi:
                    bitiş = Girdi.SelectionStart + Clipboard.GetText().Length;
                    Girdi.Text = Girdi.Text.Insert(Girdi.SelectionStart, Clipboard.GetText()); 
                    Girdi.SelectionStart = bitiş;
                    break;

                case MenüNeredeAçıldı_.Çıktı:
                    bitiş = Çıktı.SelectionStart + Clipboard.GetText().Length;
                    Çıktı.Text = Çıktı.Text.Insert(Çıktı.SelectionStart, Clipboard.GetText());
                    Çıktı.SelectionStart = bitiş;
                    break;

                case MenüNeredeAçıldı_.Çıktı2:
                    RichTextBox biri;
                    if (Çıktı2Filtre.Tag == null) biri = Çıktı2;
                    else biri = (Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>)[splitContainer2.Panel2.Controls[1].Name];

                    bitiş = biri.SelectionStart + Clipboard.GetText().Length;
                    biri.Text = biri.Text.Insert(biri.SelectionStart, Clipboard.GetText());
                    biri.SelectionStart = bitiş;
                    break;
            }
        }
        private void Menü1_TümünüSeç_Click(object sender, EventArgs e)
        {
            switch (MenüNeredeAçıldı)
            {
                case MenüNeredeAçıldı_.Girdi:
                    Girdi.SelectAll();
                    break;

                case MenüNeredeAçıldı_.Çıktı:
                    Çıktı.SelectAll();
                    break;

                case MenüNeredeAçıldı_.Çıktı2:
                    RichTextBox biri;
                    if (Çıktı2Filtre.Tag == null) biri = Çıktı2;
                    else biri = (Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>)[splitContainer2.Panel2.Controls[1].Name];

                    biri.SelectAll();
                    break;
            }
        }
        private void Menü1_Temizle_Click(object sender, EventArgs e)
        {
            switch (MenüNeredeAçıldı)
            {
                case MenüNeredeAçıldı_.Girdi:
                    Girdi.Clear();
                    break;

                case MenüNeredeAçıldı_.Çıktı:
                case MenüNeredeAçıldı_.Çıktı2:
                    splitContainer2.Panel2Collapsed = true;
                    Çıktı.Clear();
                    Çıktı2.Clear();
                    splitContainer2.Panel2.Controls.RemoveAt(1);
                    splitContainer2.Panel2.Controls.Add(Çıktı2);
                    if (Çıktı2Filtre.Tag != null) (Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>).Clear();
                    Çıktı2Filtre.Tag = null;
                    Çıktı2Filtre.Visible = false;
                    Çıktı2Filtre.Items.Clear();
                    break;
            }
        }
        private void Menü1_Başlıklı_Click(object sender, EventArgs e)
        {
            string[] dizi = Girdi.Lines;

            for (int i = 0; i < dizi.Length; i++)
            {
                if (string.IsNullOrEmpty(dizi[i])) continue;

                if (Menü1_Başlıklı.Checked)
                {
                    if (dizi[i].StartsWith(Bağlantılar_Uygulama.Başlık)) dizi[i] = dizi[i].Remove(0, Bağlantılar_Uygulama.Başlık.Length);
                }
                else
                {
                    if (!dizi[i].StartsWith(Bağlantılar_Uygulama.Başlık)) dizi[i] = Bağlantılar_Uygulama.Başlık + dizi[i];
                }
            }

            Girdi.Lines = dizi;
        }

        private void ŞablonListesi_Ekle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ŞablonAdı.Text)) return;
            if (ŞablonListesi.Items.IndexOf(ŞablonAdı.Text) > -1) return;
            if (ŞablonAdı.Text.StartsWith("Şablon")) ŞablonAdı.Text = "_" + ŞablonAdı.Text;

            File.WriteAllText(Şablon.Klasörü + ŞablonAdı.Text + ".txt", Girdi.Text);

            ŞablonListesi.SelectedIndexChanged -= ŞablonListesi_SelectedIndexChanged;
            ŞablonListesi.Items.Insert(0, ŞablonAdı.Text);
            ŞablonListesi.SelectedIndex = 0;
            Şablon.SeçiliOlanınAdı = ŞablonAdı.Text + ".txt";
            ŞablonListesi.SelectedIndexChanged += ŞablonListesi_SelectedIndexChanged;
        }
        private void ŞablonListesi_Sil_Click(object sender, EventArgs e)
        {
            if (ŞablonListesi.SelectedIndex == -1) return;
            if (ŞablonListesi.SelectedItem.ToString().StartsWith("Şablon")) return;

            if (File.Exists(Şablon.Klasörü + ŞablonListesi.SelectedItem.ToString() + ".txt")) File.Delete(Şablon.Klasörü + ŞablonListesi.SelectedItem.ToString() + ".txt");
            ŞablonListesi.Items.RemoveAt(ŞablonListesi.SelectedIndex);
        }
        private void ŞablonListesi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ŞablonListesi.SelectedIndex == -1) return;

            try { File.WriteAllText(Şablon.Klasörü + Şablon.SeçiliOlanınAdı, Girdi.Text); } catch (Exception) { }
            ŞablonAdı.Text = ŞablonListesi.SelectedItem.ToString();
            Şablon.SeçiliOlanınAdı = ŞablonAdı.Text + ".txt";

            string Yol = Şablon.Klasörü + Şablon.SeçiliOlanınAdı;
            if (File.Exists(Yol)) Girdi.Text = File.ReadAllText(Yol);
        }

        private void Gönder_Click(object sender, EventArgs e)
        {
            if (Gönderi.Görev != null)
            {
                if (Gönderi.Görev.Status == TaskStatus.Running)
                {
                    if (Gönder.Text.StartsWith("Bekleyiniz")) return;

                    Gönderi.AcilDur = true;
                    int Tick = Environment.TickCount + 15000;
                    while (Gönderi.Görev.Status == TaskStatus.Running && Tick > Environment.TickCount) { Thread.Sleep(Gönderi.AnlıkBekleme()); Gönder.Text = "Bekleyiniz " + (((Tick - Environment.TickCount)/1000)+1) + " sn"; Application.DoEvents(); }
                    Gönder.Text = "Gönder";

                    Gönder.Checked = false;
                    try { Gönderi.Görev.Dispose(); } catch (Exception) { }    
                    Gönderi.Görev = null;
                    return;
                }

                Gönderi.Görev.Dispose();
            }

            Gönder.Checked = true;
            Gönderi.AcilDur = false;

            Action<object> Gönder_Görevi___ = (object a) => { Görev__Gönder(); };
            Gönderi.Görev = new Task(Gönder_Görevi___, null);
            Gönderi.Görev.Start();   
        }
        void Görev__Gönder()
        {
            try
            {
                Sayac_Tur = 0; Sayac_CevapVeren = 0; Sayac_ZamanAşımıOluşan = 0;
                do
                {
                    Sayac_Tur++; Sayac_Tur_2++;

                    Çıktı.Invoke((Action)(() => {
                        if (Menü1_ÇıktıyıTemizle.Checked)
                        {
                            MenüNeredeAçıldı = MenüNeredeAçıldı_.Çıktı;
                            Menü1_Temizle_Click(null, null);
                        }
                        else Çıktı_Bilgi("----- " + DateTime.Now.ToString() + " -----");
                    }));

                    foreach (var satır in Girdi.Lines)
                    {
                        try
                        {
                            string Komut = satır;
                            int gecici_int = satır.IndexOf("//");
                            if (gecici_int >= 0) Komut = satır.Substring(0, gecici_int);
                            Komut = Komut.Trim();
                            if (Komut == "")
                            {
                                ÇıktıYaz(Environment.NewLine);
                                continue;
                            }

                            if (Komut.StartsWith(">>>Sabitler"))
                            {
                                string[] KomutListesi = Komut.Split(';');
                                Bekleme_İkiKomutArası.Invoke((Action)(() => 
                                {
                                    switch (KomutListesi[1])
                                    {
                                        case ("İkiKomutArasıBekleme"): Bekleme_İkiKomutArası.Value = Convert.ToInt32(KomutListesi[2]); break;
                                        case ("KomutaCevapZamanAşımı"): Bekleme_KomutaCevap.Value = Convert.ToInt32(KomutListesi[2]); break;

                                        case ("Başlıklı"):
                                            if (KomutListesi[2].StartsWith("E")) Menü1_Başlıklı.Checked = true;
                                            else Menü1_Başlıklı.Checked = false;
                                            break;

                                        case ("Girdili"):
                                            if (KomutListesi[2].StartsWith("E")) Menü1_Girdili.Checked = true;
                                            else Menü1_Girdili.Checked = false;
                                            break;
                                    }
                                }));
                                goto Bitir;
                            }
                            else if (Komut.StartsWith(">>>Komutlar"))
                            {
                                string[] KomutListesi = Komut.Split(';');
                                switch (KomutListesi[1])
                                {
                                    case ("ÇıktıPunto"): Çıktı.Invoke((Action)(() => { Çıktı.Font = new Font(Çıktı.Font.FontFamily, Convert.ToInt32(KomutListesi[2])); })); break;
                                    case ("Bekle"): Gönderi.Bekle(Convert.ToInt32(KomutListesi[2])); break;
                                }
                                goto Bitir;
                            }

                        YenidenDeneme:
                            if (Gönderi.AcilDur) break;
                            string Gönderilecek_Komut;
                            if (Menü1_Başlıklı.Checked) Gönderilecek_Komut = Bağlantılar_Uygulama.Başlık + Komut;
                            else Gönderilecek_Komut = Komut;
                            if (Menü1_Girdili.Checked) Çıktı_Bilgi(Gönderilecek_Komut);

                            Gönderi.GönderimAnı = Environment.TickCount;
                            Bağlantılar_Uygulama.Gönder(Gönderilecek_Komut);

                            int za = Environment.TickCount + (int)Bekleme_KomutaCevap.Value;
                            while (Gönderi.GönderimAnı > 0 && za > Environment.TickCount) Gönderi.Bekle(5);
                            if (Gönderi.GönderimAnı > 0)
                            {
                                Çıktı_Hata("--- Cevap Alınmadı ---");
                                Sayac_ZamanAşımıOluşan++; Sayac_ZamanAşımıOluşan_2++;
                                goto YenidenDeneme;
                            }
                            if (Girdi.Lines.Length > 1) Gönderi.Bekle((int)Bekleme_İkiKomutArası.Value);
                            Bitir:;
                        }
                        catch (Exception ex) { Çıktı_Hata(satır + " >>>" + ex.Message); Gönderi.Bekle(1000); }
                    }

                } while (Menü1_SürekliDevamEt.Checked && !Gönderi.AcilDur);
            }
            catch (Exception ex) { Çıktı_Hata(ex.Message); }

            Gönder.Invoke((Action)(() => { Gönder.Checked = false; }));
        }

        private void Çıktı2Filtre_SelectedIndexChanged(object sender, EventArgs e)
        {
            Dictionary<string, RichTextBox> Gruplama_listesi = Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>;
            splitContainer2.Panel2.Controls.RemoveAt(1);
            splitContainer2.Panel2.Controls.Add(Gruplama_listesi[Çıktı2Filtre.Text]);
        }
        void ÇıktıYaz(string Metin, Color MetinRengi = new Color(), Color ArkaFon = new Color())
        {
            if (MetinRengi.IsEmpty) MetinRengi = Color.Black;
            if (ArkaFon.IsEmpty) ArkaFon = Color.White;

            Çıktı.Invoke((Action)(() => 
            {
                Çıktı.SelectionColor = MetinRengi;
                Çıktı.SelectionBackColor = ArkaFon;
                Çıktı.AppendText(Metin);
                Çıktı.ScrollToCaret();
            }));
        }
        void Çıktı2Yaz(string Metin)
        {
            Color MetinRengi = Color.White;
            Color ArkaFon = Color.Black;

            if (Metin[0] == '\u001b')
            {
                //Gunluk Eklentisi VT100 Terminali
                // \033[1;%d;%dmMesaj\033[0m -> \033 0x1b

                Color[] Renkler =
                {
                    Color.Black,
                    Color.Red,
                    Color.LimeGreen,
                    Color.Yellow,
                    Color.DodgerBlue,
                    Color.Magenta,
                    Color.Cyan,
                    Color.White
                };

                try
                {
                    int komum_m = Metin.IndexOf('m');
                    string[] renkler_yazı = Metin.Substring(4, komum_m - 4).Split(';');
                    Metin = Metin.Substring(komum_m + 1);
                    Metin = Metin.Remove(Metin.IndexOf('\u001b')) + Environment.NewLine;

                    MetinRengi = Renkler[Convert.ToInt32(renkler_yazı[0]) - 30];
                    ArkaFon = Renkler[Convert.ToInt32(renkler_yazı[1]) -  40];
                }
                catch (Exception) { }
            }

            RichTextBox Okunan_Grup_İçeriği = null;
            RichTextBox asıl_kutucuk = null;
            string[] Gruplama = Metin.Split(' ');
            if (Gruplama.Length > 2 && Gruplama[1].IndexOf(':') >= 0)
            {
                if (Çıktı2Filtre.Tag == null)
                {
                    Dictionary<string, RichTextBox> Gruplama_listesi_ = new Dictionary<string, RichTextBox>();
                    Çıktı2.Invoke((Action)(() =>
                    {
                        Gruplama_listesi_["***** Tümü *****"] = Çıktı2;
                        Çıktı2Filtre.Items.Add("***** Tümü *****");
                        Çıktı2Filtre.Tag = Gruplama_listesi_;
                    }));
                }

                string Grup = Gruplama[1].Substring(0, Gruplama[1].IndexOf(':'));
                Dictionary<string, RichTextBox> Gruplama_listesi = Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>;
                asıl_kutucuk = Gruplama_listesi["***** Tümü *****"];

                if (!string.IsNullOrEmpty(Grup))
                {
                    if (!Gruplama_listesi.TryGetValue(Grup, out Okunan_Grup_İçeriği))
                    {
                        Okunan_Grup_İçeriği = new RichTextBox();
                        Okunan_Grup_İçeriği.BackColor = Color.Black;
                        Okunan_Grup_İçeriği.ContextMenuStrip = Menü1;
                        Okunan_Grup_İçeriği.Dock = DockStyle.Fill;
                        Okunan_Grup_İçeriği.Font = asıl_kutucuk.Font;
                        Okunan_Grup_İçeriği.ForeColor = Color.White;
                        //Okunan_Grup_İçeriği.Location = new Point(0, 0);
                        Okunan_Grup_İçeriği.Margin = new Padding(3, 2, 3, 2);
                        Okunan_Grup_İçeriği.Name = Grup;
                        //Okunan_Grup_İçeriği.Size = new Size(173, 96);
                        //Okunan_Grup_İçeriği.TabIndex = 9;
                        Okunan_Grup_İçeriği.Text = "";
                        ipucu.SetToolTip(Okunan_Grup_İçeriği, "Cihazın " + Grup + " dalına ait hata ayıklama mesajları");
                        Gruplama_listesi[Grup] = Okunan_Grup_İçeriği;

                        Çıktı2Filtre.Invoke((Action)(() =>
                        {
                            Çıktı2Filtre.Items.Add(Grup);
                            Çıktı2Filtre.SelectedItem = Grup;
                        }));
                    }
                }
            }
            else
            {
                if (Çıktı2Filtre.Tag == null) asıl_kutucuk = Çıktı2;
                else asıl_kutucuk = (Çıktı2Filtre.Tag as Dictionary<string, RichTextBox>)["***** Tümü *****"];
            }

            Çıktı2Filtre.Invoke((Action)(() =>
            {
                splitContainer2.Panel2Collapsed = false;
                if (Çıktı2Filtre.Items.Count > 1) Çıktı2Filtre.Visible = true;
                if (Bekleme_KomutaCevap.BackColor == Color.MistyRose) Bekleme_KomutaCevap.BackColor = Color.LightCyan;
                else Bekleme_KomutaCevap.BackColor = Color.MistyRose;

                asıl_kutucuk.SelectionColor = MetinRengi;
                asıl_kutucuk.SelectionBackColor = ArkaFon;
                asıl_kutucuk.AppendText(Metin);
                asıl_kutucuk.ScrollToCaret();

                if (Okunan_Grup_İçeriği != null)
                {
                    Okunan_Grup_İçeriği.SelectionColor = MetinRengi;
                    Okunan_Grup_İçeriği.SelectionBackColor = ArkaFon;
                    Okunan_Grup_İçeriği.AppendText(Metin);
                    Okunan_Grup_İçeriği.ScrollToCaret();
                }
            }));
        }
        void Çıktı_Bilgi(string Metin)
        {
            ÇıktıYaz(Metin + Environment.NewLine, Color.Black, Color.YellowGreen);
        }
        void Çıktı_Uyarı(string Metin)
        {
            ÇıktıYaz(Metin + Environment.NewLine, Color.Black, Color.Gold);
        }
        void Çıktı_Hata(string Metin)
        {
            ÇıktıYaz(Metin + Environment.NewLine, Color.Black, Color.OrangeRed);
        }

        private void Dinleme_Tick(object sender, EventArgs e)
        {
            ipucu.SetToolTip(Çıktı, 
               "Son seferdeki sayımlar " + Environment.NewLine + Environment.NewLine +
               "Tur : " + Sayac_Tur + Environment.NewLine +
               //"Yeni bağlantı kurulan : " + Sayac_YeniBağlantı + Environment.NewLine +
               "Cevap veren : " + Sayac_CevapVeren + Environment.NewLine +
               "Zaman aşımına uğrayan : " + Sayac_ZamanAşımıOluşan + Environment.NewLine +
               Environment.NewLine +
               "Tüm sayımlar " + Environment.NewLine + Environment.NewLine +
               "Tur : " + Sayac_Tur_2 + Environment.NewLine +
               //"Yeni bağlantı kurulan : " + Sayac_YeniBağlantı_2 + Environment.NewLine +
               "Cevap veren : " + Sayac_CevapVeren_2 + Environment.NewLine +
               "Zaman aşımına uğrayan : " + Sayac_ZamanAşımıOluşan_2);
        }
    }
}
