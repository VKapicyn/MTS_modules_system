using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            InitEvents();
        }

        public void InitEvents()
        {
            listView1.Clear();
            listView1.Columns.Add("N", 30, HorizontalAlignment.Right);
            listView1.Columns.Add("Date", 70, HorizontalAlignment.Left);
            listView1.Columns.Add("Time", 80, HorizontalAlignment.Left);
            listView1.Columns.Add("Source", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Event", 220, HorizontalAlignment.Left);
        }

        public void AddEvent(string source, string text)
        {
            string str = "";
            DateTime time = DateTime.Now;

            ListViewItem item = new ListViewItem((listView1.Items.Count + 1).ToString());
            for (int k = 1; k < listView1.Columns.Count; k++)
            {
                item.SubItems.Add("");
                string s = "";
                ColumnHeader col = listView1.Columns[k];
                if (col.Text == "Date")
                    s = time.ToShortDateString();
                else if (col.Text == "Time")
                    s = String.Format("{0}.{1:000}", time.ToLongTimeString(), time.Millisecond);
                else if (col.Text == "Source")
                    s = source;
                else if (col.Text == "Event")
                    s = text;
                item.SubItems[k].Text = s;
                str = str + s;
            }
            listView1.Items.Add(item);

            if (listView1.SelectedItems.Count == 1 && listView1.SelectedItems[0].Index == listView1.Items.Count - 2)
            {
                listView1.SelectedItems[0].Selected = false;
                item.Selected = true;
                item.Focused = true;
                item.EnsureVisible();
            }
            else if (listView1.SelectedItems.Count == 0)
                item.EnsureVisible();

        }

        private void Form1_Closing(object sender, FormClosingEventArgs e)
        {
            DateTime close_time = DateTime.Now;
            String name = "log_" + close_time.ToShortDateString() + ".txt";
            StreamWriter sw = new StreamWriter(name, true, Encoding.UTF8);
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                sw.WriteLine(listView1.Items[i].SubItems[0].Text + "  |  " +
                    listView1.Items[i].SubItems[1].Text + "  |  " +
                    listView1.Items[i].SubItems[2].Text + "  |  " +
                    listView1.Items[i].SubItems[3].Text + "  |  " +
                    listView1.Items[i].SubItems[4].Text
                    );
            }
            sw.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddEvent("System", "All positions closed, robot stopped!!!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("first","10","-","B");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button6.Enabled = true;
            button5.Enabled = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            button6.Enabled = false;
            button5.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("first", "10", "-", "S");
        }

    }
}
