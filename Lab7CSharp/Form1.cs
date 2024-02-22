using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        private Form2 form2;
        private Form3 form3;

        public Form1()
        {
            InitializeComponent();
            form2 = new Form2();
            form3 = new Form3();

            form2.Owner = this;
            form3.Owner = this;
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            // Get the current time and extract seconds
            int seconds = DateTime.Now.Second;

            // Display seconds in decimal
            label1.Text = $"Decimal: {seconds}";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get the current time and extract seconds
            int seconds = DateTime.Now.Second;

            // Convert seconds to binary
            string binarySeconds = Convert.ToString(seconds, 2);

            // Display seconds in binary
            label1.Text = $"Binary: {binarySeconds}";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Get the current time and extract seconds
            int seconds = DateTime.Now.Second;

            // Convert seconds to hexadecimal
            string hexSeconds = seconds.ToString("X");

            // Display seconds in hexadecimal
            label1.Text = $"Hexadecimal: {hexSeconds}";
        }
    }
}
