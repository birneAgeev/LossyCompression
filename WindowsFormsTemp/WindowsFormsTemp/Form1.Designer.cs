namespace WindowsFormsTemp
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.currentPictureBox = new System.Windows.Forms.PictureBox();
            this.initialPictureBox = new System.Windows.Forms.PictureBox();
            this.uTrackBar = new System.Windows.Forms.TrackBar();
            this.yTrackBar = new System.Windows.Forms.TrackBar();
            this.vTrackBar = new System.Windows.Forms.TrackBar();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LoadImageButton = new System.Windows.Forms.Button();
            this.psnrLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.yuvCheckBox = new System.Windows.Forms.CheckBox();
            this.yLabel = new System.Windows.Forms.Label();
            this.uLabel = new System.Windows.Forms.Label();
            this.vLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.currentPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.initialPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // currentPictureBox
            // 
            this.currentPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentPictureBox.Location = new System.Drawing.Point(575, 35);
            this.currentPictureBox.Name = "currentPictureBox";
            this.currentPictureBox.Size = new System.Drawing.Size(256, 256);
            this.currentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.currentPictureBox.TabIndex = 0;
            this.currentPictureBox.TabStop = false;
            // 
            // initialPictureBox
            // 
            this.initialPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.initialPictureBox.Location = new System.Drawing.Point(12, 35);
            this.initialPictureBox.Name = "initialPictureBox";
            this.initialPictureBox.Size = new System.Drawing.Size(256, 256);
            this.initialPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.initialPictureBox.TabIndex = 1;
            this.initialPictureBox.TabStop = false;
            // 
            // uTrackBar
            // 
            this.uTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uTrackBar.Enabled = false;
            this.uTrackBar.Location = new System.Drawing.Point(360, 388);
            this.uTrackBar.Maximum = 8;
            this.uTrackBar.Name = "uTrackBar";
            this.uTrackBar.Size = new System.Drawing.Size(127, 45);
            this.uTrackBar.TabIndex = 5;
            this.uTrackBar.Value = 8;
            this.uTrackBar.Scroll += new System.EventHandler(this.uTrackBar_Scroll);
            // 
            // yTrackBar
            // 
            this.yTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yTrackBar.Enabled = false;
            this.yTrackBar.Location = new System.Drawing.Point(360, 328);
            this.yTrackBar.Maximum = 8;
            this.yTrackBar.Name = "yTrackBar";
            this.yTrackBar.Size = new System.Drawing.Size(127, 45);
            this.yTrackBar.TabIndex = 6;
            this.yTrackBar.Value = 8;
            this.yTrackBar.Scroll += new System.EventHandler(this.yTrackBar_Scroll);
            // 
            // vTrackBar
            // 
            this.vTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.vTrackBar.Enabled = false;
            this.vTrackBar.Location = new System.Drawing.Point(360, 453);
            this.vTrackBar.Maximum = 8;
            this.vTrackBar.Name = "vTrackBar";
            this.vTrackBar.Size = new System.Drawing.Size(127, 45);
            this.vTrackBar.TabIndex = 7;
            this.vTrackBar.Value = 8;
            this.vTrackBar.Scroll += new System.EventHandler(this.vTrackBar_Scroll);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // LoadImageButton
            // 
            this.LoadImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LoadImageButton.Location = new System.Drawing.Point(12, 350);
            this.LoadImageButton.Name = "LoadImageButton";
            this.LoadImageButton.Size = new System.Drawing.Size(75, 23);
            this.LoadImageButton.TabIndex = 8;
            this.LoadImageButton.Text = "Load Image";
            this.LoadImageButton.UseVisualStyleBackColor = true;
            this.LoadImageButton.Click += new System.EventHandler(this.LoadImageButton_Click);
            // 
            // psnrLabel
            // 
            this.psnrLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.psnrLabel.AutoSize = true;
            this.psnrLabel.Location = new System.Drawing.Point(216, 355);
            this.psnrLabel.Name = "psnrLabel";
            this.psnrLabel.Size = new System.Drawing.Size(0, 13);
            this.psnrLabel.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(176, 360);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "PSNR:";
            // 
            // yuvCheckBox
            // 
            this.yuvCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yuvCheckBox.AutoSize = true;
            this.yuvCheckBox.Location = new System.Drawing.Point(623, 388);
            this.yuvCheckBox.Name = "yuvCheckBox";
            this.yuvCheckBox.Size = new System.Drawing.Size(109, 17);
            this.yuvCheckBox.TabIndex = 11;
            this.yuvCheckBox.Text = "Enable YUV Filter";
            this.yuvCheckBox.UseVisualStyleBackColor = true;
            this.yuvCheckBox.CheckedChanged += new System.EventHandler(this.yuvCheckBox_CheckedChanged);
            // 
            // yLabel
            // 
            this.yLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(493, 328);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(0, 13);
            this.yLabel.TabIndex = 12;
            // 
            // uLabel
            // 
            this.uLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.uLabel.AutoSize = true;
            this.uLabel.Location = new System.Drawing.Point(493, 388);
            this.uLabel.Name = "uLabel";
            this.uLabel.Size = new System.Drawing.Size(0, 13);
            this.uLabel.TabIndex = 13;
            // 
            // vLabel
            // 
            this.vLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.vLabel.AutoSize = true;
            this.vLabel.Location = new System.Drawing.Point(493, 453);
            this.vLabel.Name = "vLabel";
            this.vLabel.Size = new System.Drawing.Size(0, 13);
            this.vLabel.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(357, 312);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(357, 372);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(15, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "U";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(357, 437);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(14, 13);
            this.label8.TabIndex = 17;
            this.label8.Text = "V";
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(422, 157);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(28, 24);
            this.button1.TabIndex = 18;
            this.button1.Text = "<-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 509);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.vLabel);
            this.Controls.Add(this.uLabel);
            this.Controls.Add(this.yLabel);
            this.Controls.Add(this.yuvCheckBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.psnrLabel);
            this.Controls.Add(this.LoadImageButton);
            this.Controls.Add(this.vTrackBar);
            this.Controls.Add(this.yTrackBar);
            this.Controls.Add(this.uTrackBar);
            this.Controls.Add(this.initialPictureBox);
            this.Controls.Add(this.currentPictureBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.currentPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.initialPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox currentPictureBox;
        private System.Windows.Forms.PictureBox initialPictureBox;
        private System.Windows.Forms.TrackBar uTrackBar;
        private System.Windows.Forms.TrackBar yTrackBar;
        private System.Windows.Forms.TrackBar vTrackBar;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button LoadImageButton;
        private System.Windows.Forms.Label psnrLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox yuvCheckBox;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label uLabel;
        private System.Windows.Forms.Label vLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}

