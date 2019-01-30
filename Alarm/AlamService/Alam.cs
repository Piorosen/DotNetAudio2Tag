using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    public delegate void LifeTimeEvent(object sender, AlamStruct AlamStruct);
    public partial class Alam : UserControl
    {
        public event LifeTimeEvent LifeTimeEnd;
        private void OnLifeTimeEnd(AlamStruct alamStruct)
        {
            LifeTimeEnd?.Invoke(this, alamStruct);
        }

        AlamStruct AlamStruct;

        public Alam(AlamStruct alam)
        {
            InitializeComponent();

            AlamStruct = alam;

            AlamStruct = alam;
            Label_Title.Text = AlamStruct.Title;
            Label_Body.Text = AlamStruct.Body;

            if (alam.BackColor != Color.Empty)
                Panel_Main.BackColor = AlamStruct.BackColor;

            if (alam.ImagePath != string.Empty && new FileInfo(alam.ImagePath).Exists)
            {
                try
                {
                    Picture_Image.Image = Image.FromFile(AlamStruct.ImagePath);
                    Label_Title.Size = new Size(Label_Title.Size.Width - Picture_Image.Size.Width - 10, Label_Title.Size.Height);
                    Label_Body.Size = new Size(Label_Body.Size.Width - Picture_Image.Size.Width - 10, Label_Body.Size.Height);
                }
                catch (Exception)
                {
                }
            }

            if (alam.BorderStyle != Picture_Image.BorderStyle)
                Picture_Image.BorderStyle = AlamStruct.BorderStyle;

            Timer.Start();
        }

        private void Panel_Main_Paint(object sender, PaintEventArgs e)
        {
            var g = (sender as Control);
            float Size = 5;
            g.CreateGraphics().DrawRectangle(new Pen(Color.Chartreuse, Size), Size / 2, Size / 2, g.Size.Width - Size, g.Size.Height - Size);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (AlamStruct.LifeTime == -1) return;
            AlamStruct.LifeTime -= Timer.Interval / 1000.0f;
            if (AlamStruct.LifeTime <= 0)
            {
                Timer.Stop();
                OnLifeTimeEnd(AlamStruct);
            }
        }

        private void Btn_Close_Click(object sender, EventArgs e)
        {
            OnLifeTimeEnd(AlamStruct);
        }
    }
}
