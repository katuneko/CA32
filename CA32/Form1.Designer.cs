namespace CA32
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.RefleshButton = new System.Windows.Forms.Button();
            this.RunStop = new System.Windows.Forms.Button();
            this.Fmin = new System.Windows.Forms.Button();
            this.Fmid = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.StayProb = new System.Windows.Forms.TextBox();
            this.DeathProb = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.MaximumSize = new System.Drawing.Size(512, 512);
            this.pictureBox1.MinimumSize = new System.Drawing.Size(256, 256);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // RefleshButton
            // 
            this.RefleshButton.Location = new System.Drawing.Point(12, 274);
            this.RefleshButton.Name = "RefleshButton";
            this.RefleshButton.Size = new System.Drawing.Size(104, 41);
            this.RefleshButton.TabIndex = 1;
            this.RefleshButton.Text = "Reflesh";
            this.RefleshButton.UseVisualStyleBackColor = true;
            this.RefleshButton.Click += new System.EventHandler(this.RefleshButton_Click);
            // 
            // RunStop
            // 
            this.RunStop.Location = new System.Drawing.Point(146, 274);
            this.RunStop.Name = "RunStop";
            this.RunStop.Size = new System.Drawing.Size(115, 41);
            this.RunStop.TabIndex = 2;
            this.RunStop.Text = "Run/Stop";
            this.RunStop.UseVisualStyleBackColor = true;
            this.RunStop.Click += new System.EventHandler(this.RunStop_Click);
            // 
            // Fmin
            // 
            this.Fmin.Location = new System.Drawing.Point(12, 321);
            this.Fmin.Name = "Fmin";
            this.Fmin.Size = new System.Drawing.Size(79, 41);
            this.Fmin.TabIndex = 3;
            this.Fmin.Text = "Fmin";
            this.Fmin.UseVisualStyleBackColor = true;
            this.Fmin.Click += new System.EventHandler(this.Fmin_Click);
            // 
            // Fmid
            // 
            this.Fmid.Location = new System.Drawing.Point(97, 321);
            this.Fmid.Name = "Fmid";
            this.Fmid.Size = new System.Drawing.Size(79, 41);
            this.Fmid.TabIndex = 4;
            this.Fmid.Text = "Fmid";
            this.Fmid.UseVisualStyleBackColor = true;
            this.Fmid.Click += new System.EventHandler(this.Fmid_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(182, 321);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(79, 41);
            this.button5.TabIndex = 5;
            this.button5.Text = "All 95";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.Button5_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 50;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 423);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(57, 41);
            this.button6.TabIndex = 9;
            this.button6.Text = "-";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.Button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(138, 423);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(57, 41);
            this.button7.TabIndex = 10;
            this.button7.Text = "+";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.Button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 470);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(57, 41);
            this.button8.TabIndex = 11;
            this.button8.Text = "=";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.Button8_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(138, 470);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(57, 41);
            this.button9.TabIndex = 12;
            this.button9.Text = "#";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.Button9_Click);
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(12, 368);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(79, 41);
            this.button10.TabIndex = 13;
            this.button10.Text = "□5";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.Button10_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(97, 368);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(79, 41);
            this.button11.TabIndex = 14;
            this.button11.Text = "□50";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.Button11_Click);
            // 
            // button12
            // 
            this.button12.Location = new System.Drawing.Point(182, 368);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(79, 41);
            this.button12.TabIndex = 15;
            this.button12.Text = "□95";
            this.button12.UseVisualStyleBackColor = true;
            this.button12.Click += new System.EventHandler(this.Button12_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(75, 470);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(57, 41);
            this.button13.TabIndex = 17;
            this.button13.Text = "||";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.Button13_Click);
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(75, 423);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(57, 41);
            this.button14.TabIndex = 16;
            this.button14.Text = "|";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.Button14_Click);
            // 
            // StayProb
            // 
            this.StayProb.Location = new System.Drawing.Point(95, 517);
            this.StayProb.Name = "StayProb";
            this.StayProb.Size = new System.Drawing.Size(100, 31);
            this.StayProb.TabIndex = 18;
            this.StayProb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.StayProb_KeyDown);
            // 
            // DeathProb
            // 
            this.DeathProb.Location = new System.Drawing.Point(95, 555);
            this.DeathProb.Name = "DeathProb";
            this.DeathProb.Size = new System.Drawing.Size(100, 31);
            this.DeathProb.TabIndex = 19;
            this.DeathProb.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DeathProb_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 524);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 24);
            this.label1.TabIndex = 20;
            this.label1.Text = "Stay";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 555);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 24);
            this.label2.TabIndex = 21;
            this.label2.Text = "Death";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(204, 423);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(57, 41);
            this.button1.TabIndex = 22;
            this.button1.Text = ".";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(279, 590);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DeathProb);
            this.Controls.Add(this.StayProb);
            this.Controls.Add(this.button13);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.button12);
            this.Controls.Add(this.button11);
            this.Controls.Add(this.button10);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.Fmid);
            this.Controls.Add(this.Fmin);
            this.Controls.Add(this.RunStop);
            this.Controls.Add(this.RefleshButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "CARGB";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button RefleshButton;
        private System.Windows.Forms.Button RunStop;
        private System.Windows.Forms.Button Fmin;
        private System.Windows.Forms.Button Fmid;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Button button12;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.TextBox StayProb;
        private System.Windows.Forms.TextBox DeathProb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
    }
}

