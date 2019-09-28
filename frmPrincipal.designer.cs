namespace ProcessamentoImagens
{
    partial class frmPrincipal
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAbrirImagem = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.exemplosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semDMAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.luminânciaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.negativoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.comDMAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.luminânciaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.negativoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aula1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semDMAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.comDMAToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.espelhamentoDiagonalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espelhamentoHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.espelhamentoVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.separarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotacionarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pretoBrancoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aula2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aula3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.semDMAToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.comDMAToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.fatiamentoDeBitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.equalizaçãoDoHistogramaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.suavizaçãoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.conversoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToHSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rGBToCMYToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictBoxImg1 = new System.Windows.Forms.PictureBox();
            this.pictBoxImg2 = new System.Windows.Forms.PictureBox();
            this.pb1 = new System.Windows.Forms.PictureBox();
            this.pb2 = new System.Windows.Forms.PictureBox();
            this.pb3 = new System.Windows.Forms.PictureBox();
            this.lbValues = new System.Windows.Forms.Label();
            this.trkbHue = new System.Windows.Forms.TrackBar();
            this.trkbBrilho = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbHue = new System.Windows.Forms.Label();
            this.lbBrilho = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbBrilho)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAbrirImagem
            // 
            this.btnAbrirImagem.Location = new System.Drawing.Point(12, 573);
            this.btnAbrirImagem.Name = "btnAbrirImagem";
            this.btnAbrirImagem.Size = new System.Drawing.Size(101, 23);
            this.btnAbrirImagem.TabIndex = 106;
            this.btnAbrirImagem.Text = "Abrir Imagem";
            this.btnAbrirImagem.UseVisualStyleBackColor = true;
            this.btnAbrirImagem.Click += new System.EventHandler(this.btnAbrirImagem_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(119, 573);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(101, 23);
            this.btnLimpar.TabIndex = 107;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exemplosToolStripMenuItem,
            this.aula1ToolStripMenuItem,
            this.aula2ToolStripMenuItem,
            this.aula3ToolStripMenuItem,
            this.conversoresToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1220, 24);
            this.menuStrip1.TabIndex = 112;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // exemplosToolStripMenuItem
            // 
            this.exemplosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.semDMAToolStripMenuItem,
            this.comDMAToolStripMenuItem});
            this.exemplosToolStripMenuItem.Name = "exemplosToolStripMenuItem";
            this.exemplosToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.exemplosToolStripMenuItem.Text = "Exemplos";
            // 
            // semDMAToolStripMenuItem
            // 
            this.semDMAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.luminânciaToolStripMenuItem,
            this.negativoToolStripMenuItem});
            this.semDMAToolStripMenuItem.Name = "semDMAToolStripMenuItem";
            this.semDMAToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.semDMAToolStripMenuItem.Text = "Sem DMA";
            // 
            // luminânciaToolStripMenuItem
            // 
            this.luminânciaToolStripMenuItem.Name = "luminânciaToolStripMenuItem";
            this.luminânciaToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.luminânciaToolStripMenuItem.Text = "Luminância";
            this.luminânciaToolStripMenuItem.Click += new System.EventHandler(this.luminânciaToolStripMenuItem_Click);
            // 
            // negativoToolStripMenuItem
            // 
            this.negativoToolStripMenuItem.Name = "negativoToolStripMenuItem";
            this.negativoToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.negativoToolStripMenuItem.Text = "Negativo";
            this.negativoToolStripMenuItem.Click += new System.EventHandler(this.negativoToolStripMenuItem_Click);
            // 
            // comDMAToolStripMenuItem
            // 
            this.comDMAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.luminânciaToolStripMenuItem1,
            this.negativoToolStripMenuItem1});
            this.comDMAToolStripMenuItem.Name = "comDMAToolStripMenuItem";
            this.comDMAToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.comDMAToolStripMenuItem.Text = "Com DMA";
            // 
            // luminânciaToolStripMenuItem1
            // 
            this.luminânciaToolStripMenuItem1.Name = "luminânciaToolStripMenuItem1";
            this.luminânciaToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.luminânciaToolStripMenuItem1.Text = "Luminância";
            this.luminânciaToolStripMenuItem1.Click += new System.EventHandler(this.luminânciaToolStripMenuItem1_Click);
            // 
            // negativoToolStripMenuItem1
            // 
            this.negativoToolStripMenuItem1.Name = "negativoToolStripMenuItem1";
            this.negativoToolStripMenuItem1.Size = new System.Drawing.Size(136, 22);
            this.negativoToolStripMenuItem1.Text = "Negativo";
            this.negativoToolStripMenuItem1.Click += new System.EventHandler(this.negativoToolStripMenuItem1_Click);
            // 
            // aula1ToolStripMenuItem
            // 
            this.aula1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.semDMAToolStripMenuItem1,
            this.comDMAToolStripMenuItem1});
            this.aula1ToolStripMenuItem.Name = "aula1ToolStripMenuItem";
            this.aula1ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aula1ToolStripMenuItem.Text = "Aula 1";
            // 
            // semDMAToolStripMenuItem1
            // 
            this.semDMAToolStripMenuItem1.Name = "semDMAToolStripMenuItem1";
            this.semDMAToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.semDMAToolStripMenuItem1.Text = "Sem DMA";
            // 
            // comDMAToolStripMenuItem1
            // 
            this.comDMAToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.espelhamentoDiagonalToolStripMenuItem,
            this.espelhamentoHorizontalToolStripMenuItem,
            this.espelhamentoVerticalToolStripMenuItem,
            this.separarToolStripMenuItem,
            this.rotacionarToolStripMenuItem,
            this.pretoBrancoToolStripMenuItem});
            this.comDMAToolStripMenuItem1.Name = "comDMAToolStripMenuItem1";
            this.comDMAToolStripMenuItem1.Size = new System.Drawing.Size(130, 22);
            this.comDMAToolStripMenuItem1.Text = "Com DMA";
            // 
            // espelhamentoDiagonalToolStripMenuItem
            // 
            this.espelhamentoDiagonalToolStripMenuItem.Name = "espelhamentoDiagonalToolStripMenuItem";
            this.espelhamentoDiagonalToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.espelhamentoDiagonalToolStripMenuItem.Text = "Espelhamento Diagonal";
            this.espelhamentoDiagonalToolStripMenuItem.Click += new System.EventHandler(this.espelhamentoDiagonalToolStripMenuItem_Click);
            // 
            // espelhamentoHorizontalToolStripMenuItem
            // 
            this.espelhamentoHorizontalToolStripMenuItem.Name = "espelhamentoHorizontalToolStripMenuItem";
            this.espelhamentoHorizontalToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.espelhamentoHorizontalToolStripMenuItem.Text = "Espelhamento Horizontal";
            this.espelhamentoHorizontalToolStripMenuItem.Click += new System.EventHandler(this.espelhamentoHorizontalToolStripMenuItem_Click);
            // 
            // espelhamentoVerticalToolStripMenuItem
            // 
            this.espelhamentoVerticalToolStripMenuItem.Name = "espelhamentoVerticalToolStripMenuItem";
            this.espelhamentoVerticalToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.espelhamentoVerticalToolStripMenuItem.Text = "Espelhamento Vertical";
            this.espelhamentoVerticalToolStripMenuItem.Click += new System.EventHandler(this.espelhamentoVerticalToolStripMenuItem_Click);
            // 
            // separarToolStripMenuItem
            // 
            this.separarToolStripMenuItem.Name = "separarToolStripMenuItem";
            this.separarToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.separarToolStripMenuItem.Text = "Separar";
            this.separarToolStripMenuItem.Click += new System.EventHandler(this.separarToolStripMenuItem_Click);
            // 
            // rotacionarToolStripMenuItem
            // 
            this.rotacionarToolStripMenuItem.Name = "rotacionarToolStripMenuItem";
            this.rotacionarToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.rotacionarToolStripMenuItem.Text = "Rotacionar";
            this.rotacionarToolStripMenuItem.Click += new System.EventHandler(this.rotacionarToolStripMenuItem_Click);
            // 
            // pretoBrancoToolStripMenuItem
            // 
            this.pretoBrancoToolStripMenuItem.Name = "pretoBrancoToolStripMenuItem";
            this.pretoBrancoToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.pretoBrancoToolStripMenuItem.Text = "PretoBranco";
            this.pretoBrancoToolStripMenuItem.Click += new System.EventHandler(this.pretoBrancoToolStripMenuItem_Click);
            // 
            // aula2ToolStripMenuItem
            // 
            this.aula2ToolStripMenuItem.Name = "aula2ToolStripMenuItem";
            this.aula2ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aula2ToolStripMenuItem.Text = "Aula 2";
            // 
            // aula3ToolStripMenuItem
            // 
            this.aula3ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.semDMAToolStripMenuItem2,
            this.comDMAToolStripMenuItem2});
            this.aula3ToolStripMenuItem.Name = "aula3ToolStripMenuItem";
            this.aula3ToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aula3ToolStripMenuItem.Text = "Aula 3";
            // 
            // semDMAToolStripMenuItem2
            // 
            this.semDMAToolStripMenuItem2.Name = "semDMAToolStripMenuItem2";
            this.semDMAToolStripMenuItem2.Size = new System.Drawing.Size(130, 22);
            this.semDMAToolStripMenuItem2.Text = "Sem DMA";
            // 
            // comDMAToolStripMenuItem2
            // 
            this.comDMAToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fatiamentoDeBitsToolStripMenuItem,
            this.equalizaçãoDoHistogramaToolStripMenuItem,
            this.suavizaçãoToolStripMenuItem});
            this.comDMAToolStripMenuItem2.Name = "comDMAToolStripMenuItem2";
            this.comDMAToolStripMenuItem2.Size = new System.Drawing.Size(130, 22);
            this.comDMAToolStripMenuItem2.Text = "Com DMA";
            // 
            // fatiamentoDeBitsToolStripMenuItem
            // 
            this.fatiamentoDeBitsToolStripMenuItem.Name = "fatiamentoDeBitsToolStripMenuItem";
            this.fatiamentoDeBitsToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.fatiamentoDeBitsToolStripMenuItem.Text = "Fatiamento de bits";
            this.fatiamentoDeBitsToolStripMenuItem.Click += new System.EventHandler(this.fatiamentoDeBitsToolStripMenuItem_Click);
            // 
            // equalizaçãoDoHistogramaToolStripMenuItem
            // 
            this.equalizaçãoDoHistogramaToolStripMenuItem.Name = "equalizaçãoDoHistogramaToolStripMenuItem";
            this.equalizaçãoDoHistogramaToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.equalizaçãoDoHistogramaToolStripMenuItem.Text = "Equalização do Histograma";
            this.equalizaçãoDoHistogramaToolStripMenuItem.Click += new System.EventHandler(this.equalizaçãoDoHistogramaToolStripMenuItem_Click);
            // 
            // suavizaçãoToolStripMenuItem
            // 
            this.suavizaçãoToolStripMenuItem.Name = "suavizaçãoToolStripMenuItem";
            this.suavizaçãoToolStripMenuItem.Size = new System.Drawing.Size(218, 22);
            this.suavizaçãoToolStripMenuItem.Text = "Suavização";
            this.suavizaçãoToolStripMenuItem.Click += new System.EventHandler(this.suavizaçãoToolStripMenuItem_Click);
            // 
            // conversoresToolStripMenuItem
            // 
            this.conversoresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rGBToHSIToolStripMenuItem,
            this.rGBToCMYToolStripMenuItem});
            this.conversoresToolStripMenuItem.Name = "conversoresToolStripMenuItem";
            this.conversoresToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.conversoresToolStripMenuItem.Text = "Conversores";
            // 
            // rGBToHSIToolStripMenuItem
            // 
            this.rGBToHSIToolStripMenuItem.Name = "rGBToHSIToolStripMenuItem";
            this.rGBToHSIToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.rGBToHSIToolStripMenuItem.Text = "RGB to HSI";
            this.rGBToHSIToolStripMenuItem.Click += new System.EventHandler(this.RGBToHSIToolStripMenuItem_Click);
            // 
            // rGBToCMYToolStripMenuItem
            // 
            this.rGBToCMYToolStripMenuItem.Name = "rGBToCMYToolStripMenuItem";
            this.rGBToCMYToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.rGBToCMYToolStripMenuItem.Text = "RGB to CMY";
            this.rGBToCMYToolStripMenuItem.Click += new System.EventHandler(this.RGBToCMYToolStripMenuItem_Click);
            // 
            // pictBoxImg1
            // 
            this.pictBoxImg1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictBoxImg1.Location = new System.Drawing.Point(5, 33);
            this.pictBoxImg1.Name = "pictBoxImg1";
            this.pictBoxImg1.Size = new System.Drawing.Size(600, 510);
            this.pictBoxImg1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictBoxImg1.TabIndex = 102;
            this.pictBoxImg1.TabStop = false;
            this.pictBoxImg1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PictBoxImg1_MouseMove);
            // 
            // pictBoxImg2
            // 
            this.pictBoxImg2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pictBoxImg2.Location = new System.Drawing.Point(822, 33);
            this.pictBoxImg2.Name = "pictBoxImg2";
            this.pictBoxImg2.Size = new System.Drawing.Size(386, 338);
            this.pictBoxImg2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBoxImg2.TabIndex = 105;
            this.pictBoxImg2.TabStop = false;
            // 
            // pb1
            // 
            this.pb1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pb1.Location = new System.Drawing.Point(611, 33);
            this.pb1.Name = "pb1";
            this.pb1.Size = new System.Drawing.Size(205, 166);
            this.pb1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb1.TabIndex = 113;
            this.pb1.TabStop = false;
            // 
            // pb2
            // 
            this.pb2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pb2.Location = new System.Drawing.Point(611, 205);
            this.pb2.Name = "pb2";
            this.pb2.Size = new System.Drawing.Size(205, 166);
            this.pb2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb2.TabIndex = 114;
            this.pb2.TabStop = false;
            // 
            // pb3
            // 
            this.pb3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pb3.Location = new System.Drawing.Point(611, 377);
            this.pb3.Name = "pb3";
            this.pb3.Size = new System.Drawing.Size(205, 166);
            this.pb3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb3.TabIndex = 115;
            this.pb3.TabStop = false;
            // 
            // lbValues
            // 
            this.lbValues.AutoSize = true;
            this.lbValues.Location = new System.Drawing.Point(73, 118);
            this.lbValues.Name = "lbValues";
            this.lbValues.Size = new System.Drawing.Size(0, 13);
            this.lbValues.TabIndex = 116;
            // 
            // trkbHue
            // 
            this.trkbHue.Location = new System.Drawing.Point(889, 393);
            this.trkbHue.Minimum = -10;
            this.trkbHue.Name = "trkbHue";
            this.trkbHue.Size = new System.Drawing.Size(217, 45);
            this.trkbHue.TabIndex = 117;
            this.trkbHue.TickFrequency = 10;
            this.trkbHue.ValueChanged += new System.EventHandler(this.TrkbHue_ValueChanged);
            // 
            // trkbBrilho
            // 
            this.trkbBrilho.Location = new System.Drawing.Point(889, 457);
            this.trkbBrilho.Maximum = 100;
            this.trkbBrilho.Minimum = -100;
            this.trkbBrilho.Name = "trkbBrilho";
            this.trkbBrilho.Size = new System.Drawing.Size(217, 45);
            this.trkbBrilho.SmallChange = 5;
            this.trkbBrilho.TabIndex = 118;
            this.trkbBrilho.TickFrequency = 10;
            this.trkbBrilho.ValueChanged += new System.EventHandler(this.TrackBar2_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(886, 377);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 119;
            this.label1.Text = "HUE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(886, 441);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 13);
            this.label2.TabIndex = 120;
            this.label2.Text = "Brilho";
            // 
            // lbHue
            // 
            this.lbHue.AutoSize = true;
            this.lbHue.Location = new System.Drawing.Point(1112, 403);
            this.lbHue.Name = "lbHue";
            this.lbHue.Size = new System.Drawing.Size(0, 13);
            this.lbHue.TabIndex = 121;
            // 
            // lbBrilho
            // 
            this.lbBrilho.AutoSize = true;
            this.lbBrilho.Location = new System.Drawing.Point(1112, 468);
            this.lbBrilho.Name = "lbBrilho";
            this.lbBrilho.Size = new System.Drawing.Size(0, 13);
            this.lbBrilho.TabIndex = 122;
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1220, 608);
            this.Controls.Add(this.lbBrilho);
            this.Controls.Add(this.lbHue);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trkbBrilho);
            this.Controls.Add(this.trkbHue);
            this.Controls.Add(this.lbValues);
            this.Controls.Add(this.pb3);
            this.Controls.Add(this.pb2);
            this.Controls.Add(this.pb1);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnAbrirImagem);
            this.Controls.Add(this.pictBoxImg2);
            this.Controls.Add(this.pictBoxImg1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário Principal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictBoxImg2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbBrilho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnAbrirImagem;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem exemplosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem semDMAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem luminânciaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem negativoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem comDMAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem luminânciaToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem negativoToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem aula1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem semDMAToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem comDMAToolStripMenuItem1;
        private System.Windows.Forms.PictureBox pictBoxImg1;
        private System.Windows.Forms.PictureBox pictBoxImg2;
        private System.Windows.Forms.ToolStripMenuItem espelhamentoDiagonalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espelhamentoHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aula2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem espelhamentoVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem separarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rotacionarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pretoBrancoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aula3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem semDMAToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem comDMAToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem fatiamentoDeBitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem equalizaçãoDoHistogramaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem suavizaçãoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem conversoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rGBToHSIToolStripMenuItem;
        private System.Windows.Forms.PictureBox pb1;
        private System.Windows.Forms.PictureBox pb2;
        private System.Windows.Forms.PictureBox pb3;
        private System.Windows.Forms.Label lbValues;
        private System.Windows.Forms.ToolStripMenuItem rGBToCMYToolStripMenuItem;
        private System.Windows.Forms.TrackBar trkbHue;
        private System.Windows.Forms.TrackBar trkbBrilho;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbHue;
        private System.Windows.Forms.Label lbBrilho;
    }
}

