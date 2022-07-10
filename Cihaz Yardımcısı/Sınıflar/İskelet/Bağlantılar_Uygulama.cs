// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using System;
using System.Drawing;

namespace Cihaz_Yardımcısı
{
    class Bağlantılar_Uygulama
    {
        public static string DosyaKayıtKlasörü = "";
        public static Font GörselDüzen = null;
        public static string Başlık = "";
        public static string AraHedefCihaz = "";

        public delegate void Çağırılacak_Islem_(string Gelen);
        public static Çağırılacak_Islem_ Çağırılacak_Islem_CihazDeğişti = null;
        public static Çağırılacak_Islem_ Çağırılacak_Islem_BilgiGeldi = null;
        public static Çağırılacak_Islem_ Çağırılacak_Islem_Kapatıldı = null;

        public static void Gönder(string Komut)
        {
            switch (Bağlantılar_Tarama.Durumu())
            {
                case Bağlantılar_Tarama.Durum.CihazBulunamadı:
                    throw new Exception("Kullanılabilir bir cihaz veya uygulama mevcut değil, soldaki ağaca sağ tıklayıp YENİLE tuşuna basınız");
                case Bağlantılar_Tarama.Durum.Taranıyor:
                    throw new Exception("Tarama işlemi bitene kadar bekleyiniz");
            }
            
            if (Cihazlar.GeçerliCihaz.Ayarı.Tipi != Cihaz_Tip.AğUdpDinleyiciGönderici && Cihazlar.GeçerliCihaz.Ayarı.Tipi != Cihaz_Tip.AğUdpGönderici)
            {
                if (Cihazlar.GeçerliCihaz == null || Cihazlar.GeçerliCihaz.Aracı == null || !Cihazlar.GeçerliCihaz.Aracı.BağlantıKurulduMu())
                {
                    throw new Exception("Seçili cihaz henüz kullanıma hazır değil, bağlamtı kurulana kadar ( resmi yeşile döner ) bekleyiniz");
                }
            }

            Cihazlar.GeçerliCihaz.Aracı.Gönder(Komut + " ", AraHedefCihaz);
        }
    }
}
