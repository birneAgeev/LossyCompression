namespace WindowsFormsTemp
{
    partial class MainForm
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
            this.shiftButton = new System.Windows.Forms.Button();
            this.saveImageButton = new System.Windows.Forms.Button();
            this.grayscaleBox = new System.Windows.Forms.GroupBox();
            this.equalWeightRadioButton = new System.Windows.Forms.RadioButton();
            this.ccir6011RadioButton = new System.Windows.Forms.RadioButton();
            this.grayscaleCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.currentPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.initialPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTrackBar)).BeginInit();
            this.grayscaleBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // currentPictureBox
            // 
            this.currentPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentPictureBox.Location = new System.Drawing.Point(575, 35);
            this.currentPictureBox.MaximumSize = new System.Drawing.Size(768, 768);
            this.currentPictureBox.MinimumSize = new System.Drawing.Size(256, 256);
            this.currentPictureBox.Name = "currentPictureBox";
            this.currentPictureBox.Size = new System.Drawing.Size(256, 256);
            this.currentPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.currentPictureBox.TabIndex = 0;
            this.currentPictureBox.TabStop = false;
            // 
            // initialPictureBox
            // 
            this.initialPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.initialPictureBox.Location = new System.Drawing.Point(12, 35);
            this.initialPictureBox.MaximumSize = new System.Drawing.Size(768, 768);
            this.initialPictureBox.MinimumSize = new System.Drawing.Size(256, 256);
            this.initialPictureBox.Name = "initialPictureBox";
            this.initialPictureBox.Size = new System.Drawing.Size(256, 256);
            this.initialPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
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
            this.yuvCheckBox.Location = new System.Drawing.Point(601, 328);
            this.yuvCheckBox.Name = "yuvCheckBox";
            this.yuvCheckBox.Size = new System.Drawing.Size(117, 17);
            this.yuvCheckBox.TabIndex = 11;
            this.yuvCheckBox.Text = "Enable YCrCb Filter";
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
            // shiftButton
            // 
            this.shiftButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.shiftButton.Location = new System.Drawing.Point(422, 157);
            this.shiftButton.Name = "shiftButton";
            this.shiftButton.Size = new System.Drawing.Size(28, 24);
            this.shiftButton.TabIndex = 18;
            this.shiftButton.Text = "<-";
            this.shiftButton.UseVisualStyleBackColor = true;
            this.shiftButton.Click += new System.EventHandler(this.shiftButton_Click);
            // 
            // saveImageButton
            // 
            this.saveImageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.saveImageButton.Location = new System.Drawing.Point(756, 350);
            this.saveImageButton.Name = "saveImageButton";
            this.saveImageButton.Size = new System.Drawing.Size(75, 23);
            this.saveImageButton.TabIndex = 19;
            this.saveImageButton.Text = "Save Image";
            this.saveImageButton.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.saveImageButton.UseVisualStyleBackColor = true;
            this.saveImageButton.Click += new System.EventHandler(this.saveImageButton_Click);
            // 
            // grayscaleBox
            // 
            this.grayscaleBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grayscaleBox.Controls.Add(this.ccir6011RadioButton);
            this.grayscaleBox.Controls.Add(this.equalWeightRadioButton);
            this.grayscaleBox.Location = new System.Drawing.Point(575, 388);
            this.grayscaleBox.Name = "grayscaleBox";
            this.grayscaleBox.Size = new System.Drawing.Size(200, 100);
            this.grayscaleBox.TabIndex = 20;
            this.grayscaleBox.TabStop = false;
            this.grayscaleBox.Text = "Grayscale Settings";
            // 
            // equalWeightRadioButton
            // 
            this.equalWeightRadioButton.AutoSize = true;
            this.equalWeightRadioButton.Enabled = false;
            this.equalWeightRadioButton.Location = new System.Drawing.Point(26, 28);
            this.equalWeightRadioButton.Name = "equalWeightRadioButton";
            this.equalWeightRadioButton.Size = new System.Drawing.Size(94, 17);
            this.equalWeightRadioButton.TabIndex = 0;
            this.equalWeightRadioButton.TabStop = true;
            this.equalWeightRadioButton.Text = "Equal Weights";
            this.equalWeightRadioButton.UseVisualStyleBackColor = true;
            this.equalWeightRadioButton.CheckedChanged += new System.EventHandler(this.equalWeightRadioButton_CheckedChanged);
            // 
            // ccir6011RadioButton
            // 
            this.ccir6011RadioButton.AutoSize = true;
            this.ccir6011RadioButton.Enabled = false;
            this.ccir6011RadioButton.Location = new System.Drawing.Point(26, 65);
            this.ccir6011RadioButton.Name = "ccir6011RadioButton";
            this.ccir6011RadioButton.Size = new System.Drawing.Size(80, 17);
            this.ccir6011RadioButton.TabIndex = 1;
            this.ccir6011RadioButton.TabStop = true;
            this.ccir6011RadioButton.Text = "CCIR 601-1";
            this.ccir6011RadioButton.UseVisualStyleBackColor = true;
            this.ccir6011RadioButton.CheckedChanged += new System.EventHandler(this.ccir6011RadioButton_CheckedChanged);
            // 
            // grayscaleCheckBox
            // 
            this.grayscaleCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grayscaleCheckBox.AutoSize = true;
            this.grayscaleCheckBox.Location = new System.Drawing.Point(601, 356);
            this.grayscaleCheckBox.Name = "grayscaleCheckBox";
            this.grayscaleCheckBox.Size = new System.Drawing.Size(134, 17);
            this.grayscaleCheckBox.TabIndex = 21;
            this.grayscaleCheckBox.Text = "Enable Grayscale Filter";
            this.grayscaleCheckBox.UseVisualStyleBackColor = true;
            this.grayscaleCheckBox.CheckedChanged += new System.EventHandler(this.grayscaleCheckBox_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 509);
            this.Controls.Add(this.grayscaleCheckBox);
            this.Controls.Add(this.grayscaleBox);
            this.Controls.Add(this.saveImageButton);
            this.Controls.Add(this.shiftButton);
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
            this.MinimumSize = new System.Drawing.Size(859, 547);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.currentPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.initialPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vTrackBar)).EndInit();
            this.grayscaleBox.ResumeLayout(false);
            this.grayscaleBox.PerformLayout();
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
        private System.Windows.Forms.Button shiftButton;
        private System.Windows.Forms.Button saveImageButton;
        private System.Windows.Forms.GroupBox grayscaleBox;
        private System.Windows.Forms.RadioButton ccir6011RadioButton;
        private System.Windows.Forms.RadioButton equalWeightRadioButton;
        private System.Windows.Forms.CheckBox grayscaleCheckBox;
    }
}

