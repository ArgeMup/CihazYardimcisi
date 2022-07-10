namespace Cihaz_Yardımcısı.Uygulama
{
    partial class Gönder_Al
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ŞablonListesi = new System.Windows.Forms.ListBox();
            this.Sil = new System.Windows.Forms.Button();
            this.ŞablonAdı = new System.Windows.Forms.TextBox();
            this.Ekle = new System.Windows.Forms.Button();
            this.Girdi = new System.Windows.Forms.TextBox();
            this.Menü1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menü1_MetniKaydır = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_ÇıktıyıTemizle = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_SürekliDevamEt = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menü1_Girdili = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Başlıklı = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Sabitler = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Sabitler_Girdili = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Sabitler_Başlıklı = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Sabitler_İkiKomutArasındaBekleme = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Sabitler_KomutCevapZamanAşımı = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Komutlar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Komutlar_Bekle = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Komutlar_ÇıktıPunto = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Şablonlar = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Şablonlar_UygulamaGönderisi = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Şablonlar_CevabıHexOlarakGöster = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menü1_Kes = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Kopyala = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Yapıştır = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_TümünüSeç = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü1_Temizle = new System.Windows.Forms.ToolStripMenuItem();
            this.Çıktı = new System.Windows.Forms.RichTextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Çıktı2Filtre = new System.Windows.Forms.ComboBox();
            this.Çıktı2 = new System.Windows.Forms.RichTextBox();
            this.Bekleme_İkiKomutArası = new System.Windows.Forms.NumericUpDown();
            this.Bekleme_KomutaCevap = new System.Windows.Forms.NumericUpDown();
            this.ipucu = new System.Windows.Forms.ToolTip(this.components);
            this.Gönder = new System.Windows.Forms.CheckBox();
            this.Dinleme = new System.Windows.Forms.Timer(this.components);
            this.Menü1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Bekleme_İkiKomutArası)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bekleme_KomutaCevap)).BeginInit();
            this.SuspendLayout();
            // 
            // ŞablonListesi
            // 
            this.ŞablonListesi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ŞablonListesi.FormattingEnabled = true;
            this.ŞablonListesi.HorizontalScrollbar = true;
            this.ŞablonListesi.ItemHeight = 16;
            this.ŞablonListesi.Location = new System.Drawing.Point(3, 2);
            this.ŞablonListesi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ŞablonListesi.Name = "ŞablonListesi";
            this.ŞablonListesi.Size = new System.Drawing.Size(103, 100);
            this.ŞablonListesi.TabIndex = 0;
            this.ipucu.SetToolTip(this.ŞablonListesi, "Şablonların listesi");
            this.ŞablonListesi.SelectedIndexChanged += new System.EventHandler(this.ŞablonListesi_SelectedIndexChanged);
            // 
            // Sil
            // 
            this.Sil.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Sil.Location = new System.Drawing.Point(57, 133);
            this.Sil.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Sil.Name = "Sil";
            this.Sil.Size = new System.Drawing.Size(48, 23);
            this.Sil.TabIndex = 3;
            this.Sil.Text = "Sil";
            this.ipucu.SetToolTip(this.Sil, "Listedeki seçili olan şablonu siler (Sadece kullanıcını eklediği şablonlar siline" +
        "bilir)");
            this.Sil.UseVisualStyleBackColor = true;
            this.Sil.Click += new System.EventHandler(this.ŞablonListesi_Sil_Click);
            // 
            // ŞablonAdı
            // 
            this.ŞablonAdı.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ŞablonAdı.Location = new System.Drawing.Point(3, 105);
            this.ŞablonAdı.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ŞablonAdı.Name = "ŞablonAdı";
            this.ŞablonAdı.Size = new System.Drawing.Size(103, 22);
            this.ŞablonAdı.TabIndex = 1;
            this.ipucu.SetToolTip(this.ŞablonAdı, "Eklenecek yeni şablonun adı veya geçerli olanın adı");
            // 
            // Ekle
            // 
            this.Ekle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Ekle.Location = new System.Drawing.Point(3, 133);
            this.Ekle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Ekle.Name = "Ekle";
            this.Ekle.Size = new System.Drawing.Size(48, 23);
            this.Ekle.TabIndex = 2;
            this.Ekle.Text = "Ekle";
            this.ipucu.SetToolTip(this.Ekle, "Yeni şablonu kaydeder");
            this.Ekle.UseVisualStyleBackColor = true;
            this.Ekle.Click += new System.EventHandler(this.ŞablonListesi_Ekle_Click);
            // 
            // Girdi
            // 
            this.Girdi.ContextMenuStrip = this.Menü1;
            this.Girdi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Girdi.Location = new System.Drawing.Point(0, 0);
            this.Girdi.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Girdi.Multiline = true;
            this.Girdi.Name = "Girdi";
            this.Girdi.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Girdi.Size = new System.Drawing.Size(257, 30);
            this.Girdi.TabIndex = 7;
            this.ipucu.SetToolTip(this.Girdi, "Girdi");
            // 
            // Menü1
            // 
            this.Menü1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menü1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menü1_MetniKaydır,
            this.Menü1_ÇıktıyıTemizle,
            this.Menü1_SürekliDevamEt,
            this.toolStripSeparator1,
            this.Menü1_Girdili,
            this.Menü1_Başlıklı,
            this.Menü1_Sabitler,
            this.Menü1_Komutlar,
            this.Menü1_Şablonlar,
            this.toolStripSeparator2,
            this.Menü1_Kes,
            this.Menü1_Kopyala,
            this.Menü1_Yapıştır,
            this.Menü1_TümünüSeç,
            this.Menü1_Temizle});
            this.Menü1.Name = "Menü_Girdi";
            this.Menü1.ShowCheckMargin = true;
            this.Menü1.ShowImageMargin = false;
            this.Menü1.Size = new System.Drawing.Size(189, 354);
            this.Menü1.Opening += new System.ComponentModel.CancelEventHandler(this.Menü1_Opening);
            // 
            // Menü1_MetniKaydır
            // 
            this.Menü1_MetniKaydır.CheckOnClick = true;
            this.Menü1_MetniKaydır.Name = "Menü1_MetniKaydır";
            this.Menü1_MetniKaydır.Size = new System.Drawing.Size(188, 26);
            this.Menü1_MetniKaydır.Text = "Metni kaydır";
            this.Menü1_MetniKaydır.ToolTipText = "Uzun satırların alt satırdan devam etmesini sağlar.";
            this.Menü1_MetniKaydır.Click += new System.EventHandler(this.Menü1_MetniKaydır_Click);
            // 
            // Menü1_ÇıktıyıTemizle
            // 
            this.Menü1_ÇıktıyıTemizle.CheckOnClick = true;
            this.Menü1_ÇıktıyıTemizle.Name = "Menü1_ÇıktıyıTemizle";
            this.Menü1_ÇıktıyıTemizle.Size = new System.Drawing.Size(188, 26);
            this.Menü1_ÇıktıyıTemizle.Text = "Çıktıyı temizle";
            this.Menü1_ÇıktıyıTemizle.ToolTipText = "Her yeni döngüde çıktıyı temizler.";
            // 
            // Menü1_SürekliDevamEt
            // 
            this.Menü1_SürekliDevamEt.CheckOnClick = true;
            this.Menü1_SürekliDevamEt.Name = "Menü1_SürekliDevamEt";
            this.Menü1_SürekliDevamEt.Size = new System.Drawing.Size(188, 26);
            this.Menü1_SürekliDevamEt.Text = "Sürekli devam et";
            this.Menü1_SürekliDevamEt.ToolTipText = "Girdiyi sonsuz tekrarlar.";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(185, 6);
            // 
            // Menü1_Girdili
            // 
            this.Menü1_Girdili.CheckOnClick = true;
            this.Menü1_Girdili.Name = "Menü1_Girdili";
            this.Menü1_Girdili.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Girdili.Text = "Girdili";
            this.Menü1_Girdili.ToolTipText = "Hem komut hem cevap ikiside çıktıya yazdırılır.";
            // 
            // Menü1_Başlıklı
            // 
            this.Menü1_Başlıklı.Checked = true;
            this.Menü1_Başlıklı.CheckOnClick = true;
            this.Menü1_Başlıklı.CheckState = System.Windows.Forms.CheckState.Checked;
            this.Menü1_Başlıklı.Name = "Menü1_Başlıklı";
            this.Menü1_Başlıklı.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Başlıklı.Text = "Başlıklı";
            this.Menü1_Başlıklı.ToolTipText = "Başlık (TS12 1) eklenir.  ";
            this.Menü1_Başlıklı.Click += new System.EventHandler(this.Menü1_Başlıklı_Click);
            // 
            // Menü1_Sabitler
            // 
            this.Menü1_Sabitler.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menü1_Sabitler_Girdili,
            this.Menü1_Sabitler_Başlıklı,
            this.Menü1_Sabitler_İkiKomutArasındaBekleme,
            this.Menü1_Sabitler_KomutCevapZamanAşımı});
            this.Menü1_Sabitler.Name = "Menü1_Sabitler";
            this.Menü1_Sabitler.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Sabitler.Text = "Sabitler";
            // 
            // Menü1_Sabitler_Girdili
            // 
            this.Menü1_Sabitler_Girdili.Name = "Menü1_Sabitler_Girdili";
            this.Menü1_Sabitler_Girdili.Size = new System.Drawing.Size(278, 26);
            this.Menü1_Sabitler_Girdili.Text = "Girdili";
            this.Menü1_Sabitler_Girdili.ToolTipText = "Komut satırından kontol.";
            this.Menü1_Sabitler_Girdili.Click += new System.EventHandler(this.Menü1_Sabitler_Girdili_Click);
            // 
            // Menü1_Sabitler_Başlıklı
            // 
            this.Menü1_Sabitler_Başlıklı.Name = "Menü1_Sabitler_Başlıklı";
            this.Menü1_Sabitler_Başlıklı.Size = new System.Drawing.Size(278, 26);
            this.Menü1_Sabitler_Başlıklı.Text = "Başlıklı";
            this.Menü1_Sabitler_Başlıklı.ToolTipText = "Komut satırından kontol.";
            this.Menü1_Sabitler_Başlıklı.Click += new System.EventHandler(this.Menü1_Sabitler_Başlıklı_Click);
            // 
            // Menü1_Sabitler_İkiKomutArasındaBekleme
            // 
            this.Menü1_Sabitler_İkiKomutArasındaBekleme.Name = "Menü1_Sabitler_İkiKomutArasındaBekleme";
            this.Menü1_Sabitler_İkiKomutArasındaBekleme.Size = new System.Drawing.Size(278, 26);
            this.Menü1_Sabitler_İkiKomutArasındaBekleme.Text = "İki komut arasında bekleme";
            this.Menü1_Sabitler_İkiKomutArasındaBekleme.ToolTipText = "Komut satırından kontol.";
            this.Menü1_Sabitler_İkiKomutArasındaBekleme.Click += new System.EventHandler(this.Menü1_Sabitler_İkiKomutArasındaBekleme_Click);
            // 
            // Menü1_Sabitler_KomutCevapZamanAşımı
            // 
            this.Menü1_Sabitler_KomutCevapZamanAşımı.Name = "Menü1_Sabitler_KomutCevapZamanAşımı";
            this.Menü1_Sabitler_KomutCevapZamanAşımı.Size = new System.Drawing.Size(278, 26);
            this.Menü1_Sabitler_KomutCevapZamanAşımı.Text = "Komuta cevap zaman aşımı ";
            this.Menü1_Sabitler_KomutCevapZamanAşımı.ToolTipText = "Komut satırından kontol.";
            this.Menü1_Sabitler_KomutCevapZamanAşımı.Click += new System.EventHandler(this.Menü1_Sabitler_KomutCevapZamanAşımı_Click);
            // 
            // Menü1_Komutlar
            // 
            this.Menü1_Komutlar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menü1_Komutlar_Bekle,
            this.Menü1_Komutlar_ÇıktıPunto});
            this.Menü1_Komutlar.Name = "Menü1_Komutlar";
            this.Menü1_Komutlar.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Komutlar.Text = "Komutlar";
            // 
            // Menü1_Komutlar_Bekle
            // 
            this.Menü1_Komutlar_Bekle.Name = "Menü1_Komutlar_Bekle";
            this.Menü1_Komutlar_Bekle.Size = new System.Drawing.Size(164, 26);
            this.Menü1_Komutlar_Bekle.Text = "Bekle";
            this.Menü1_Komutlar_Bekle.ToolTipText = "İki komut arası fazladan bekleme.";
            this.Menü1_Komutlar_Bekle.Click += new System.EventHandler(this.Menü1_Komutlar_Bekle_Click);
            // 
            // Menü1_Komutlar_ÇıktıPunto
            // 
            this.Menü1_Komutlar_ÇıktıPunto.Name = "Menü1_Komutlar_ÇıktıPunto";
            this.Menü1_Komutlar_ÇıktıPunto.Size = new System.Drawing.Size(164, 26);
            this.Menü1_Komutlar_ÇıktıPunto.Text = "Çıktı punto";
            this.Menü1_Komutlar_ÇıktıPunto.ToolTipText = "Çıktı karakter büyüklüğü.";
            this.Menü1_Komutlar_ÇıktıPunto.Click += new System.EventHandler(this.Menü1_Komutlar_ÇıktıPunto_Click);
            // 
            // Menü1_Şablonlar
            // 
            this.Menü1_Şablonlar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menü1_Şablonlar_UygulamaGönderisi,
            this.Menü1_Şablonlar_CevabıHexOlarakGöster});
            this.Menü1_Şablonlar.Name = "Menü1_Şablonlar";
            this.Menü1_Şablonlar.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Şablonlar.Text = "Şablonlar";
            this.Menü1_Şablonlar.Visible = false;
            // 
            // Menü1_Şablonlar_UygulamaGönderisi
            // 
            this.Menü1_Şablonlar_UygulamaGönderisi.Name = "Menü1_Şablonlar_UygulamaGönderisi";
            this.Menü1_Şablonlar_UygulamaGönderisi.Size = new System.Drawing.Size(255, 26);
            this.Menü1_Şablonlar_UygulamaGönderisi.Text = "Uygulama gönderisi";
            this.Menü1_Şablonlar_UygulamaGönderisi.Click += new System.EventHandler(this.Menü1_Şablonlar_UygulamaGönderisi_Click);
            // 
            // Menü1_Şablonlar_CevabıHexOlarakGöster
            // 
            this.Menü1_Şablonlar_CevabıHexOlarakGöster.Name = "Menü1_Şablonlar_CevabıHexOlarakGöster";
            this.Menü1_Şablonlar_CevabıHexOlarakGöster.Size = new System.Drawing.Size(255, 26);
            this.Menü1_Şablonlar_CevabıHexOlarakGöster.Text = "Cevabı hex olarak göster";
            this.Menü1_Şablonlar_CevabıHexOlarakGöster.Click += new System.EventHandler(this.Menü1_Şablonlar_CevabıHexOlarakGöster_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(185, 6);
            // 
            // Menü1_Kes
            // 
            this.Menü1_Kes.Name = "Menü1_Kes";
            this.Menü1_Kes.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Kes.Text = "Kes";
            this.Menü1_Kes.Click += new System.EventHandler(this.Menü1_Kes_Click);
            // 
            // Menü1_Kopyala
            // 
            this.Menü1_Kopyala.Name = "Menü1_Kopyala";
            this.Menü1_Kopyala.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Kopyala.Text = "Kopyala";
            this.Menü1_Kopyala.Click += new System.EventHandler(this.Menü1_Kopyala_Click);
            // 
            // Menü1_Yapıştır
            // 
            this.Menü1_Yapıştır.Name = "Menü1_Yapıştır";
            this.Menü1_Yapıştır.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Yapıştır.Text = "Yapıştır";
            this.Menü1_Yapıştır.Click += new System.EventHandler(this.Menü1_Yapıştır_Click);
            // 
            // Menü1_TümünüSeç
            // 
            this.Menü1_TümünüSeç.Name = "Menü1_TümünüSeç";
            this.Menü1_TümünüSeç.Size = new System.Drawing.Size(188, 26);
            this.Menü1_TümünüSeç.Text = "Tümünü seç";
            this.Menü1_TümünüSeç.Click += new System.EventHandler(this.Menü1_TümünüSeç_Click);
            // 
            // Menü1_Temizle
            // 
            this.Menü1_Temizle.Name = "Menü1_Temizle";
            this.Menü1_Temizle.Size = new System.Drawing.Size(188, 26);
            this.Menü1_Temizle.Text = "Temizle";
            this.Menü1_Temizle.Click += new System.EventHandler(this.Menü1_Temizle_Click);
            // 
            // Çıktı
            // 
            this.Çıktı.ContextMenuStrip = this.Menü1;
            this.Çıktı.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Çıktı.Location = new System.Drawing.Point(0, 0);
            this.Çıktı.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Çıktı.Name = "Çıktı";
            this.Çıktı.Size = new System.Drawing.Size(257, 96);
            this.Çıktı.TabIndex = 8;
            this.Çıktı.Text = "";
            this.ipucu.SetToolTip(this.Çıktı, "Çıktı");
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(109, 28);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Girdi);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(257, 128);
            this.splitContainer1.SplitterDistance = 30;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 8;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.Çıktı);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.Çıktı2Filtre);
            this.splitContainer2.Panel2.Controls.Add(this.Çıktı2);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(257, 96);
            this.splitContainer2.SplitterDistance = 126;
            this.splitContainer2.TabIndex = 9;
            // 
            // Çıktı2Filtre
            // 
            this.Çıktı2Filtre.Dock = System.Windows.Forms.DockStyle.Top;
            this.Çıktı2Filtre.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Çıktı2Filtre.FormattingEnabled = true;
            this.Çıktı2Filtre.Location = new System.Drawing.Point(0, 0);
            this.Çıktı2Filtre.Name = "Çıktı2Filtre";
            this.Çıktı2Filtre.Size = new System.Drawing.Size(96, 24);
            this.Çıktı2Filtre.Sorted = true;
            this.Çıktı2Filtre.TabIndex = 10;
            this.ipucu.SetToolTip(this.Çıktı2Filtre, "Gruplanmış hata ayıklama mesajları");
            this.Çıktı2Filtre.Visible = false;
            this.Çıktı2Filtre.SelectedIndexChanged += new System.EventHandler(this.Çıktı2Filtre_SelectedIndexChanged);
            // 
            // Çıktı2
            // 
            this.Çıktı2.BackColor = System.Drawing.Color.Black;
            this.Çıktı2.ContextMenuStrip = this.Menü1;
            this.Çıktı2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Çıktı2.Font = new System.Drawing.Font("Courier New", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Çıktı2.ForeColor = System.Drawing.Color.White;
            this.Çıktı2.Location = new System.Drawing.Point(0, 0);
            this.Çıktı2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Çıktı2.Name = "Çıktı2";
            this.Çıktı2.Size = new System.Drawing.Size(96, 100);
            this.Çıktı2.TabIndex = 9;
            this.Çıktı2.Text = "";
            this.ipucu.SetToolTip(this.Çıktı2, "Cihaza ait hata ayıklama mesajları");
            // 
            // Bekleme_İkiKomutArası
            // 
            this.Bekleme_İkiKomutArası.Location = new System.Drawing.Point(109, 2);
            this.Bekleme_İkiKomutArası.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Bekleme_İkiKomutArası.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.Bekleme_İkiKomutArası.Name = "Bekleme_İkiKomutArası";
            this.Bekleme_İkiKomutArası.Size = new System.Drawing.Size(85, 22);
            this.Bekleme_İkiKomutArası.TabIndex = 4;
            this.ipucu.SetToolTip(this.Bekleme_İkiKomutArası, "İki komut arasındaki bekleme msn");
            this.Bekleme_İkiKomutArası.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Bekleme_KomutaCevap
            // 
            this.Bekleme_KomutaCevap.Location = new System.Drawing.Point(200, 2);
            this.Bekleme_KomutaCevap.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Bekleme_KomutaCevap.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.Bekleme_KomutaCevap.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Bekleme_KomutaCevap.Name = "Bekleme_KomutaCevap";
            this.Bekleme_KomutaCevap.Size = new System.Drawing.Size(85, 22);
            this.Bekleme_KomutaCevap.TabIndex = 5;
            this.ipucu.SetToolTip(this.Bekleme_KomutaCevap, "Komuta cevap zaman aşımı msn");
            this.Bekleme_KomutaCevap.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // Gönder
            // 
            this.Gönder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Gönder.Appearance = System.Windows.Forms.Appearance.Button;
            this.Gönder.AutoCheck = false;
            this.Gönder.FlatAppearance.CheckedBackColor = System.Drawing.Color.DeepSkyBlue;
            this.Gönder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Gönder.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Gönder.Location = new System.Drawing.Point(291, 2);
            this.Gönder.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Gönder.Name = "Gönder";
            this.Gönder.Size = new System.Drawing.Size(73, 22);
            this.Gönder.TabIndex = 9;
            this.Gönder.Text = "Gönder";
            this.Gönder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Gönder.UseVisualStyleBackColor = true;
            this.Gönder.Click += new System.EventHandler(this.Gönder_Click);
            // 
            // Dinleme
            // 
            this.Dinleme.Enabled = true;
            this.Dinleme.Interval = 10000;
            this.Dinleme.Tick += new System.EventHandler(this.Dinleme_Tick);
            // 
            // Gönder_Al
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Gönder);
            this.Controls.Add(this.ŞablonAdı);
            this.Controls.Add(this.Bekleme_İkiKomutArası);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.Ekle);
            this.Controls.Add(this.Bekleme_KomutaCevap);
            this.Controls.Add(this.Sil);
            this.Controls.Add(this.ŞablonListesi);
            this.Enabled = false;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Gönder_Al";
            this.Size = new System.Drawing.Size(369, 162);
            this.Load += new System.EventHandler(this.Gönder_Al_Load);
            this.Menü1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Bekleme_İkiKomutArası)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bekleme_KomutaCevap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox ŞablonListesi;
        private System.Windows.Forms.Button Sil;
        private System.Windows.Forms.TextBox ŞablonAdı;
        private System.Windows.Forms.Button Ekle;
        private System.Windows.Forms.TextBox Girdi;
        private System.Windows.Forms.RichTextBox Çıktı;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.NumericUpDown Bekleme_İkiKomutArası;
        private System.Windows.Forms.ToolTip ipucu;
        private System.Windows.Forms.NumericUpDown Bekleme_KomutaCevap;
        private System.Windows.Forms.ContextMenuStrip Menü1;
        private System.Windows.Forms.ToolStripMenuItem Menü1_SürekliDevamEt;
        private System.Windows.Forms.ToolStripMenuItem Menü1_ÇıktıyıTemizle;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Komutlar;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Komutlar_Bekle;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Sabitler_İkiKomutArasındaBekleme;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Sabitler_KomutCevapZamanAşımı;
        private System.Windows.Forms.ToolStripMenuItem Menü1_MetniKaydır;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Sabitler;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Başlıklı;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Sabitler_Başlıklı;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Komutlar_ÇıktıPunto;
        private System.Windows.Forms.CheckBox Gönder;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Girdili;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Sabitler_Girdili;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Kes;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Kopyala;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Yapıştır;
        private System.Windows.Forms.ToolStripMenuItem Menü1_TümünüSeç;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Temizle;
        private System.Windows.Forms.Timer Dinleme;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Şablonlar;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Şablonlar_UygulamaGönderisi;
        private System.Windows.Forms.ToolStripMenuItem Menü1_Şablonlar_CevabıHexOlarakGöster;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.RichTextBox Çıktı2;
        private System.Windows.Forms.ComboBox Çıktı2Filtre;
    }
}
