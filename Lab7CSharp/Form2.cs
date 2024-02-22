//using System;
//using System.ComponentModel;
//using System.Drawing;
//using System.Windows.Forms;

//namespace Lab7CSharp
//{
//    public partial class Form2 : Form
//    {
//        public Form2()
//        {
//            InitializeComponent();
//        }

//        private void Form2_Load(object sender, EventArgs e) { }

//        private void button4_Click(object sender, EventArgs e)
//        {

//        }

//        private void button5_Click(object sender, EventArgs e)
//        {

//        }

//        private void button6_Click(object sender, EventArgs e)
//        {

//        }

//        private void button7_Click(object sender, EventArgs e)
//        {

//        }
//    }
//}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form2 : Form
    {
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        public Form2()
        {
            InitializeComponent();

            // Initialize components
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();

            // Set up OpenFileDialog
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.jpeg;*.png|All Files|*.*";
            openFileDialog.Title = "Select an Image File";

            // Set up SaveFileDialog
            saveFileDialog.Filter = "BMP Image|*.bmp|JPEG Image|*.jpg;*.jpeg|PNG Image|*.png";
            saveFileDialog.Title = "Save Converted Image";

            // Set up pictureBox1
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            // Attach event handlers
            openFileDialog.FileOk += OpenFileDialog_FileOk;

            this.FormClosing += Form_FormClosing;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.Owner is Form1)
            {
                form1 = (Form1)this.Owner;
            }
        }

        private void Form2_Load(object sender, EventArgs e) { }

        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ConvertToGrayscale(pictureBox1.Image);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // Check if the image is null before attempting to save
            if (pictureBox1.Image == null)
            {
                MessageBox.Show(
                    "Please load and convert an image first.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            // Show SaveFileDialog
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the file name selected by the user
                string fileName = saveFileDialog.FileName;

                // Save the image to the specified file
                pictureBox1.Image.Save(fileName);
            }
        }

        private void OpenFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            // Load the selected image into the pictureBox1
            pictureBox1.Image = new Bitmap(openFileDialog.FileName);
        }

        private void ConvertToGrayscale(Image image)
        {
            if (image == null)
            {
                MessageBox.Show(
                    "Please load an image first.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return;
            }

            Bitmap original = new Bitmap(image);

            for (int y = 0; y < original.Height; y++)
            {
                for (int x = 0; x < original.Width; x++)
                {
                    Color pixelColor = original.GetPixel(x, y);
                    int grayValue = (int)(
                        pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11
                    );
                    Color newColor = Color.FromArgb(grayValue, grayValue, grayValue);
                    original.SetPixel(x, y, newColor);
                }
            }

            // Display the grayscale image
            pictureBox1.Image = original;
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
