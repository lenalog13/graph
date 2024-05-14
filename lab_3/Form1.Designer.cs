namespace lab_3
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PointInGrap = new System.Windows.Forms.Timer(this.components);
            this.AnT2 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.AnT1 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.AnT3 = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.SuspendLayout();
            // 
            // PointInGrap
            // 
            this.PointInGrap.Tick += new System.EventHandler(this.PointInGrap_Tick);
            // 
            // AnT2
            // 
            this.AnT2.AccumBits = ((byte)(0));
            this.AnT2.AutoCheckErrors = false;
            this.AnT2.AutoFinish = false;
            this.AnT2.AutoMakeCurrent = true;
            this.AnT2.AutoSwapBuffers = true;
            this.AnT2.BackColor = System.Drawing.Color.Black;
            this.AnT2.ColorBits = ((byte)(32));
            this.AnT2.DepthBits = ((byte)(16));
            this.AnT2.Location = new System.Drawing.Point(947, 105);
            this.AnT2.Name = "AnT2";
            this.AnT2.Size = new System.Drawing.Size(201, 405);
            this.AnT2.StencilBits = ((byte)(0));
            this.AnT2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(656, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "График:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(656, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Длина (м):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(656, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(121, 25);
            this.label3.TabIndex = 5;
            this.label3.Text = "Масса (кг):";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(656, 294);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(234, 25);
            this.label4.TabIndex = 6;
            this.label4.Text = "Коэффициент трения:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(656, 367);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "Угол:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(655, 440);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(158, 25);
            this.label5.TabIndex = 8;
            this.label5.Text = "Нач. скорость:";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.comboBox1.Location = new System.Drawing.Point(661, 105);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(229, 33);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(655, 514);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 25);
            this.label8.TabIndex = 15;
            this.label8.Text = "Время (с):";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(660, 542);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(208, 38);
            this.button1.TabIndex = 16;
            this.button1.Text = "Построить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(953, 524);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(38, 25);
            this.label12.TabIndex = 17;
            this.label12.Text = "Ep";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1092, 524);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(37, 25);
            this.label13.TabIndex = 18;
            this.label13.Text = "Ek";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(953, 555);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 25);
            this.label10.TabIndex = 19;
            this.label10.Text = "None";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(1092, 555);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 25);
            this.label11.TabIndex = 20;
            this.label11.Text = "None";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(660, 176);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(230, 31);
            this.textBox5.TabIndex = 22;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(661, 248);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(230, 31);
            this.textBox1.TabIndex = 23;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(660, 322);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(230, 31);
            this.textBox6.TabIndex = 24;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(661, 395);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(230, 31);
            this.textBox7.TabIndex = 25;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(661, 468);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(230, 31);
            this.textBox8.TabIndex = 26;
            // 
            // AnT1
            // 
            this.AnT1.AccumBits = ((byte)(0));
            this.AnT1.AutoCheckErrors = false;
            this.AnT1.AutoFinish = false;
            this.AnT1.AutoMakeCurrent = true;
            this.AnT1.AutoSwapBuffers = true;
            this.AnT1.BackColor = System.Drawing.Color.Black;
            this.AnT1.ColorBits = ((byte)(32));
            this.AnT1.DepthBits = ((byte)(16));
            this.AnT1.Location = new System.Drawing.Point(12, 81);
            this.AnT1.Name = "AnT1";
            this.AnT1.Size = new System.Drawing.Size(620, 499);
            this.AnT1.StencilBits = ((byte)(0));
            this.AnT1.TabIndex = 27;
            // 
            // AnT3
            // 
            this.AnT3.AccumBits = ((byte)(0));
            this.AnT3.AutoCheckErrors = false;
            this.AnT3.AutoFinish = false;
            this.AnT3.AutoMakeCurrent = true;
            this.AnT3.AutoSwapBuffers = true;
            this.AnT3.BackColor = System.Drawing.Color.Black;
            this.AnT3.ColorBits = ((byte)(32));
            this.AnT3.DepthBits = ((byte)(16));
            this.AnT3.Location = new System.Drawing.Point(1197, 105);
            this.AnT3.Name = "AnT3";
            this.AnT3.Size = new System.Drawing.Size(352, 405);
            this.AnT3.StencilBits = ((byte)(0));
            this.AnT3.TabIndex = 28;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1578, 665);
            this.Controls.Add(this.AnT3);
            this.Controls.Add(this.AnT1);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AnT2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer PointInGrap;
        private Tao.Platform.Windows.SimpleOpenGlControl AnT2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private Tao.Platform.Windows.SimpleOpenGlControl AnT1;
        private Tao.Platform.Windows.SimpleOpenGlControl AnT3;
    }
}

