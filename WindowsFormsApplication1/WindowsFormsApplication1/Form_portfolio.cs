using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form_portfolio : Form
    {
        public Form_portfolio()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < (dataGridView1.RowCount-1); i++)
            {
                Portfolio.p portfel;
                portfel.ticker = dataGridView1.Rows[i].Cells[0].Value.ToString();
                portfel.amount = Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                portfel.type = Char.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                Portfolio.portfel.Add(portfel);
            }
        }
    }
}
