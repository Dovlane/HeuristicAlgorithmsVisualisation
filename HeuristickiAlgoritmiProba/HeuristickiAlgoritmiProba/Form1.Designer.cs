namespace HeuristickiAlgoritmiProba
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.prikazGB = new System.Windows.Forms.GroupBox();
            this.prikazTezinaChB = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.zadavanjeGrafaGB = new System.Windows.Forms.GroupBox();
            this.usmerenaGranaChB = new System.Windows.Forms.CheckBox();
            this.obrisiGrafBtn = new System.Windows.Forms.Button();
            this.pokreniBtn = new System.Windows.Forms.Button();
            this.pauzirajBtn = new System.Windows.Forms.Button();
            this.prekiniBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.statistikaRichTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.timerAlgoritma = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.prikazGB.SuspendLayout();
            this.zadavanjeGrafaGB.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureBox1.Location = new System.Drawing.Point(-1, 27);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(808, 524);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseUp);
            // 
            // prikazGB
            // 
            this.prikazGB.Controls.Add(this.prikazTezinaChB);
            this.prikazGB.Location = new System.Drawing.Point(811, 29);
            this.prikazGB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.prikazGB.Name = "prikazGB";
            this.prikazGB.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.prikazGB.Size = new System.Drawing.Size(217, 45);
            this.prikazGB.TabIndex = 1;
            this.prikazGB.TabStop = false;
            this.prikazGB.Text = "Prikaz";
            // 
            // prikazTezinaChB
            // 
            this.prikazTezinaChB.AutoSize = true;
            this.prikazTezinaChB.Location = new System.Drawing.Point(4, 18);
            this.prikazTezinaChB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.prikazTezinaChB.Name = "prikazTezinaChB";
            this.prikazTezinaChB.Size = new System.Drawing.Size(91, 19);
            this.prikazTezinaChB.TabIndex = 0;
            this.prikazTezinaChB.Text = "prikaz težina";
            this.prikazTezinaChB.UseVisualStyleBackColor = true;
            this.prikazTezinaChB.CheckedChanged += new System.EventHandler(this.prikazTezinaChB_CheckedChanged);
            // 
            // zadavanjeGrafaGB
            // 
            this.zadavanjeGrafaGB.Controls.Add(this.usmerenaGranaChB);
            this.zadavanjeGrafaGB.Location = new System.Drawing.Point(814, 81);
            this.zadavanjeGrafaGB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.zadavanjeGrafaGB.Name = "zadavanjeGrafaGB";
            this.zadavanjeGrafaGB.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.zadavanjeGrafaGB.Size = new System.Drawing.Size(214, 44);
            this.zadavanjeGrafaGB.TabIndex = 2;
            this.zadavanjeGrafaGB.TabStop = false;
            this.zadavanjeGrafaGB.Text = "Zadavanje grafa";
            // 
            // usmerenaGranaChB
            // 
            this.usmerenaGranaChB.AutoSize = true;
            this.usmerenaGranaChB.Location = new System.Drawing.Point(4, 18);
            this.usmerenaGranaChB.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.usmerenaGranaChB.Name = "usmerenaGranaChB";
            this.usmerenaGranaChB.Size = new System.Drawing.Size(111, 19);
            this.usmerenaGranaChB.TabIndex = 0;
            this.usmerenaGranaChB.Text = "usmerena grana";
            this.usmerenaGranaChB.UseVisualStyleBackColor = true;
            this.usmerenaGranaChB.CheckedChanged += new System.EventHandler(this.usmerenaGranaChB_CheckedChanged);
            // 
            // obrisiGrafBtn
            // 
            this.obrisiGrafBtn.Location = new System.Drawing.Point(817, 132);
            this.obrisiGrafBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.obrisiGrafBtn.Name = "obrisiGrafBtn";
            this.obrisiGrafBtn.Size = new System.Drawing.Size(97, 51);
            this.obrisiGrafBtn.TabIndex = 3;
            this.obrisiGrafBtn.Text = "Obriši trenutni graf";
            this.obrisiGrafBtn.UseVisualStyleBackColor = true;
            this.obrisiGrafBtn.Click += new System.EventHandler(this.obrisiGrafBtn_Click);
            // 
            // pokreniBtn
            // 
            this.pokreniBtn.Location = new System.Drawing.Point(5, 17);
            this.pokreniBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pokreniBtn.Name = "pokreniBtn";
            this.pokreniBtn.Size = new System.Drawing.Size(61, 27);
            this.pokreniBtn.TabIndex = 5;
            this.pokreniBtn.Text = "Pokreni";
            this.pokreniBtn.UseVisualStyleBackColor = true;
            this.pokreniBtn.Click += new System.EventHandler(this.pokreniBtn_Click);
            // 
            // pauzirajBtn
            // 
            this.pauzirajBtn.Location = new System.Drawing.Point(72, 17);
            this.pauzirajBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.pauzirajBtn.Name = "pauzirajBtn";
            this.pauzirajBtn.Size = new System.Drawing.Size(61, 27);
            this.pauzirajBtn.TabIndex = 6;
            this.pauzirajBtn.Text = "Pauziraj";
            this.pauzirajBtn.UseVisualStyleBackColor = true;
            this.pauzirajBtn.Click += new System.EventHandler(this.pauzirajBtn_Click);
            // 
            // prekiniBtn
            // 
            this.prekiniBtn.Location = new System.Drawing.Point(140, 17);
            this.prekiniBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.prekiniBtn.Name = "prekiniBtn";
            this.prekiniBtn.Size = new System.Drawing.Size(63, 27);
            this.prekiniBtn.TabIndex = 7;
            this.prekiniBtn.Text = "Prekini";
            this.prekiniBtn.UseVisualStyleBackColor = true;
            this.prekiniBtn.Click += new System.EventHandler(this.prekiniBtn_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.statistikaRichTextBox);
            this.groupBox1.Location = new System.Drawing.Point(815, 239);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox1.Size = new System.Drawing.Size(217, 312);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistika";
            // 
            // statistikaRichTextBox
            // 
            this.statistikaRichTextBox.Location = new System.Drawing.Point(7, 22);
            this.statistikaRichTextBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.statistikaRichTextBox.Name = "statistikaRichTextBox";
            this.statistikaRichTextBox.Size = new System.Drawing.Size(210, 284);
            this.statistikaRichTextBox.TabIndex = 0;
            this.statistikaRichTextBox.Text = "";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.pokreniBtn);
            this.groupBox2.Controls.Add(this.pauzirajBtn);
            this.groupBox2.Controls.Add(this.prekiniBtn);
            this.groupBox2.Location = new System.Drawing.Point(820, 189);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.groupBox2.Size = new System.Drawing.Size(203, 44);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kontrola izvršavanja algoritma";
            // 
            // timerAlgoritma
            // 
            this.timerAlgoritma.Interval = 2000;
            this.timerAlgoritma.Tick += new System.EventHandler(this.timerAlgoritma_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1036, 564);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.obrisiGrafBtn);
            this.Controls.Add(this.zadavanjeGrafaGB);
            this.Controls.Add(this.prikazGB);
            this.Controls.Add(this.pictureBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.prikazGB.ResumeLayout(false);
            this.prikazGB.PerformLayout();
            this.zadavanjeGrafaGB.ResumeLayout(false);
            this.zadavanjeGrafaGB.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox prikazGB;
        private System.Windows.Forms.CheckBox prikazTezinaChB;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.GroupBox zadavanjeGrafaGB;
        private System.Windows.Forms.CheckBox usmerenaGranaChB;
        private System.Windows.Forms.Button obrisiGrafBtn;
        private System.Windows.Forms.Button pokreniBtn;
        private System.Windows.Forms.Button pauzirajBtn;
        private System.Windows.Forms.Button prekiniBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox statistikaRichTextBox;
        private System.Windows.Forms.Timer timerAlgoritma;
    }
}

