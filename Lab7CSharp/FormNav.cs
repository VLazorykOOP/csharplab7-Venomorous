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
        private Form2 form2;

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            form2.Show();
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
}
