using System;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using WindowsFormsTemp.Calculator;
using WindowsFormsTemp.Filters;
using WindowsFormsTemp.ImagePrimitives;
using WindowsFormsTemp.Properties;

namespace WindowsFormsTemp
{
    public partial class MainForm : Form
    {
        private const string DefaultImagePath = "ImageData/image_Lena256gb.bmp";
        private IBitmap currentPlainBitmap;
        private IBitmap initialPlainBitmap;

        public MainForm()
        {
            InitializeComponent();
            initialPlainBitmap = new Bitmap(DefaultImagePath).ToPlainBitmap();
            currentPlainBitmap = initialPlainBitmap;

            yLabel.Text = yTrackBar.Value.ToString(CultureInfo.InvariantCulture);
            uLabel.Text = uTrackBar.Value.ToString(CultureInfo.InvariantCulture);
            vLabel.Text = vTrackBar.Value.ToString(CultureInfo.InvariantCulture);

            UpdateState();
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new OpenFileDialog())
            {
                fileDialog.Title = Resources.Form1_LoadImageButton_Click_Open_image;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    var initialBitmap = new Bitmap(fileDialog.FileName);
                    initialPlainBitmap = initialBitmap.ToPlainBitmap();
                    currentPlainBitmap = initialPlainBitmap;

                    UpdateCheckBox();
                }
            }
        }

        private void vTrackBar_Scroll(object sender, EventArgs e)
        {
            vLabel.Text = vTrackBar.Value.ToString(CultureInfo.InvariantCulture);
            UpdateScrollsState();
        }

        private void uTrackBar_Scroll(object sender, EventArgs e)
        {
            uLabel.Text = uTrackBar.Value.ToString(CultureInfo.InvariantCulture);
            UpdateScrollsState();
        }

        private void yTrackBar_Scroll(object sender, EventArgs e)
        {
            yLabel.Text = yTrackBar.Value.ToString(CultureInfo.InvariantCulture);
            UpdateScrollsState();
        }

        private void UpdateScrollsState()
        {
            UpdateCheckBox();
        }

        private void UpdateState()
        {
            initialPictureBox.Image = initialPlainBitmap.ToDotNetBitmap();
            currentPictureBox.Image = currentPlainBitmap.ToDotNetBitmap();

            string psnrText = PsnrCalculator.Instance.Calculate(initialPlainBitmap, currentPlainBitmap)
                                            .ToString("F3", CultureInfo.InvariantCulture);

            if (psnrText == "Infinity")
            {
                psnrLabel.Text = @"∞";
                psnrLabel.Font = new Font(new FontFamily("Times New Roman"), 12.0f);
                psnrLabel.Location = new Point(psnrLabel.Location.X, label2.Location.Y - 4);
            }
            else
            {
                psnrLabel.Text = psnrText + @" dB";
                psnrLabel.Font = new Font(new FontFamily("Times New Roman"), 10.0f);
                psnrLabel.Location = new Point(psnrLabel.Location.X, label2.Location.Y - 2);
            }
        }

        private void UpdateCheckBox()
        {
            currentPlainBitmap = initialPlainBitmap;
            if (grayscaleCheckBox.Checked)
            {
                equalWeightRadioButton.Enabled = true;
                ccir6011RadioButton.Enabled = true;

                if (equalWeightRadioButton.Checked)
                    currentPlainBitmap = currentPlainBitmap.Apply(GrayScaleFilter.EqualWeights);
                if (ccir6011RadioButton.Checked)
                    currentPlainBitmap = currentPlainBitmap.Apply(GrayScaleFilter.Ccir6011);
            }
            else
            {
                equalWeightRadioButton.Enabled = false;
                ccir6011RadioButton.Enabled = false;
                equalWeightRadioButton.Checked = false;
                ccir6011RadioButton.Checked = false;
            }

            if (yuvCheckBox.Checked)
            {
                currentPlainBitmap = currentPlainBitmap.Apply(YuvFilter.Instance, new YuvData
                    {
                        YQuantizationDegree = (byte) yTrackBar.Value,
                        UQuantizationDegree = (byte) uTrackBar.Value,
                        VQuantizationDegree = (byte) vTrackBar.Value,
                    });

                yTrackBar.Enabled = true;
                uTrackBar.Enabled = true;
                vTrackBar.Enabled = true;
            }
            else
            {
                yTrackBar.Enabled = false;
                uTrackBar.Enabled = false;
                vTrackBar.Enabled = false;
            }
            UpdateState();
        }

        private void yuvCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }

        private void shiftButton_Click(object sender, EventArgs e)
        {
            initialPlainBitmap = currentPlainBitmap;
            UpdateCheckBox();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            int size = Math.Min(Size.Width*256/859, Size.Height*256/547);
            currentPictureBox.Size = new Size(size, size);
            initialPictureBox.Size = new Size(size, size);

            initialPictureBox.Location = new Point(12, 35);
            currentPictureBox.Location = new Point(Size.Width - 25 - currentPictureBox.Size.Width, 35);

            shiftButton.Location =
                new Point(
                    (initialPictureBox.Location.X + currentPictureBox.Location.X + currentPictureBox.Size.Width)/2 -
                    shiftButton.Size.Width/2,
                    (initialPictureBox.Location.Y + currentPictureBox.Location.Y + currentPictureBox.Size.Height)/2 -
                    shiftButton.Size.Height/2);
        }

        private void saveImageButton_Click(object sender, EventArgs e)
        {
            using (var fileDialog = new SaveFileDialog())
            {
                fileDialog.FileName = "bitmap.bmp";
                fileDialog.Filter = Resources.MainForm_saveImageButton_Click_bmp_files____bmp____bmp_All_files__________;
                fileDialog.Title = Resources.MainForm_saveImageButton_Click_Save_image;

                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    currentPlainBitmap.ToDotNetBitmap().Save(fileDialog.FileName);
                }
            }
        }

        private void grayscaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }

        private void equalWeightRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }

        private void ccir6011RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }
    }
}