// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using System;
using System.Windows.Forms;

namespace Cihaz_Yardımcısı
{
    class Görseller
    {
        public enum Resim
        {
            SeriPort_Yeşil, SeriPort_Kırmızı, SeriPort_Mavi, Ağ_Yeşil, Ağ_Kırmızı, Ağ_Mavi, Uygulama_Yeşil, Uygulama_Kırmızı, Uygulama_Mavi,
            Durgun, Meşgul, Seçili, Bulunanlar, İzinKontrolu, İzinAraMal, Değiştirme
        };
        static TreeView Ağaç;

        public static void Başlat(TreeView AğaçGörseli)
        {
            Ağaç = AğaçGörseli;
        }

        public static void YenidenÇizdir()
        {
            Ağaç.Nodes.Clear();
            Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.SeriPort]].Dalı = Dal_Ekle(Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.SeriPort], "Seri port cihazı ayarları buradan değiştirilebilir", Resim.SeriPort_Mavi, Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.SeriPort]], null);
            Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğTcpSunucu]].Dalı = Dal_Ekle(Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğTcpSunucu], "Bilgisayar sunucu olarak çalışacaktır", Resim.Ağ_Mavi, Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğTcpSunucu]], null);
            Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğTcpİstemci]].Dalı = Dal_Ekle(Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğTcpİstemci], "Bilgisayar istemci olarak çalışacaktır", Resim.Ağ_Mavi, Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğTcpİstemci]], null);
            Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğUdpDinleyiciGönderici]].Dalı = Dal_Ekle(Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğUdpDinleyiciGönderici], "Bilgisayar dinleyici olarak çalışacak ve sadece bilgi gönderenlere bilgi gönderebilecektir", Resim.Ağ_Mavi, Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğUdpDinleyiciGönderici]], null);
            Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğUdpGönderici]].Dalı = Dal_Ekle(Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğUdpGönderici], "Bilgisayar gönderici olarak çalışacaktır", Resim.Ağ_Mavi, Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.AğUdpGönderici]], null);
            Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.KomutSatırı]].Dalı = Dal_Ekle(Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.KomutSatırı], "Komut satırı uygulaması ayarları buradan değiştirilebilir", Resim.Uygulama_Mavi, Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)Cihaz_Tip.KomutSatırı]], null);

            foreach (var biri in Cihazlar.Ayarlar_Cihazlar)
            {
                biri.Value.Dalı = Dal_Ekle(biri.Value.Adı, biri.Value.İpucu, Görseller.Resim_Bul(biri.Value.Tipi, biri.Value.Etkin ? "m" : "k"), biri.Value, Cihazlar.Ayarlar_Bağlantılar[Cihazlar.Cihaz_Tip_İsim[(int)biri.Value.Tipi]].Dalı);
            }

            Ağaç.ExpandAll();
        }
        
        public static TreeNode Dal_Ekle(string GörünenYazı, string İpucu, Resim Görsel, object Etiket, TreeNode ÜstDal)
        {
            TreeNode dal = null;

            if (Ağaç.InvokeRequired)
            {
                Ağaç.Invoke((Action)(() =>
                {
                    dal = _Dal_Ekle_(GörünenYazı, İpucu, Görsel, Etiket, ÜstDal);
                }));
            }
            else dal = _Dal_Ekle_(GörünenYazı, İpucu, Görsel, Etiket, ÜstDal);

            return dal;
        }
        static TreeNode _Dal_Ekle_(string GörünenYazı, string İpucu, Resim Görsel, object Etiket, TreeNode ÜstDal)
        {
            TreeNode yeni_dal;
            if (ÜstDal == null) yeni_dal = Ağaç.Nodes.Add(GörünenYazı);
            else yeni_dal = ÜstDal.Nodes.Add(GörünenYazı);

            if (yeni_dal.Parent != null) yeni_dal.Parent.Expand();

            yeni_dal.Tag = Etiket;
            yeni_dal.ToolTipText = İpucu;
            yeni_dal.ImageIndex = (int)Görsel;
            yeni_dal.SelectedImageIndex = (int)Görsel;

            return yeni_dal;
        }
        public static void Dal_Değiştir(TreeNode Dal, string GörünenYazısı = null, string İpucu = null, Resim Görsel = Resim.Değiştirme)
        {
            if (Dal == null) return;

            if (Ağaç.InvokeRequired)
            {
                Ağaç.Invoke((Action)(() =>
                {
                    _Dal_Değiştir_(Dal, GörünenYazısı, İpucu, Görsel);
                }));
            }
            else _Dal_Değiştir_(Dal, GörünenYazısı, İpucu, Görsel);
        }
        static void _Dal_Değiştir_(TreeNode Dal, string GörünenYazısı, string İpucu, Resim Görsel)
        {
            if (!string.IsNullOrEmpty(GörünenYazısı)) Dal.Text = GörünenYazısı;
            if (!string.IsNullOrEmpty(İpucu)) Dal.ToolTipText = İpucu;
            if (Görsel != Resim.Değiştirme)
            {
                Dal.ImageIndex = (int)Görsel;
                Dal.SelectedImageIndex = (int)Görsel;
            }
        }
        public static void Dal_Sil(TreeNode Dal)
        {
            if (Dal == null) return;

            if (Ağaç.InvokeRequired)
            {
                Ağaç.Invoke((Action)(() =>
                {
                    Dal.Remove();
                }));
            }
            else Dal.Remove();
        }

        public static Resim Resim_Bul(Cihaz_Tip Tip, string k_m_y)
        {
            switch (k_m_y)
            {
                case "k":
                    switch (Tip)
                    {
                        case Cihaz_Tip.SeriPort: return Resim.SeriPort_Kırmızı;
                        case Cihaz_Tip.AğTcpSunucu: return Resim.Ağ_Kırmızı;
                        case Cihaz_Tip.AğTcpİstemci: return Resim.Ağ_Kırmızı;
                        case Cihaz_Tip.AğUdpDinleyiciGönderici: return Resim.Ağ_Kırmızı;
                        case Cihaz_Tip.AğUdpGönderici: return Resim.Ağ_Kırmızı;
                        case Cihaz_Tip.KomutSatırı: return Resim.Uygulama_Kırmızı;
                    }
                    break;

                case "m":
                    switch (Tip)
                    {
                        case Cihaz_Tip.SeriPort: return Resim.SeriPort_Mavi;
                        case Cihaz_Tip.AğTcpSunucu: return Resim.Ağ_Mavi;
                        case Cihaz_Tip.AğTcpİstemci: return Resim.Ağ_Mavi;
                        case Cihaz_Tip.AğUdpDinleyiciGönderici: return Resim.Ağ_Mavi;
                        case Cihaz_Tip.AğUdpGönderici: return Resim.Ağ_Mavi;
                        case Cihaz_Tip.KomutSatırı: return Resim.Uygulama_Mavi;
                    }
                    break;

                case "y":
                    switch (Tip)
                    {
                        case Cihaz_Tip.SeriPort: return Resim.SeriPort_Yeşil;
                        case Cihaz_Tip.AğTcpSunucu: return Resim.Ağ_Yeşil;
                        case Cihaz_Tip.AğTcpİstemci: return Resim.Ağ_Yeşil;
                        case Cihaz_Tip.AğUdpDinleyiciGönderici: return Resim.Ağ_Yeşil;
                        case Cihaz_Tip.AğUdpGönderici: return Resim.Ağ_Yeşil;
                        case Cihaz_Tip.KomutSatırı: return Resim.Uygulama_Yeşil;
                    }
                    break;
            }

            return Resim.Değiştirme;
        }
    }
}
