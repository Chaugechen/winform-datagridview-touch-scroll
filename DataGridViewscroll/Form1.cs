using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataGridViewscroll
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            for (int i =0 ;i< 100;i++)
            {
                dataGridView1.Rows.Add(i,"User" + i);
            }
            new TouchGrid(dataGridView1);        
        }
    }
}
