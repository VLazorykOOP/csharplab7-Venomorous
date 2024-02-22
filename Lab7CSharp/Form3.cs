using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab7CSharp
{
    public partial class Form3 : Form
    {
        Random random = new Random();
        private List<Figure> figures;

        Graphics g;

        public Form3()
        {
            InitializeComponent();

            InitializeComboBox();

            figures = new List<Figure>();
            g = pictureBox1.CreateGraphics();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.Owner is Form1)
            {
                form1 = (Form1)this.Owner;
            }
        }

        private void InitializeComboBox()
        {
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DrawItem += ComboBoxColors_DrawItem;

            List<Color> colorList = new List<Color>
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
            };

            foreach (Color color in colorList)
            {
                comboBox1.Items.Add(color);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void ComboBoxColors_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;
                Color itemColor = (Color)comboBox.Items[e.Index];

                // Draw the color rectangle
                e.Graphics.FillRectangle(
                    new SolidBrush(itemColor),
                    e.Bounds.Left + 1,
                    e.Bounds.Top + 1,
                    20,
                    e.Bounds.Height - 2
                );

                // Draw the color name
                e.Graphics.DrawString(
                    itemColor.Name,
                    comboBox.Font,
                    Brushes.Black,
                    e.Bounds.Left + 25,
                    e.Bounds.Top + 1
                );

                // If the item is selected, draw a focus rectangle
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.DrawRectangle(
                        Pens.Black,
                        e.Bounds.Left,
                        e.Bounds.Top,
                        e.Bounds.Width - 1,
                        e.Bounds.Height - 1
                    );
                }

                e.DrawFocusRectangle();
            }
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            string[] figureNames = { "П’ятикутник", "Прямокутник", "Трикутник", "Ромб" };

            int yPos = 20;

            foreach (string figureName in figureNames)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.Text = figureName;
                radioButton.Location = new Point(10, yPos);
                radioButton.CheckedChanged += RadioButton_CheckedChanged; // Add event handler
                groupBox1.Controls.Add(radioButton);

                // Increment yPos by the height of the radio button plus some spacing (e.g., 5)
                yPos += radioButton.Height + 5;
            }

            ((RadioButton)groupBox1.Controls[0]).Checked = true;

            UpdateLabels(figureNames[0]); // Update labels based on the first figure

            int groupBoxHeight = yPos; // Height is now based on the accumulated yPos
            groupBox1.Height = groupBoxHeight;

            this.FormClosing += Form_FormClosing;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Update labels based on the selected figure
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                string selectedFigure = radioButton.Text;
                UpdateLabels(selectedFigure);
            }
        }

        private void UpdateLabels(string selectedFigure)
        {
            // Update labels based on the selected figure
            switch (selectedFigure)
            {
                case "П’ятикутник":
                    label3.Text = "Ширина:";
                    label4.Visible = false;
                    textBox3.Visible = false;
                    break;

                case "Прямокутник":
                    label3.Text = "Довжина:";
                    label4.Text = "Ширина:";
                    label4.Visible = true;
                    textBox3.Visible = true;
                    break;

                case "Трикутник":
                    label3.Text = "Сторона A:";
                    label4.Text = "Сторона B:";
                    label4.Visible = true;
                    textBox3.Visible = true;
                    break;

                case "Ромб":
                    label3.Text = "Діагональ 1:";
                    label4.Text = "Діагональ 2:";
                    label4.Visible = true;
                    textBox3.Visible = true;
                    break;

                default:
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the selected figure type
            string selectedFigure = groupBox1
                .Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked)
                ?.Text;

            if (selectedFigure != null)
            {
                // Get the selected color
                Color selectedColor = (Color)comboBox1.SelectedItem;

                for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
                {
                    // Create a figure based on the selected parameters
                    Figure newFigure = CreateFigure(selectedFigure, selectedColor);

                    figures.Add(newFigure);
                }

                DrawFigures(figures, g);
            }

            //Console.WriteLine("figures: ");
            //foreach (Figure figure in figures)
            //{
            //    Console.WriteLine(figure);
            //    Console.WriteLine($"X & Y : {figure.X} {figure.Y}");
            //}
        }

        private Figure CreateFigure(string figureType, Color color)
        {
            int x = random.Next(0, pictureBox1.Width - 1);
            int y = random.Next(0, pictureBox1.Height - 1);

            switch (figureType)
            {
                case "П’ятикутник":
                    int side = int.Parse(textBox2.Text);
                    return new Pentagon(x, y, side, color);
                case "Прямокутник":
                    int length = int.Parse(textBox2.Text);
                    int width = int.Parse(textBox3.Text);
                    return new RectangleFigure(x, y, color, length, width);
                case "Трикутник":
                    int sideA = int.Parse(textBox2.Text);
                    int sideB = int.Parse(textBox3.Text);
                    return new Triangle(x, y, color, sideA, sideB);
                case "Ромб":
                    int diagonal1 = int.Parse(textBox2.Text);
                    int diagonal2 = int.Parse(textBox3.Text);
                    return new Rhombus(x, y, color, diagonal1, diagonal2);
                default:
                    return null;
            }
        }

        private void DrawFigures(List<Figure> figures, Graphics graphics)
        {
            pictureBox1.BackColor = Color.White;

            foreach (Figure figure in figures)
            {
                figure.Draw(graphics);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // Pass the Graphics object from PaintEventArgs to the DrawFigures method
            DrawFigures(figures, e.Graphics);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            figures.Clear();
        }
    }
}
