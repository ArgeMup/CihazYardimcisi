// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using ArgeMup.HazirKod;
using System.Net;
using System.Windows.Forms;

namespace Cihaz_Yardımcısı
{
    class O
    {
        public static UygulamaOncedenCalistirildiMi_ UygulamaOncedenCalistirildiMi;
        public static PencereVeTepsiIkonuKontrolu_ UygulamaKontrolu;
        public static DurumBildirimi_ DurumBildirimi;
        public static Ayarlar_ Ayarlar;

        public static bool Çalışıyor = true;
        public static string pak;
        public static object KullanıcınınSeçtiğiUygulama;
        public static string KullanıcınınSeçtiğiUygulama_Adı = "";
    }
}
