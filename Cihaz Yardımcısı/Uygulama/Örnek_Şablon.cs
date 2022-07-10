// Copyright ArgeMup GNU GENERAL PUBLIC LICENSE Version 3 <http://www.gnu.org/licenses/> <https://github.com/ArgeMup/CihazYardimcisi>

using System;
using System.Windows.Forms;
using ArgeMup.HazirKod.Dönüştürme;

namespace Cihaz_Yardımcısı.Uygulama
{
    public partial class Örnek_Şablon : UserControl
    {
        public const string Sürüm = "V1.0";

        #region Değişkenler
        //Kullanılabilir nesneler Bağlantılar_Uygulama içerisinde
        #endregion

        public Örnek_Şablon( /*Font*/ )
        {
            InitializeComponent();

            Bağlantılar_Uygulama.Çağırılacak_Islem_CihazDeğişti = Çağırılacak_Islem_CihazDeğişti;
            Bağlantılar_Uygulama.Çağırılacak_Islem_BilgiGeldi = Çağırılacak_Islem_BilgiGeldi;
            Bağlantılar_Uygulama.Çağırılacak_Islem_Kapatıldı = Çağırılacak_Islem_Kapatıldı;
        }
        void Çağırılacak_Islem_CihazDeğişti(string Gelen)
        {
            Console.WriteLine("Çağırılacak_Islem_CihazDeğişti " + Gelen);
        }
        void Çağırılacak_Islem_BilgiGeldi(string Gelen)
        {
            Console.WriteLine("Çağırılacak_Islem_BilgiGeldi " + Gelen);
        }
        private void Çağırılacak_Islem_Kapatıldı(string Gelen)
        {
            Console.WriteLine("Çağırılacak_Islem_Kapatıldı");
        }
        
        ///////////////////////////////////////////////////////////////////////////////////////////////
        
    }
}
