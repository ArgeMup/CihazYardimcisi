// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using System.Collections.Generic;
using ArgeMup.HazirKod.Dönüştürme;
using ArgeMup.HazirKod.DonanımHaberleşmesi;
using System.Windows.Forms;
using System;
using System.IO;

namespace Cihaz_Yardımcısı
{
    public enum Cihaz_Tip { SeriPort, AğTcpSunucu, AğTcpİstemci, AğUdpDinleyiciGönderici, AğUdpGönderici, KomutSatırı };

    public class Cihazlar
    {
        public static string[] Cihaz_Tip_İsim = new string[] { "Seri Port", "Tcp Sunucu", "Tcp İstemci", "Udp Dinleyici Gönderici", "Udp Gönderici", "Komut Satırı" };
        public static List<_Cihaz> EtkinCihazlar = new List<_Cihaz> { };
        public static Dictionary<string, _Ayar_Cihaz_> Ayarlar_Cihazlar = null;
        public static Dictionary<string, _Ayar_Bağlantı_> Ayarlar_Bağlantılar = null;

        public static void Başlat()
        {
            EtkinCihazlar.Clear();

            if (Ayarlar_Bağlantılar != null)
            {
                Ayarlar_Bağlantılar.Clear();
                Ayarlar_Bağlantılar = null;
            }
            if (Ayarlar_Cihazlar != null)
            {
                Ayarlar_Cihazlar.Clear();
                Ayarlar_Cihazlar = null;
            }

            //Bağlantılar
            string okunan = O.Ayarlar.Oku("Ayarlar_Bağlantılar");
            if (!string.IsNullOrEmpty(okunan))
            {
                try
                {
                    Ayarlar_Bağlantılar = D_Nesne.BaytDizisinden(D_HexYazı.BaytDizisine(okunan)) as Dictionary<string, _Ayar_Bağlantı_>;
                }
                catch (Exception) { }
            }
            if (Ayarlar_Bağlantılar == null)
            {
                Ayarlar_Bağlantılar = new Dictionary<string, _Ayar_Bağlantı_>();
                for (int i = 0; i < Cihaz_Tip_İsim.Length; i++)
                {
                    _Ayar_Bağlantı_ yeni = new _Ayar_Bağlantı_()
                    {
                        Adı = Cihaz_Tip_İsim[i],
                        Tipi = (Cihaz_Tip)i
                    };

                    Ayarlar_Bağlantılar.Add(yeni.Adı, yeni);
                }
            }
            
            //Cihazlar
            okunan = O.Ayarlar.Oku("Ayarlar_Cihazlar");
            if (!string.IsNullOrEmpty(okunan))
            {
                try
                {
                    Ayarlar_Cihazlar = D_Nesne.BaytDizisinden(D_HexYazı.BaytDizisine(okunan)) as Dictionary<string, _Ayar_Cihaz_>;
                }
                catch (Exception) { }
            }
            if (Ayarlar_Cihazlar == null)
            {
                Ayarlar_Cihazlar = new Dictionary<string, _Ayar_Cihaz_>();

                Ekle_Cihaz(false, Cihaz_Tip.SeriPort, "CTC1  921600 8 Yok 1 1500 500 500");
                Ekle_Cihaz(false, Cihaz_Tip.SeriPort, "CTC2 CTC2 115200 8 Yok 1 1500 500 500");
            }

            Görseller.YenidenÇizdir();
            Bağlantılar_Tarama.Başlat();
        }
        public static void Durdur()
        {
            foreach (var b in EtkinCihazlar)
            {
                if (b.Aracı != null) b.Aracı.Durdur();
            }
        }
        static _Ayar_Cihaz_ Ekle_Cihaz(bool KullanıcıEkledi, Cihaz_Tip Tip, string BağlantıyaÖzgüDetaylar, string SonEklemeYazısı = "")
        {
            if (string.IsNullOrEmpty(BağlantıyaÖzgüDetaylar)) throw new Exception("BağlantıyaÖzgüDetaylar boş olamaz");

            if (!BağlantıyaÖzgüDetaylar.Contains("/")) BağlantıyaÖzgüDetaylar = BağlantıyaÖzgüDetaylar.Replace(" ", " / ");
            string[] dizi = BağlantıyaÖzgüDetaylar.Split('/');
            for (int i = 0; i < dizi.Length; i++) dizi[i] = dizi[i].Trim();

            _Ayar_Cihaz_ chz = new _Ayar_Cihaz_
            {
                Adı = dizi[0],
                Başlık = dizi[1],
                Tipi = Tip,
                KullanıcıEkledi = KullanıcıEkledi,
                SonEklemeYazısı = SonEklemeYazısı,
                ZamanAşımı_Uyandırma_msn = int.Parse(dizi[dizi.Length - 3]),
                ZamanAşımı_İlkTarama_Alma_msn = int.Parse(dizi[dizi.Length - 2]),
                ZamanAşımı_Gönderme_msn = int.Parse(dizi[dizi.Length - 1])
            };

            chz.İpucu = ( string.IsNullOrEmpty(dizi[1]) ? "Başlıksız" : dizi[1] ) + " ";
            for (int i = 2; i < dizi.Length - 3; i++) chz.İpucu += dizi[i] + " ";
            chz.İpucu += Environment.NewLine + "Uyandırma:" + chz.ZamanAşımı_Uyandırma_msn + "msn, Tarama alma:" + chz.ZamanAşımı_İlkTarama_Alma_msn + "msn, Gönderme:" + chz.ZamanAşımı_Gönderme_msn + "msn";

            switch (Tip)
            {
                case Cihaz_Tip.SeriPort:
                    chz.SeriPort = new _Ayar_Cihaz_SeriPort_();
                    chz.SeriPort.BaudRate = int.Parse(dizi[2]);
                    chz.SeriPort.DataBits = int.Parse(dizi[3]);
                    chz.SeriPort.Parity = dizi[4];
                    chz.SeriPort.StopBits = double.Parse(dizi[5]);
                    break;

                case Cihaz_Tip.AğTcpSunucu:
                case Cihaz_Tip.AğUdpDinleyiciGönderici:
                    chz.AğSunucu = new _Ayar_Cihaz_AğSunucu_();
                    chz.AğSunucu.ErişimNoktası = int.Parse(dizi[2]);
                    break;

                case Cihaz_Tip.AğTcpİstemci:
                case Cihaz_Tip.AğUdpGönderici:
                    chz.Ağİstemci = new _Ayar_Cihaz_Ağİstemci_();
                    chz.Ağİstemci.IpVeyaAdresi = dizi[2];
                    chz.Ağİstemci.ErişimNoktası = int.Parse(dizi[3]);
                    break;

                case Cihaz_Tip.KomutSatırı:
                    chz.KomutSatırıUygulaması = new _Ayar_Cihaz_KomutSatırıUygulaması_();
                    chz.KomutSatırıUygulaması.DosyaYolu = dizi[2];
                    chz.KomutSatırıUygulaması.Parametreleri = dizi[3];
                    break;
            }

            Ayarlar_Cihazlar.Add(dizi[0], chz);
            return chz;
        }
        public static void Ekle_Cihaz_Eleman(_Ayar_Cihaz_ Ayarı, string CihazaÖzgüDetaylar)
        {
            //sp  COMx 1 255
            //ağs kullanılmıyor, yapı içinde
            //aği 1.1.1.253 6
            //ks  kullanılmıyor, yapı içinde

            if (string.IsNullOrEmpty(CihazaÖzgüDetaylar)) throw new Exception("CihazaÖzgüDetaylar boş olamaz");

            if (!CihazaÖzgüDetaylar.Contains("/")) CihazaÖzgüDetaylar = CihazaÖzgüDetaylar.Replace(" ", " / ");
            string[] dizi = CihazaÖzgüDetaylar.Split('/');
            for (int i = 0; i < dizi.Length; i++) dizi[i] = dizi[i].Trim();

            _Cihaz chz;
            switch (Ayarı.Tipi)
            {
                case Cihaz_Tip.SeriPort:
                    if (string.IsNullOrEmpty(Ayarı.Başlık))
                    {
                        chz = new _Cihaz();
                        chz.Ayarı = Ayarı;
                        chz.İpucu += CihazaÖzgüDetaylar;

                        chz.Başlık = "";
                        EtkinCihazlar.Add(chz);
                        chz.Dalı = Görseller.Dal_Ekle("Başlıksız", chz.İpucu, Görseller.Resim.SeriPort_Kırmızı, chz, Ayarı.Dalı);
                    }
                    else
                    {
                        for (int i = int.Parse(dizi[1]); i < int.Parse(dizi[1]) + int.Parse(dizi[2]); i++)
                        {
                            chz = new _Cihaz();
                            chz.Ayarı = Ayarı;
                            chz.İpucu += CihazaÖzgüDetaylar;

                            chz.Başlık = Ayarı.Başlık + " " + i + " ";
                            EtkinCihazlar.Add(chz);
                            chz.Dalı = Görseller.Dal_Ekle(chz.Başlık, chz.İpucu, Görseller.Resim.SeriPort_Kırmızı, chz, Ayarı.Dalı);
                        }
                    }
                    break;

                case Cihaz_Tip.AğTcpİstemci:
                case Cihaz_Tip.AğUdpGönderici:
                    int adet = int.Parse(dizi[1]);
                    dizi = dizi[0].Split('.');
                    int adr_3 = int.Parse(dizi[0]);
                    int adr_2 = int.Parse(dizi[1]);
                    int adr_1 = int.Parse(dizi[2]);
                    int adr_0 = int.Parse(dizi[3]);
                    for (int i = 0; i < adet; i++)
                    {
                        chz = new _Cihaz();
                        chz.Ayarı = Ayarı;
                        chz.İpucu += adr_3 + "." + adr_2 + "." + adr_1 + "." + adr_0;
                        if (++adr_0 == 256)
                        {
                            adr_0 = 0;
                            if (++adr_1 == 256)
                            {
                                adr_1 = 0;
                                if (++adr_2 == 256)
                                {
                                    adr_2 = 0;
                                    ++adr_3;
                                }
                            }
                        }

                        chz.Başlık = string.IsNullOrEmpty(Ayarı.Başlık) ? "" : Ayarı.Başlık + " ";
                        EtkinCihazlar.Add(chz);
                        chz.Dalı = Görseller.Dal_Ekle(chz.Başlık, chz.İpucu, Görseller.Resim.Ağ_Kırmızı, chz, Ayarı.Dalı);
                    }
                    break;
            }
        }

        public static bool Kullanıcıİsteği_Ekle(object Hedef, string Detaylar)
        {
            try
            {
                if (Hedef is _Ayar_Bağlantı_)
                {
                    //sp  CTCx CTCx 921600 8 Yok 1 1000 1500 500
                    //ağs CTCx CTCx 9999 1000 1500 500
                    //aği CTCx CTCx 127.0.0.1 9999 5000 1000 1500 500
                    //ks  CTCx / CTCx / C:\\Uygulama.exe / -a 100 -b 500 / 1000 / 1500 / 500

                    _Ayar_Bağlantı_ bağlantı = Hedef as _Ayar_Bağlantı_;
                    bağlantı.SonEklemeYazısı = Detaylar;

                    _Ayar_Cihaz_ chz = Ekle_Cihaz(true, bağlantı.Tipi, Detaylar);
                    chz.Dalı = Görseller.Dal_Ekle(chz.Adı, chz.İpucu, Görseller.Resim_Bul(chz.Tipi, "m"), chz, Ayarlar_Bağlantılar[Cihaz_Tip_İsim[(int)chz.Tipi]].Dalı);
                }
                else if (Hedef is _Ayar_Cihaz_)
                {
                    //sp  COMx 1 255
                    //ağs kullanılmıyor
                    //aği 1.1.1.253 6
                    //ks  kullanılmıyor

                    _Ayar_Cihaz_ cihaz = Hedef as _Ayar_Cihaz_;
                    cihaz.SonEklemeYazısı = Detaylar;

                    Ekle_Cihaz_Eleman(cihaz, Detaylar);
                }

                Kullanıcıİsteği_Kaydet();
                return true;
            }
            catch (Exception) { return false; }
        }
        public static void Kullanıcıİsteği_Sil(_Ayar_Cihaz_ Cihaz)
        {
            Ayarlar_Cihazlar.Remove(Cihaz.Adı);
            Kullanıcıİsteği_Kaydet();
        }
        public static void Kullanıcıİsteği_Kaydet()
        {
            //Bağlantılar
            string okunan = D_HexYazı.BaytDizisinden(D_Nesne.BaytDizisine(Ayarlar_Bağlantılar));
            O.Ayarlar.Yaz("Ayarlar_Bağlantılar", okunan);

            //Cihazlar
            okunan = D_HexYazı.BaytDizisinden(D_Nesne.BaytDizisine(Ayarlar_Cihazlar));
            O.Ayarlar.Yaz("Ayarlar_Cihazlar", okunan);

            O.Ayarlar.DeğişiklikleriKaydet();
        }

        public static _Cihaz GeçerliCihaz = null;
        public static void Eleman_Başlat(_Cihaz Cihaz)
        {
            if (GeçerliCihaz == Cihaz) return;

            if (GeçerliCihaz != null)
            {
                GeçerliCihaz.Aracı.Durdur();
                GeçerliCihaz.Aracı = null;
                System.Threading.Thread.Sleep(500);
            }

            string[] dizi = Cihaz.İpucu.Split('/');
            for (int i = 0; i < dizi.Length; i++) dizi[i] = dizi[i].Trim();

            switch (Cihaz.Ayarı.Tipi)
            {
                case Cihaz_Tip.SeriPort:
                    Cihaz.SeriPort = new SeriPort_(dizi[0], Cihaz.Ayarı.SeriPort.BaudRate, GeriBildirim_Islemi_,
                        Cihaz.Dalı, true, 1000, Cihaz.Ayarı.ZamanAşımı_Gönderme_msn, Cihaz.Ayarı.SeriPort.DataBits, Cihaz.Ayarı.SeriPort.Parity, Cihaz.Ayarı.SeriPort.StopBits);
                    Cihaz.Aracı = Cihaz.SeriPort;
                    break;

                case Cihaz_Tip.AğTcpSunucu:
                    Cihaz.TcpSunucu = new TcpSunucu_(Cihaz.Ayarı.AğSunucu.ErişimNoktası, GeriBildirim_Islemi_,
                        Cihaz.Dalı, true, 1000, Cihaz.Ayarı.ZamanAşımı_Gönderme_msn, false);
                    Cihaz.Aracı = Cihaz.TcpSunucu;
                    Görseller.Dal_Değiştir(Cihaz.Dalı, "", "", Görseller.Resim.Ağ_Yeşil);
                    break;

                case Cihaz_Tip.AğTcpİstemci:
                    Cihaz.Tcpİstemci = new Tcpİstemci_(Cihaz.Ayarı.Ağİstemci.ErişimNoktası, Cihaz.Ayarı.Ağİstemci.IpVeyaAdresi, GeriBildirim_Islemi_,
                        Cihaz.Dalı, true, 1000, Cihaz.Ayarı.ZamanAşımı_Gönderme_msn);
                    Cihaz.Aracı = Cihaz.Tcpİstemci;
                    break;

                case Cihaz_Tip.AğUdpDinleyiciGönderici:
                    Cihaz.UdpDinleyici = new UdpDinleyici_(Cihaz.Ayarı.AğSunucu.ErişimNoktası, GeriBildirim_Islemi_,
                        Cihaz.Dalı, true, 1000, Cihaz.Ayarı.ZamanAşımı_Gönderme_msn, false);
                    Cihaz.Aracı = Cihaz.UdpDinleyici;
                    Görseller.Dal_Değiştir(Cihaz.Dalı, "", "", Görseller.Resim.Ağ_Yeşil);
                    break;

                case Cihaz_Tip.AğUdpGönderici:
                    Cihaz.UdpDinleyici = new UdpDinleyici_(-1, GeriBildirim_Islemi_,
                        Cihaz.Dalı, true, 1000, Cihaz.Ayarı.ZamanAşımı_Gönderme_msn, false);
                    Cihaz.Aracı = Cihaz.UdpDinleyici;
                    Görseller.Dal_Değiştir(Cihaz.Dalı, "", "", Görseller.Resim.Ağ_Yeşil);
                    break;

                case Cihaz_Tip.KomutSatırı:
                    Cihaz.KomutSatırıUygulaması = new KomutSatırıUygulaması_(Cihaz.Ayarı.KomutSatırıUygulaması.DosyaYolu, Cihaz.Ayarı.KomutSatırıUygulaması.Parametreleri, GeriBildirim_Islemi_,
                        Cihaz.Dalı, true, 1000);
                    Cihaz.Aracı = Cihaz.KomutSatırıUygulaması;
                    break;
            }

            GeçerliCihaz = Cihaz;
            Bağlantılar_Uygulama.Başlık = Cihaz.Başlık;
            if (Bağlantılar_Uygulama.Çağırılacak_Islem_CihazDeğişti != null) Bağlantılar_Uygulama.Çağırılacak_Islem_CihazDeğişti(Cihaz.Ayarı.Adı);
        }
        static void GeriBildirim_Islemi_(string Kaynak, GeriBildirim_Türü_ Tür, object İçerik, object Hatırlatıcı)
        {
            if (!O.Çalışıyor) return;

            TreeNode tn = Hatırlatıcı as TreeNode, mevcut_olan = null;
            _Cihaz chz = tn.Tag as _Cihaz, chz_yeni;
            _Ayar_Cihaz_ ayr = tn.Tag as _Ayar_Cihaz_;
            Cihaz_Tip tip = chz == null ? ayr.Tipi : chz.Ayarı.Tipi;

            switch (Tür)
            {
                case GeriBildirim_Türü_.BilgiGeldi:
                    switch (tip)
                    {
                        case Cihaz_Tip.AğUdpDinleyiciGönderici:
                            foreach (TreeNode dal in tn.Nodes)
                            {
                                if (dal.Text == Kaynak)
                                {
                                    mevcut_olan = dal;
                                }
                                else Görseller.Dal_Değiştir(dal, null, null, Görseller.Resim_Bul(tip, "k"));
                            }
                            if (mevcut_olan == null)
                            {
                                chz_yeni = new _Cihaz();
                                chz_yeni.Ayarı = ayr;
                                chz_yeni.İpucu += "";

                                chz_yeni.Başlık = "";
                                EtkinCihazlar.Add(chz_yeni);
                                chz_yeni.Dalı = Görseller.Dal_Ekle(Kaynak, chz_yeni.İpucu, Görseller.Resim_Bul(tip, "y"), chz_yeni, tn);
                                mevcut_olan = chz_yeni.Dalı;
                            }
                            Görseller.Dal_Değiştir(mevcut_olan, null, null, Görseller.Resim_Bul(tip, "y"));
                            Görseller.Dal_Değiştir(tn, null, null, Görseller.Resim_Bul(tip, "y"));
                            break;
                    }

                    if (Bağlantılar_Uygulama.Çağırılacak_Islem_BilgiGeldi != null) Bağlantılar_Uygulama.Çağırılacak_Islem_BilgiGeldi(İçerik as string);
                    break;

                case GeriBildirim_Türü_.BağlantıKuruldu:
                    switch (tip)
                    {
                        case Cihaz_Tip.AğTcpSunucu:
                            foreach (TreeNode dal in tn.Nodes)
                            {
                                if (dal.Text == Kaynak)
                                {
                                    mevcut_olan = dal;
                                    break;
                                }
                            }
                            if (mevcut_olan == null)
                            {
                                chz_yeni = new _Cihaz();
                                chz_yeni.Ayarı = ayr;
                                chz_yeni.İpucu += "";

                                chz_yeni.Başlık = "";
                                EtkinCihazlar.Add(chz_yeni);
                                chz_yeni.Dalı = Görseller.Dal_Ekle(Kaynak, chz_yeni.İpucu, Görseller.Resim_Bul(tip, "y"), chz_yeni, tn);
                                mevcut_olan = chz_yeni.Dalı;
                            }
                            Görseller.Dal_Değiştir(mevcut_olan, null, null, Görseller.Resim_Bul(tip, "y"));
                            Görseller.Dal_Değiştir(tn, null, null, Görseller.Resim_Bul(tip, "y"));
                            return;
                    }

                    Görseller.Dal_Değiştir(tn, null, Kaynak, Görseller.Resim_Bul(tip, "y"));
                    break;
                    
                case GeriBildirim_Türü_.BağlantıKoptu:
                case GeriBildirim_Türü_.Durduruldu:
                    switch (tip)
                    {
                        case Cihaz_Tip.AğTcpSunucu:
                        case Cihaz_Tip.AğUdpDinleyiciGönderici:
                            bool tümünü = true;
                            if (Kaynak.Contains(":")) tümünü = false;
                            
                            foreach (TreeNode dal in tn.Nodes)
                            {
                                if (tümünü || dal.Text == Kaynak) Görseller.Dal_Değiştir(dal, "", "", Görseller.Resim_Bul(tip, "k"));
                            }
                            if (!tümünü) return;
                            break;
                    }

                    Görseller.Dal_Değiştir(tn, null, Kaynak, Görseller.Resim_Bul(tip, "k"));
                    break;
            }
        }
    }

    #region Ayarlar
    [Serializable]
    public struct _Ayar_Cihaz_SeriPort_
    {
        public int BaudRate;
        public int DataBits;
        public string Parity;
        public double StopBits;
    }
    [Serializable]
    public struct _Ayar_Cihaz_AğSunucu_
    {
        public int ErişimNoktası;
    }
    [Serializable]
    public struct _Ayar_Cihaz_Ağİstemci_
    {
        public string IpVeyaAdresi;
        public int ErişimNoktası;
        public int İlkBağlantıyıKurmaZamanAşımı;
    }
    [Serializable]
    public struct _Ayar_Cihaz_KomutSatırıUygulaması_
    {
        public string DosyaYolu;
        public string Parametreleri;
    }

    [Serializable]
    public class _Ayar_Cihaz_
    {
        public bool Etkin = true;
        public string Adı;
        public string Başlık;
        public Cihaz_Tip Tipi;
        public bool KullanıcıEkledi;
        public string SonEklemeYazısı;
        public string İpucu;

        public int ZamanAşımı_Uyandırma_msn;
        public int ZamanAşımı_İlkTarama_Alma_msn;
        public int ZamanAşımı_Gönderme_msn;

        public _Ayar_Cihaz_SeriPort_ SeriPort;
        public _Ayar_Cihaz_AğSunucu_ AğSunucu;
        public _Ayar_Cihaz_Ağİstemci_ Ağİstemci;
        public _Ayar_Cihaz_KomutSatırıUygulaması_ KomutSatırıUygulaması;

        [NonSerialized]
        public TreeNode Dalı;
        [NonSerialized]
        public _Cihaz Cihazı;
    }

    [Serializable]
    public class _Ayar_Bağlantı_
    {
        public bool Etkin = true;
        public string Adı = "";
        public Cihaz_Tip Tipi;
        
        public string SonEklemeYazısı = "";

        [NonSerialized]
        public TreeNode Dalı = null;
    }
    #endregion

    #region Cihazlar
    public class _Cihaz
    {
        public TreeNode Dalı;
        public string Başlık;
        public string İpucu = "";
        public _Ayar_Cihaz_ Ayarı;

        public IDonanımHaberlleşmesi Aracı;
        public SeriPort_ SeriPort;
        public TcpSunucu_ TcpSunucu;
        public Tcpİstemci_ Tcpİstemci;
        public UdpDinleyici_ UdpDinleyici;
        public KomutSatırıUygulaması_ KomutSatırıUygulaması;
    }
    #endregion
}
