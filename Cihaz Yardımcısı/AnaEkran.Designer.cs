namespace Cihaz_Yardımcısı
{
    partial class AnaEkran
    {
        class ÇiftTamponluTreeView : System.Windows.Forms.TreeView
        {
            private const int TVM_SETEXTENDEDSTYLE = 0x1100 + 44;
            private const int TVM_GETEXTENDEDSTYLE = 0x1100 + 45;
            private const int TVS_EX_DOUBLEBUFFER = 0x0004;
            protected override void OnHandleCreated(System.EventArgs e)
            {
                ArgeMup.HazirKod.W32_9.SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, (System.IntPtr)TVS_EX_DOUBLEBUFFER, (System.IntPtr)TVS_EX_DOUBLEBUFFER);
                base.OnHandleCreated(e);
            }
        }

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AnaEkran));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Ağaç = new Cihaz_Yardımcısı.AnaEkran.ÇiftTamponluTreeView();
            this.Menü_Ağaç = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.Menü_Ağaç_yenile = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü_Ağaç_ekle = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü_Ağaç_TextBox = new System.Windows.Forms.ToolStripTextBox();
            this.Menü_Ağaç_tamam = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü_Ağaç_kaldır = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü_Ağaç_izinDurumu = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü_Ağaç_ayırıcı1 = new System.Windows.Forms.ToolStripSeparator();
            this.Menü_Ağaç_genişlet = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü_Ağaç_daralt = new System.Windows.Forms.ToolStripMenuItem();
            this.Menü_Ağaç_ayırıcı2 = new System.Windows.Forms.ToolStripSeparator();
            this.Menü_Ağaç_uygulama = new System.Windows.Forms.ToolStripMenuItem();
            this.ResimListesi = new System.Windows.Forms.ImageList(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.Menü_Ağaç.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AutoScroll = true;
            this.splitContainer1.Panel1.Controls.Add(this.Ağaç);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.AutoScroll = true;
            this.splitContainer1.Size = new System.Drawing.Size(320, 141);
            this.splitContainer1.SplitterDistance = 97;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // Ağaç
            // 
            this.Ağaç.ContextMenuStrip = this.Menü_Ağaç;
            this.Ağaç.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Ağaç.HideSelection = false;
            this.Ağaç.HotTracking = true;
            this.Ağaç.ImageIndex = 0;
            this.Ağaç.ImageList = this.ResimListesi;
            this.Ağaç.Location = new System.Drawing.Point(0, 0);
            this.Ağaç.Margin = new System.Windows.Forms.Padding(2);
            this.Ağaç.Name = "Ağaç";
            this.Ağaç.SelectedImageIndex = 0;
            this.Ağaç.ShowNodeToolTips = true;
            this.Ağaç.Size = new System.Drawing.Size(95, 139);
            this.Ağaç.TabIndex = 0;
            this.Ağaç.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.Ağaç_NodeMouseClick);
            // 
            // Menü_Ağaç
            // 
            this.Menü_Ağaç.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.Menü_Ağaç.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menü_Ağaç_yenile,
            this.Menü_Ağaç_ekle,
            this.Menü_Ağaç_kaldır,
            this.Menü_Ağaç_izinDurumu,
            this.Menü_Ağaç_ayırıcı1,
            this.Menü_Ağaç_genişlet,
            this.Menü_Ağaç_daralt,
            this.Menü_Ağaç_ayırıcı2,
            this.Menü_Ağaç_uygulama});
            this.Menü_Ağaç.Name = "Menü_Ağaç";
            this.Menü_Ağaç.Size = new System.Drawing.Size(219, 198);
            this.Menü_Ağaç.Opening += new System.ComponentModel.CancelEventHandler(this.Menü_Ağaç_Opening);
            // 
            // Menü_Ağaç_yenile
            // 
            this.Menü_Ağaç_yenile.Image = global::Cihaz_Yardımcısı.Properties.Resources.Yenile;
            this.Menü_Ağaç_yenile.Name = "Menü_Ağaç_yenile";
            this.Menü_Ağaç_yenile.Size = new System.Drawing.Size(218, 26);
            this.Menü_Ağaç_yenile.Text = "Yenile";
            this.Menü_Ağaç_yenile.Click += new System.EventHandler(this.Menü_Ağaç_yenile_Click);
            // 
            // Menü_Ağaç_ekle
            // 
            this.Menü_Ağaç_ekle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Menü_Ağaç_TextBox,
            this.Menü_Ağaç_tamam});
            this.Menü_Ağaç_ekle.Image = global::Cihaz_Yardımcısı.Properties.Resources.Ekle;
            this.Menü_Ağaç_ekle.Name = "Menü_Ağaç_ekle";
            this.Menü_Ağaç_ekle.Size = new System.Drawing.Size(218, 26);
            this.Menü_Ağaç_ekle.Text = "Ekle";
            // 
            // Menü_Ağaç_TextBox
            // 
            this.Menü_Ağaç_TextBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Menü_Ağaç_TextBox.Name = "Menü_Ağaç_TextBox";
            this.Menü_Ağaç_TextBox.Size = new System.Drawing.Size(300, 27);
            this.Menü_Ağaç_TextBox.ToolTipText = "deneme";
            // 
            // Menü_Ağaç_tamam
            // 
            this.Menü_Ağaç_tamam.Image = global::Cihaz_Yardımcısı.Properties.Resources.Ekle;
            this.Menü_Ağaç_tamam.Name = "Menü_Ağaç_tamam";
            this.Menü_Ağaç_tamam.Size = new System.Drawing.Size(374, 26);
            this.Menü_Ağaç_tamam.Text = "Ekle";
            this.Menü_Ağaç_tamam.Click += new System.EventHandler(this.Menü_Ağaç_tamam_Click);
            // 
            // Menü_Ağaç_kaldır
            // 
            this.Menü_Ağaç_kaldır.Image = global::Cihaz_Yardımcısı.Properties.Resources.Sil;
            this.Menü_Ağaç_kaldır.Name = "Menü_Ağaç_kaldır";
            this.Menü_Ağaç_kaldır.Size = new System.Drawing.Size(218, 26);
            this.Menü_Ağaç_kaldır.Text = "Kaldır";
            this.Menü_Ağaç_kaldır.Click += new System.EventHandler(this.Menü_Ağaç_kaldır_Click);
            // 
            // Menü_Ağaç_izinDurumu
            // 
            this.Menü_Ağaç_izinDurumu.Image = global::Cihaz_Yardımcısı.Properties.Resources.Durgun;
            this.Menü_Ağaç_izinDurumu.Name = "Menü_Ağaç_izinDurumu";
            this.Menü_Ağaç_izinDurumu.Size = new System.Drawing.Size(218, 26);
            this.Menü_Ağaç_izinDurumu.Text = "İzin Durumu Değiştir";
            this.Menü_Ağaç_izinDurumu.Click += new System.EventHandler(this.Menü_Ağaç_izinDurumu_Click);
            // 
            // Menü_Ağaç_ayırıcı1
            // 
            this.Menü_Ağaç_ayırıcı1.Name = "Menü_Ağaç_ayırıcı1";
            this.Menü_Ağaç_ayırıcı1.Size = new System.Drawing.Size(215, 6);
            // 
            // Menü_Ağaç_genişlet
            // 
            this.Menü_Ağaç_genişlet.Name = "Menü_Ağaç_genişlet";
            this.Menü_Ağaç_genişlet.Size = new System.Drawing.Size(218, 26);
            this.Menü_Ağaç_genişlet.Text = "Genişlet";
            this.Menü_Ağaç_genişlet.Click += new System.EventHandler(this.Menü_Ağaç_genişlet_Click);
            // 
            // Menü_Ağaç_daralt
            // 
            this.Menü_Ağaç_daralt.Name = "Menü_Ağaç_daralt";
            this.Menü_Ağaç_daralt.Size = new System.Drawing.Size(218, 26);
            this.Menü_Ağaç_daralt.Text = "Daralt";
            this.Menü_Ağaç_daralt.Click += new System.EventHandler(this.Menü_Ağaç_daralt_Click);
            // 
            // Menü_Ağaç_ayırıcı2
            // 
            this.Menü_Ağaç_ayırıcı2.Name = "Menü_Ağaç_ayırıcı2";
            this.Menü_Ağaç_ayırıcı2.Size = new System.Drawing.Size(215, 6);
            // 
            // Menü_Ağaç_uygulama
            // 
            this.Menü_Ağaç_uygulama.Image = global::Cihaz_Yardımcısı.Properties.Resources.Seçili;
            this.Menü_Ağaç_uygulama.Name = "Menü_Ağaç_uygulama";
            this.Menü_Ağaç_uygulama.Size = new System.Drawing.Size(218, 26);
            this.Menü_Ağaç_uygulama.Text = "Uygulama";
            // 
            // ResimListesi
            // 
            this.ResimListesi.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ResimListesi.ImageSize = new System.Drawing.Size(16, 16);
            this.ResimListesi.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // AnaEkran
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 141);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "AnaEkran";
            this.Opacity = 0D;
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AnaEkran_FormClosed);
            this.Load += new System.EventHandler(this.AnaEkran_Load);
            this.Shown += new System.EventHandler(this.AnaEkran_Shown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.Menü_Ağaç.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Cihaz_Yardımcısı.AnaEkran.ÇiftTamponluTreeView Ağaç;
        private System.Windows.Forms.ContextMenuStrip Menü_Ağaç;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_yenile;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_ekle;
        private System.Windows.Forms.ToolStripTextBox Menü_Ağaç_TextBox;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_tamam;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_kaldır;
        private System.Windows.Forms.ImageList ResimListesi;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_izinDurumu;
        private System.Windows.Forms.ToolStripSeparator Menü_Ağaç_ayırıcı1;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_genişlet;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_daralt;
        private System.Windows.Forms.ToolStripSeparator Menü_Ağaç_ayırıcı2;
        private System.Windows.Forms.ToolStripMenuItem Menü_Ağaç_uygulama;
    }
}

