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
    public partial class Form3 : Form
    {
        private String name, login, password, server;
        private Int32 port = 7800;

        public Form3()
        {
            InitializeComponent();
        }

        private bool check_correct()
        {
            //проверка на корректный ввод
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (check_correct() == true)
            {
                Accounts acc = new Accounts(name, login, password, server, port);
                (Application.OpenForms[1] as Form2).dataGridView1.Rows.Add("111","111","222");
                this.Close();
            }
            else
            {
                MessageBox.Show("Некорректный ввод.");
                return;
            }
        }
    }
}
