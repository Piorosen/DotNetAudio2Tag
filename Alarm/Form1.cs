using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Library;

namespace Status_Alam
{
    public partial class Form1 : Form
    {
        AlamManage manage;

        public Form1()
        {
            InitializeComponent();

            manage = new AlamManage(this);
            manage.Size = 100;
            manage.Add(new AlamStruct("Hi", "Test"));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                manage.Add(new AlamStruct("A" + i, "123412312", (i + 4)));
            }
        }
    }
}
