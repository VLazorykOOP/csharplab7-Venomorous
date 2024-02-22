using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            form2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            form3.Show();
        }
    }

    public partial class Form2 : Form
    {
        private Form1 form1;

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            form1.Show();
        }
    }

    public partial class Form3 : Form
    {
        private Form1 form1;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            form1.Show();
        }
    }
}
