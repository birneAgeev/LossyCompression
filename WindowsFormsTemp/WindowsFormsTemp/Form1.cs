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
    public partial class Form1 : Form
    {
        private const string DefaultImagePath = "ImageData/image_Lena256gb.bmp";
        private IBitmap currentPlainBitmap;
        private IBitmap initialPlainBitmap;

        public Form1()
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
            currentPlainBitmap = initialPlainBitmap.Apply(YuvFilter.Instance, new YuvData
            {
                YQuantizationDegree = (byte) yTrackBar.Value,
                UQuantizationDegree = (byte) uTrackBar.Value,
                VQuantizationDegree = (byte) vTrackBar.Value,
            });
            UpdateState();
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
            if (yuvCheckBox.Checked)
            {
                currentPlainBitmap = initialPlainBitmap.Apply(YuvFilter.Instance, new YuvData
                {
                    YQuantizationDegree = (byte) yTrackBar.Value,
                    UQuantizationDegree = (byte) uTrackBar.Value,
                    VQuantizationDegree = (byte) vTrackBar.Value,
                });

                yTrackBar.Enabled = true;
                uTrackBar.Enabled = true;
                vTrackBar.Enabled = true;

                UpdateState();
            }
            else
            {
                yTrackBar.Enabled = false;
                uTrackBar.Enabled = false;
                vTrackBar.Enabled = false;

                currentPlainBitmap = initialPlainBitmap;
                UpdateState();
            }
        }

        private void yuvCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateCheckBox();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            initialPlainBitmap = currentPlainBitmap;
            UpdateCheckBox();
        }
    }
}