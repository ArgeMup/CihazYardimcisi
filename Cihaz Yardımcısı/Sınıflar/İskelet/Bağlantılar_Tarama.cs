// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Net.Sockets;
using ArgeMup.HazirKod.DonanımHaberleşmesi;

namespace Cihaz_Yardımcısı
{
    class Bağlantılar_Tarama
    {
        public static bool Çalışsın;
        public static List<Task> Tarayıcılar = new List<Task>();
        public enum Durum { CihazBulunamadı, Taranıyor, EnAzBirCihazBulundu };

        public static void Başlat()
        {
            Durdur();

            foreach (var b in Cihazlar.EtkinCihazlar)
            {
                if (b.Dalı.Tag.GetType() == typeof(_Ayar_Cihaz_))
                {
                    _Ayar_Cihaz_ cihaz_ayar = b.Dalı.Tag as _Ayar_Cihaz_;
                    cihaz_ayar.Cihazı = null;
                    Görseller.Dal_Değiştir(cihaz_ayar.Dalı, null, null, Görseller.Resim_Bul(cihaz_ayar.Tipi, "m"));
                    foreach (TreeNode dal in cihaz_ayar.Dalı.Nodes)
                    {
                        Görseller.Dal_Sil(dal);
                    }
                }
                else b.Dalı.Remove();
                
                if (b.Aracı != null) b.Aracı.Durdur();
            }
            Cihazlar.EtkinCihazlar.Clear();

            Çalışsın = true;

            foreach (string sp in SerialPort.GetPortNames())
            {
                new Bağlantılar_Tarama_SeriPort(sp);
            }

            Tarayıcılar.ForEach(x => x.Start());
        }
        public static Durum Durumu()
        {
            if (Tarayıcılar.Count > 0)
            {
                if (!Task.WaitAll(Tarayıcılar.ToArray(), 10)) return Durum.Taranıyor;
                else
                {
                    foreach (var nesne in Tarayıcılar) nesne.Dispose();
                    Tarayıcılar.Clear();
                }
            }

            if (Cihazlar.EtkinCihazlar.Count > 0) return Durum.EnAzBirCihazBulundu;
            
            return Durum.CihazBulunamadı;
        }
        public static void Durdur()
        {
            //wait diyince bitenlere dair bilgi döncek
            Çalışsın = false;
            Task.WaitAll(Tarayıcılar.ToArray(), 1000);

            for (int i = 0; i < Tarayıcılar.Count; i++) { try { Tarayıcılar[i].Dispose(); Tarayıcılar[i] = null; } catch (Exception) { } }
            Tarayıcılar.Clear();
        }

        public static string KomutÜret(string Başlık, int Adres, string Komut)
        {
            return (string.IsNullOrEmpty(Başlık) ? "" : Başlık + " " + Adres + " ") + Komut + " ";
        }
    }

    class Bağlantılar_Tarama_SeriPort
    {
        SeriPort_ doha = null;
        IDonanımHaberlleşmesi aracı = null;
        string GelenCevap;

        public Bağlantılar_Tarama_SeriPort(string ErişimNoktası)
        {
            Action<object> _Görev = _Görev = (object Kaynak) => { _Görev_İşlem_((string)Kaynak); };

            Task Yeni = new Task(_Görev, ErişimNoktası);
            Bağlantılar_Tarama.Tarayıcılar.Add(Yeni);
        }

        public void _Görev_İşlem_(string ErişimNoktası)
        {
            TreeNode dal = null;

            foreach (var cihaz in Cihazlar.Ayarlar_Cihazlar.Values)
            {
                try
                {
                    if (!Bağlantılar_Tarama.Çalışsın) break;
                    if (cihaz.Tipi != Cihaz_Tip.SeriPort || !cihaz.Etkin) continue;

                    dal = Görseller.Dal_Ekle(ErişimNoktası + " - Açılıyor", "", Görseller.Resim.Meşgul, null, cihaz.Dalı);

                    aracı = null;
                    doha = new SeriPort_(ErişimNoktası, cihaz.SeriPort.BaudRate, GeriBildirim_Islemi_,
                            null, true, 5000, cihaz.ZamanAşımı_Gönderme_msn, cihaz.SeriPort.DataBits, cihaz.SeriPort.Parity, cihaz.SeriPort.StopBits);
                    if (doha == null)
                    {
                        Görseller.Dal_Değiştir(dal, ErişimNoktası + " - Oluşturulamadı", "", Görseller.Resim.Durgun);
                        Thread.Sleep(1500);
                        Görseller.Dal_Sil(dal);
                        return;
                    }

                    //bağlantının kurulmasını bekle
                    int za = Environment.TickCount + 1500;
                    while (za > Environment.TickCount && aracı == null && Bağlantılar_Tarama.Çalışsın) Thread.Sleep(100);
                    if (aracı == null)
                    {
                        Görseller.Dal_Değiştir(dal, ErişimNoktası + " - Açılamadı", "", Görseller.Resim.Durgun);
                        Thread.Sleep(1500);
                        Görseller.Dal_Sil(dal);
                        doha.Dispose();
                        return;
                    }

                    //uyandırma
                    Görseller.Dal_Değiştir(dal, ErişimNoktası + " - Uyandırma ");
                    za = Environment.TickCount + cihaz.ZamanAşımı_Uyandırma_msn;
                    string komut = Bağlantılar_Tarama.KomutÜret(cihaz.Başlık, 0, "YANKI");
                    bool uyandı = false;
                    while (za > Environment.TickCount && !uyandı && Bağlantılar_Tarama.Çalışsın)
                    {
                        if (Gönder_Al(komut, cihaz.ZamanAşımı_İlkTarama_Alma_msn) == "TAMAM") uyandı = true;
                        Görseller.Dal_Değiştir(dal, dal.Text + ".");
                    }

                    //tarama
                    int başla = 1, bitir = 6;
                    uyandı = false;
                    while (başla < bitir)
                    {
                        komut = Bağlantılar_Tarama.KomutÜret(cihaz.Başlık, başla, "YANKI");
                        Görseller.Dal_Değiştir(dal, ErişimNoktası + " - " + komut);
                        if (Gönder_Al(komut, cihaz.ZamanAşımı_İlkTarama_Alma_msn) == "TAMAM")
                        {
                            uyandı = true;

                            Cihazlar.Ekle_Cihaz_Eleman(cihaz, ErişimNoktası);

                            if (string.IsNullOrEmpty(cihaz.Başlık)) break;
                        }

                        başla++;
                        if (başla == bitir && uyandı)
                        {
                            bitir += 5;
                            uyandı = false;
                        }
                    }
                }
                catch (Exception) { }

                Görseller.Dal_Sil(dal);
                if (doha != null) doha.Dispose();
            }
        }

        string Gönder_Al(string Komut, int ZamanAşımı = 500)
        {
            GelenCevap = "";
            aracı.Gönder(Komut);

            int za = Environment.TickCount + ZamanAşımı;
            while (za > Environment.TickCount && Bağlantılar_Tarama.Çalışsın)
            {
                if (!string.IsNullOrEmpty(GelenCevap)) return GelenCevap;
                
                Thread.Sleep(50);
            }

            return "";
        }
        void GeriBildirim_Islemi_(string Kaynak, GeriBildirim_Türü_ Tür, object İçerik, object Hatırlatıcı)
        {
            if (!Bağlantılar_Tarama.Çalışsın) return;

            switch (Tür)
            {
                case GeriBildirim_Türü_.BilgiGeldi:
                    string gelen = İçerik as string;
                    if (gelen.StartsWith(">>")) GelenCevap = gelen.Trim('>', '\r');
                    break;

                case GeriBildirim_Türü_.BağlantıKuruldu:
                    aracı = doha;
                    break;

                case GeriBildirim_Türü_.BağlantıKoptu:
                    aracı = null;
                    break;      
            }
        }
    }
}
